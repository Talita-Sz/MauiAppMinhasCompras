using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiAppMinhasCompras;
using MauiAppMinhasCompras.Models;
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

        public void Delete(int id) { }

        public void GetAll() { } //pegar todos os produtos

        public void Search(string Q) { }
        
    }
}
//parametro para fazer o caminho até o arquivo de texto
// o campo conn que vai receber um novo objeto que vai ser uma conexao com o arquivo de texto
//o wait vai esperar a tabela ser concluida, o create tabela serve para que o sqlite crie uma tabela 
//no bd SE ela n existir. O´P é o parametro do tipo produto que é a nossa classe model, so rola o insert ataraves
// de um model produto preenchdio