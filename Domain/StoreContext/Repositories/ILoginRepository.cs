namespace Domain.StoreContext.Repositories
{
    public interface ILoginRepository
    {
        bool CheckUser(string document, string email);
    }
}
