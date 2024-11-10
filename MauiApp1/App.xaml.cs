namespace MauiApp1
{
    public partial class App : Application
    {
        public static Usuario usuariologeado {  get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }
    }
}
