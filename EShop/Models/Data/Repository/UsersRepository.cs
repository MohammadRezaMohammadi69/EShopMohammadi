using Microsoft.EntityFrameworkCore;

namespace EShop.Models.Data.Repository
{
    public class UsersRepository :Interface.IUsersRepository
    {
        private EShop.Models.Data.ShopContext _Context;
        public UsersRepository(ShopContext _Context)
        {
            this._Context = _Context;
        }
        /// <summary>
        /// لیست کاربران
        /// </summary>
        /// <returns></returns>
        public async Task<List<Models.Users>> UserList(int UserId , string Name )
        {
            IQueryable<Models.Users> ? condition = _Context.Users.Include(x => x.UserRole).ThenInclude(x => x.Roles);
            if(UserId != 0)
            {
                condition = condition.Where(x => x.UserId == UserId);
            }
            if(!String.IsNullOrEmpty(Name))
            {
                condition = condition.Where(x=>x.Name.Contains(Name));  
            }
            return await condition.ToListAsync();
        }
        /// <summary>
        /// کاربر جدید
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> New(Models.Users user)
		{
            user.CreateDate = DateTime.Now;
            user.ShowCreateDate = Utilities.PersianDateTime.GetDateTime();
            if (user == null)
			{
                return false;
			}
            await  _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
               return true;
        }
       public async Task<Users> FindUser(string username )
        {
           return  await _Context.Users.Where(x=>x.UserName == username ).Include(x=>x.UserRole).ThenInclude(x=>x.Roles).FirstOrDefaultAsync();
       
        }
        public async Task<Users> FindUser(int userid)
        {
            return await _Context.Users.Where(x => x.UserId == userid).Include(x => x.UserRole).ThenInclude(x => x.Roles).FirstOrDefaultAsync();

        }
        public async Task<ViewModel.ErorVM> Update(Models.Users user)
        {
            Models.Users OldUser = await _Context.Users.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();
            if (OldUser == null)
            {
                return new ViewModel.ErorVM { State = false, Message = " کاربری با این مشخصات یافت نشد  ", AlertClass = "danger" };
            }
            OldUser.Name = user.Name;
            OldUser.Password = user.Password;
            OldUser.Email = user.Email;
            OldUser.Age = user.Age;
            OldUser.CodePosti = user.CodePosti;
            OldUser.Active = user.Active;
            OldUser.Address = user.Address;
            OldUser.EditDate = DateTime.Now;
            OldUser.ShowEditDate = Utilities.PersianDateTime.GetDateTime();
            _Context.Users.Update(OldUser);
            try
            { // در جداول دارای رابطه مجبوریم 
                await _Context.SaveChangesAsync();
                return new ViewModel.ErorVM { State = true, Message = " کاربر با موفقیت ویرایش شد ", AlertClass = "Success" };
            }
            catch 
            {
                return new ViewModel.ErorVM { State = false, Message = " کاربر ویرایش نشد  ", AlertClass = "danger" };
            }
        }
        public async Task<ViewModel.ErorVM> Delete(int id)
        {
            if(id == 0)
            {
                return new ViewModel.ErorVM() { State = false, AlertClass = "danger", Message = " شناسه کاربری خالیست" };
            }
            Models.Users ? user = await FindUser(id);
            if(user == null)
            {
                return new ViewModel.ErorVM() { State = false, AlertClass = "danger", Message = " کاربری با این مشخصات یافت نشد  " };
            }
            _Context.Users.Remove(user);
            try // در جداول دارای رابطه مجبوریم 
            {
                await _Context.SaveChangesAsync();
                return new ViewModel.ErorVM() { State = true, AlertClass = "success", Message = " کاربر با موفقیت حذف شد  " };
            }
            catch(Exception ex)
            {
                return new ViewModel.ErorVM() { State = false, AlertClass = "danger", Message = " کاربر حذف تشد  " + ex.Message };
            }
        }
        public void Dispose()
        {
            _Context.Dispose();
        }

      
    }
}
