namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell(); nosso objeto navigtion page recebe um novo objeto para renderizar
            //é esperado uma nova tela para a ListaProduto
            // até agora foi criada uma classe model e alteramos a tela inicial
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
