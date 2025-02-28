using MauiAppMinhasCompras.Models;
using SQLite;
//o mysql é o que chamamos de SGBD(sistema gerenciador de banco de dados), o SQlite é mais uma API
//interface de Programação de Aplicações. 
//É um conjunto de padrões, ferramentas e protocolos que permite
//a comunicação entre diferentes softwares e aplicativos. O Sqlite tem uma serie de metodos que podemos
//emular instruções sql, onde n precisamos escrever o bd, o metodo é chamado
namespace MauiAppMinhasCompras.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;


        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
          
        }
        //declarção dos metodos q vamos precisar
        //async do return, leitura e escrita em arquivo, to acessando a memoria do dispositivo que pode ser lenta
        public Task<int> Insert(Produto P) 
        {
            return _conn.InsertAsync(P);
        }

        public Task<List<Produto>> Update(Produto P) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, P.Descricao, P.Quantidade, P.Preco, P.Id
                );
        }

        public Task<int> Delete(int Id) //n de linhas que foram deletadas, vai deletar da tabela produto com base no criterio: para
            //cada item da table cujo a id for igual a id passada faz a selecao
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == Id);
        }

        public Task<List<Produto>> GetAll() 
        {
           return _conn.Table<Produto>().ToListAsync();
        } //pegar todos os produtos

        public Task<List<Produto>> Search(string Q) 
        {
            string sql = "SELECT * Produto WHERE Descricao LIKE '%" + Q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
        
    }
}
//parametro para fazer o caminho até o arquivo de texto
// o campo conn que vai receber um novo objeto que vai ser uma conexao com o arquivo de texto
//o wait vai esperar a tabela ser concluida, o create tabela serve para que o sqlite crie uma tabela 
//no bd SE ela n existir. O´P é o parametro do tipo produto que é a nossa classe model, so rola o insert ataraves
// de um model produto preenchdio