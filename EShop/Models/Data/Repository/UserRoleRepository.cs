using Microsoft.EntityFrameworkCore;

namespace EShop.Models.Data.Repository
{
    public class UserRoleRepository
    {
        private readonly ShopContext _context;
        public UserRoleRepository(ShopContext _context)
        {
            this._context = _context; 
        }
        /// <summary>
        /// حذف نقش کاربران
        /// </summary>
        public async Task<ViewModel.ErorVM> Delete(int userid)
        {
            List<Models.UserRole> ? userRoles = await _context.UserRole.Where(x=>x.UserId == userid).ToListAsync();
            if (userRoles == null) return new ViewModel.ErorVM { State = true ,AlertClass= "warning", Message="این کاربر نقشی ندارد" };
            _context.UserRole.RemoveRange(userRoles);
            try // جدول های رابظه دار
            {
              await _context.SaveChangesAsync();
                return new ViewModel.ErorVM { State = true, AlertClass = "success", Message = "  نفش های کاربر با موفقیت حذف شد " };
            }
            catch (Exception ex)
            {
                return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "  نفش های کاربر حذف نشد " + ex.Message  };
            }
        }
        /// <summary>
        /// افزودن نقش کاربر
        /// </summary>
        public async Task<ViewModel.ErorVM> Add(int userid , List<int> roleid)
        {
            if(userid != null && roleid != null)
            {
                List<Models.UserRole> userRoles = new List<UserRole>();
                foreach(var item in roleid)
                {
                    Models.UserRole userRole = new UserRole() {UserId =userid ,RoleId = item};
                    userRoles.Add(userRole);    
                }
                if(userRoles !=null && userRoles.Count > 0)
                {
                    _context.UserRole.AddRange(userRoles);
                    try// جدول های رابظه دار
                    {
                        await _context.SaveChangesAsync();
                        return new ViewModel.ErorVM { State = true, AlertClass = "success", Message = "  نفش های کاربر با موفقیت اضافه شد " };
                    }
                    catch(Exception ex)
                    {
                        return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "  نفش های کاربر اضافه نشد " + ex.Message };

                    }
                }
                return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "  لیست نقش های کاربر خالی است " };
            }
            return new ViewModel.ErorVM { State = false, AlertClass = "danger", Message = "  شناسه کاربری یا شناسه نقش کاربر خالی است " };
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
