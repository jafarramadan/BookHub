using BlazorApp2.Shared;
using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;

namespace BlazorApp2.Server.Repository
{
    public class AuthorsContext : MsSqlFluentRepository<AuthorModel>
    {
        public AuthorsContext(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
