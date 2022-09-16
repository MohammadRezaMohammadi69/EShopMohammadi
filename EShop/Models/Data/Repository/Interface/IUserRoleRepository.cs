namespace EShop.Models.Data.Repository.Interface
{
    public interface IUserRoleRepository :IDisposable
    {
        Task<ViewModel.ErorVM> Delete(int userid);
        Task<ViewModel.ErorVM> Add(int userid, List<int> roleid);
    }
}
