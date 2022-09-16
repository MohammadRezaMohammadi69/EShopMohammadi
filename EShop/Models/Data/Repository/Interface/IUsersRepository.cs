namespace EShop.Models.Data.Repository.Interface
{
    public interface IUsersRepository :IDisposable
    {
        Task<List<Models.Users>> UserList(int UserId, string Name);
        Task<bool> New(Models.Users user);
        Task<Users> FindUser(string username);
        Task<Users> FindUser(int userid);
        Task<ViewModel.ErorVM> Update(Models.Users user);
        Task<ViewModel.ErorVM> Delete(int id);
    }
}
