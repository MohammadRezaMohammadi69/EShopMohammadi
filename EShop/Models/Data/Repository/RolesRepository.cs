using Microsoft.EntityFrameworkCore;

namespace EShop.Models.Data.Repository
{
    public class RolesRepository
    {
        private readonly ShopContext _context;
        public RolesRepository(ShopContext _context)
        {
            this._context = _context;
        }
        /// <summary>
        /// لیست همه نقش ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<Roles>> RoleList()
        {
            return await _context.Roles.ToListAsync();
        }
        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="UpdateRole"></param>
        /// <returns></returns>
        public async Task<ViewModel.ErorVM> Update(Roles UpdateRole)
        {
            Models.Roles ? OldRole = await _context.Roles.Where(x=>x.RoleId == UpdateRole.RoleId).FirstOrDefaultAsync();
            if (OldRole == null) { return new ViewModel.ErorVM() { State=false , AlertClass="danger" ,Message=" نقشی با این مشخضات یافت نشد "}; }
            OldRole.RoleName = UpdateRole.RoleName;
            _context.Roles.Update(OldRole);
            try// جدول های رابظه دار
            {
                await _context.SaveChangesAsync();
                return new ViewModel.ErorVM { State = true, AlertClass = "success", Message = "  نفش  با موفقیت ویرایش شد " };
            }
            catch (Exception ex)
            {
                return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "  نفش ویرایش نشد " + ex.Message };
            }
        }
        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ViewModel.ErorVM> Delete(int id)
        {
            Models.Roles? Role = await _context.Roles.Where(x => x.RoleId == id).FirstOrDefaultAsync();
            if (Role == null) { return new ViewModel.ErorVM() { State = false, AlertClass = "danger", Message = " نقشی با این مشخضات یافت نشد " }; }
          
            _context.Roles.Remove(Role);
            try// جدول های رابظه دار
            {
                await _context.SaveChangesAsync();
                return new ViewModel.ErorVM { State = true, AlertClass = "success", Message = " نقش  با موفقیت حذف شد " };
            }
            catch (Exception ex)
            {
                return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "نقش حذف نشد " + ex.Message };
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
