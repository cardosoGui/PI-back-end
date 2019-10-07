using Dapper;
using Domain.StoreContext.Repositories;
using Repository.DataContexts;
using System.Data;

namespace Repository.StoreContext.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext dataContext;
        public LoginRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public bool CheckUser(string document, string email)
        {
            return dataContext.Connection.QueryFirstOrDefault<bool>(@"SELECT 1 FROM Customer WHERE Document = @Document AND Email = @Email", new { Document = document, Email = email }, commandType: CommandType.Text);
        }
    }
}
