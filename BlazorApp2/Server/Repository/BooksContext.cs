using BlazorApp2.Shared;
using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;

namespace JLibrary.Server.Repository
{
    public class BooksContext : MsSqlFluentRepository<BookModel>
    {
        public BooksContext(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
