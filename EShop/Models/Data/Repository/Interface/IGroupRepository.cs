namespace EShop.Models.Data.Repository.Interface
{
    public interface IGroupRepository :IDisposable
    {
        Task<List<Models.Groups>> GroupList();
 
    }
}
