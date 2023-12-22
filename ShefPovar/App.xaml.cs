namespace ShefPovar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
/*            MainPage mainPage = new MainPage();
            mainPage.BludoDatabaseService = new BludoDatabaseService(); // Используйте новое свойство
            MainPage.MainPageProperty = mainPage;*/
        }
    }
}