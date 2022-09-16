namespace EShop.Models.Data.Repository.Interface
{
    public interface IRolesRepository : IDisposable
    {
        Task<List<Roles>> RoleList();
        Task<ViewModel.ErorVM> Update(Roles UpdateRole);
        Task<ViewModel.ErorVM> Delete(int id);

    }
}
