using Microsoft.EntityFrameworkCore;

namespace EShop.Models.Data.Repository
{
    public class GroupRepository:Interface.IGroupRepository
    {
        private readonly Models.Data.ShopContext _Context;
        public GroupRepository(Models.Data.ShopContext _Context)
        {
            this._Context = _Context;
        }
        /// <summary>
        /// لیست همه دسته بندی های محصولات
        /// </summary>
        /// <returns></returns>
        public async Task<List<Models.Groups>> GroupList()
        {
            return await _Context.Groups.ToListAsync();
        }
        /// <summary>
        /// گروه جدید
        /// </summary>
        /// <param name="NewGroup"></param>
        /// <returns></returns>
        public async Task <ViewModel.ErorVM> New( Groups NewGroup )
		{
            if(NewGroup == null)  return new ViewModel.ErorVM() {State = false , AlertClass="danger" , Message = " گروه جدید خالیست" };
            List<Models.Groups> ? Groups = await _Context.Groups.Where(x=>x.Name == NewGroup.Name).ToListAsync();
            if(Groups != null && Groups.Count()>0) return new ViewModel.ErorVM() { State = false, AlertClass = "danger", Message = " نام گروه تکراری است" };
            NewGroup.CreateDate = DateTime.Now;
            NewGroup.ShowCreateDate = Utilities.PersianDateTime.GetDateTime();
            await _Context.Groups.AddAsync( NewGroup );  
            await _Context.SaveChangesAsync();
            return new ViewModel.ErorVM() { State = true, AlertClass = "success", Message = "گروه جدید با موفقیت ثبت شد" };
		}
        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
