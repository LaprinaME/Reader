using System.Windows;

namespace Reader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(); // Устанавливаем ViewModel как контекст данных

        }
    }
}


/*public partial class MainWindow : Window // Определение класса MainWindow, который является подклассом Window в WPF
{
    private Uri wordUri; // Поле для хранения URI файла Word
    private MediaPlayer mediaPlayer; // Объект для воспроизведения мультимедийных файлов
    private SpeechSynthesizer synthesizer; // Объект для синтеза речи
    private WordprocessingDocument wordDocument; // Объект для представления документа Word

    // Конструктор класса MainWindow
    public MainWindow()
    {
        InitializeComponent(); // Инициализация компонентов окна из файла XAML
        InitializeComponents(); // Вынесенный метод для инициализации компонентов приложения
        Closing += MainWindow_Closing; // Обработчик события закрытия окна
    }

    // Метод для инициализации компонентов приложения
    private void InitializeComponents()
    {
        synthesizer = new SpeechSynthesizer(); // Создание объекта синтеза речи
        mediaPlayer = new MediaPlayer(); // Создание объекта для воспроизведения мультимедийных файлов
    }

    // Обработчик события закрытия окна
    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        synthesizer.Dispose(); // Освобождение ресурсов синтезатора речи
        if (wordDocument != null)
        {
            wordDocument.Dispose(); // Освобождение ресурсов документа Word
        }
    }

    // Обработчик кнопки загрузки файла Word
    private void LoadWordButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog(); // Создание диалогового окна открытия файла
        openFileDialog.Filter = "Word Files|*.docx;*.doc"; // Фильтр для выбора только файлов Word

        if (openFileDialog.ShowDialog() == true) // Если пользователь выбрал файл и нажал "ОК"
        {
            string filePath = openFileDialog.FileName; // Получение пути к выбранному файлу
            if (wordDocument != null)
            {
                wordDocument.Dispose(); // Освобождение ресурсов предыдущего документа Word
            }
            wordDocument = WordprocessingDocument.Open(filePath, false); // Открытие документа Word
            FlowDocument flowDocument = ReadTextFromWordDocument(wordDocument); // Чтение текста из документа Word
            WordContainer.Document = flowDocument; // Устанавливаем FlowDocument в FlowDocumentReader
            SpeakText(flowDocument); // Воспроизведение текста с использованием синтезатора речи

            // Обновление wordUri
            wordUri = new Uri(filePath);

            // Обновление текстового поля
            FileLoadedText.Text = $"Файл загружен: {wordUri}";

            // Установка источника данных для mediaPlayer
            mediaPlayer.Open(new Uri(filePath));
        }
    }

    // Метод для чтения текста из документа Word и его представления в формате FlowDocument
    private FlowDocument ReadTextFromWordDocument(WordprocessingDocument document)
    {
        FlowDocument flowDocument = new FlowDocument(); // Создание объекта FlowDocument
        System.Windows.Documents.Paragraph paragraph = new System.Windows.Documents.Paragraph(); // Создание абзаца

        Body body = document.MainDocumentPart.Document.Body; // Получение основной части документа

        foreach (DocumentFormat.OpenXml.Wordprocessing.Paragraph p in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
        {
            System.Windows.Documents.Run run = new System.Windows.Documents.Run(p.InnerText + Environment.NewLine); // Создание объекта Run для текста абзаца
            paragraph.Inlines.Add(run); // Добавление объекта Run в абзац
        }

        flowDocument.Blocks.Add(paragraph); // Добавление абзаца в FlowDocument

        return flowDocument; // Возврат FlowDocument с прочитанным текстом
    }

    // Метод для воспроизведения текста с использованием синтезатора речи
    private void SpeakText(FlowDocument flowDocument)
    {
        TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd); // Получение диапазона текста из FlowDocument
        string text = textRange.Text; // Получение текста из диапазона

        if (!string.IsNullOrEmpty(text)) // Если текст не пуст
        {
            synthesizer.SpeakAsyncCancelAll(); // Остановка всех асинхронных операций синтеза речи
            synthesizer.SpeakAsync(text); // Асинхронное воспроизведение текста
        }
    }

    // Обработчик кнопки воспроизведения
    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        if (synthesizer.State == SynthesizerState.Paused) // Если синтезатор находится в состоянии паузы
        {
            synthesizer.Resume(); // Возобновление воспроизведения речи
            mediaPlayer.Play(); // Возобновление воспроизведения мультимедийного файла
        }
        else
        {
            if (mediaPlayer.Source != null) // Если у мультимедийного плеера есть источник данных
            {
                synthesizer.SpeakAsyncCancelAll(); // Остановка всех асинхронных операций синтеза речи
                synthesizer.SpeakAsync(mediaPlayer.Source.ToString()); // Воспроизведение текста, соответствующего источнику данных мультимедийного плеера
                mediaPlayer.Play(); // Воспроизведение мультимедийного файла
            }
        }
    }

    // Обработчик кнопки паузы
    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        if (synthesizer != null) // Если синтезатор существует
        {
            if (synthesizer.State == SynthesizerState.Speaking) // Если синтезатор находится в состоянии воспроизведения речи
            {
                synthesizer.Pause(); // Постановка синтезатора на паузу
                mediaPlayer.Pause(); // Постановка мультимедийного плеера на паузу
            }
        }
    }
}
}*/
