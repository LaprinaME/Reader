using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Media;
using System.Speech.Synthesis;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Documents;
using Reader.Model;

namespace Reader
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private Uri wordUri;
        private MediaPlayer mediaPlayer;
        private SpeechSynthesizer synthesizer;
        private WordprocessingDocument wordDocument;
        private FlowDocument _flowDocument;
        private string _fileLoadedText;

        public ViewModel()
        {
            InitializeCommands();
            InitializeComponents();
        }

        public FlowDocument FlowDocument
        {
            get { return _flowDocument; }
            set
            {
                _flowDocument = value;
                OnPropertyChanged();
            }
        }

        public string FileLoadedText
        {
            get { return _fileLoadedText; }
            set
            {
                _fileLoadedText = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoadWordCommand { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }

        private void InitializeCommands()
        {
            LoadWordCommand = new RelayCommand(param => LoadWord());
            PlayCommand = new RelayCommand(param => Play());
            PauseCommand = new RelayCommand(param => Pause());
        }

        private void InitializeComponents()
        {
            synthesizer = new SpeechSynthesizer();
            mediaPlayer = new MediaPlayer();
        }

        private void LoadWord()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word Files|*.docx;*.doc";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (wordDocument != null)
                {
                    wordDocument.Dispose();
                }
                wordDocument = WordprocessingDocument.Open(filePath, false);
                FlowDocument = ReadTextFromWordDocument(wordDocument);
                SpeakText(FlowDocument);

                wordUri = new Uri(filePath);

                FileLoadedText = $"Файл загружен: {wordUri}";

                mediaPlayer.Open(new Uri(filePath));
            }
        }

        private FlowDocument ReadTextFromWordDocument(WordprocessingDocument document)
        {
            FlowDocument flowDocument = new FlowDocument();
            System.Windows.Documents.Paragraph paragraph = new System.Windows.Documents.Paragraph();

            Body body = document.MainDocumentPart.Document.Body;

            foreach (DocumentFormat.OpenXml.Wordprocessing.Paragraph p in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                System.Windows.Documents.Run run = new System.Windows.Documents.Run(p.InnerText + Environment.NewLine);
                paragraph.Inlines.Add(run);
            }

            flowDocument.Blocks.Add(paragraph);

            return flowDocument;
        }

        private void SpeakText(FlowDocument flowDocument)
        {
            TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            string text = textRange.Text;

            if (!string.IsNullOrEmpty(text))
            {
                synthesizer.SpeakAsyncCancelAll();
                synthesizer.SpeakAsync(text);
            }
        }

        private void Play()
        {
            if (synthesizer.State == SynthesizerState.Paused)
            {
                synthesizer.Resume();
                mediaPlayer.Play();
            }
            else
            {
                if (mediaPlayer.Source != null)
                {
                    synthesizer.SpeakAsyncCancelAll();
                    synthesizer.SpeakAsync(mediaPlayer.Source.ToString());
                    mediaPlayer.Play();
                }
            }
        }

        private void Pause()
        {
            if (synthesizer != null)
            {
                if (synthesizer.State == SynthesizerState.Speaking)
                {
                    synthesizer.Pause();
                    mediaPlayer.Pause();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
