using MauiAppMinhasCompras.Helper;

namespace MauiAppMinhasCompras
{
    public partial class App : Application //membro do C, mebro privado q vai passar ppr um prop. p8ublica
    {
        static SQLiteDatabaseHelper _db;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "Banco_sqlite_compras.db3");//independete do caminho, isso garante que vamos encontrar o db3
                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }
        //esse arquivo pod estar em qualquer canto
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell(); nosso objeto navigtion page recebe um novo objeto para renderizar
            //é esperado uma nova tela para a ListaProduto
            // até agora foi criada uma classe model e alteramos a tela inicial
            // o slite é como um arquivo e pra acessar ele vai passar pela classe databasehelper que vai ter os metodos que vãpo fazer o crud
 //essa classe acessa a API do sqlite instalado e no pacote tem os metodos p/ fazer interação com os arquivos fde texto
 //nesse aqui a classe vai se torna disponivel em todo o app p/ fazer alterações
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
