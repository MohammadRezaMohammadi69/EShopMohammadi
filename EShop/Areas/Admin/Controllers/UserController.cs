using EShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModel;

namespace EShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly EShop.Models.Data.Repository.UsersRepository _usersrepository;
		private readonly EShop.Models.Data.Repository.RolesRepository _rolesrepository;
		private readonly EShop.Models.Data.Repository.UserRoleRepository _userrolesrepository;
	

		public UserController(Models.Data.Repository.UsersRepository _usersrepository, Models.Data.Repository.RolesRepository _rolesrepository , Models.Data.Repository.UserRoleRepository _userrolesrepository)
		{
			this._usersrepository = _usersrepository;
			this._rolesrepository = _rolesrepository;
		    this._userrolesrepository = _userrolesrepository;
		}
		/// <summary>
		/// لیست همه کاربران
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Index(int UserId =0 , string Name = "")
		{
			List<Users> users = await _usersrepository.UserList(UserId ,Name );
			return View(users);
		}
		/// <summary>
		///  نمایش ویرایش کاربر
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
	    [HttpGet]
		public async Task<IActionResult> EditUser(int id)
		{
			if (id == 0)
			{
				TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "شناسه کاربری خالیست", AlertClass = "danger" });
				return RedirectToAction("Index");
			}
            Users user = await _usersrepository.FindUser(id);
			EditeUserVM EditeUserVM = new EditeUserVM();
			if (user != null)
            {
				Utilities.AESEncryption aES = new Utilities.AESEncryption(GlobalClass.AppSetting.EncKey);
				user.Password = aES.DecryptData(user.Password);
				user.ConfrimePassword = user.Password;
				EditeUserVM.Users = user;
            }
		    List<Roles> roles = await _rolesrepository.RoleList();
			if (roles != null)
            {
				ViewBag.AllRoles = roles;
			}
			return View(EditeUserVM);
		}
		/// <summary>
		///   ویرایش کاربر
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
		/// 
	    [HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> UpdateUser(EditeUserVM editeuser)
        {
			if(editeuser == null)
			{
				TempData["message"] = await Utilities.JSON.ToJson(new ErorVM() { State = false, Message = " کاربری انتخاب نشده است", AlertClass = "danger" });
				return RedirectToAction("EditUser" , new { id = editeuser.Users.UserId });
			}
			if (!ModelState.IsValid)
			{
				TempData["message"] = await Utilities.JSON.ToJson(new ErorVM() { State = false, Message = "لطفا اطلاعات را به درستی وارد کنید", AlertClass = "danger" });
				return RedirectToAction("EditUser", new { id = editeuser.Users.UserId });
			}
			editeuser.Users.Name = await Utilities.StringClassConverter.GetPersianWord(editeuser.Users.Name);
			editeuser.Users.Password = await Utilities.StringClassConverter.GetEnglishNumber(editeuser.Users.Password);
			Utilities.AESEncryption Aes = new Utilities.AESEncryption(GlobalClass.AppSetting.EncKey);
			editeuser.Users.Password = Aes.EncryptData(editeuser.Users.Password);
			editeuser.Users.Password = editeuser.Users.Password == null ? "" : await Utilities.StringClassConverter.GetEnglishNumber(editeuser.Users.Password);
			editeuser.Users.Age = editeuser.Users.Age.HasValue ? Convert.ToInt32(await Utilities.StringClassConverter.GetEnglishNumber(editeuser.Users.Age.ToString())) : 0;
			editeuser.Users.CodePosti = editeuser.Users.CodePosti == null ? "" : await Utilities.StringClassConverter.GetEnglishNumber(editeuser.Users.CodePosti);
			editeuser.Users.Address = await Utilities.StringClassConverter.GetPersianWord(editeuser.Users.Address);
			ErorVM UserEror = await _usersrepository.Update(editeuser.Users);
			if (UserEror.State == false)
			{
				TempData["message"] = await Utilities.JSON.ToJson(UserEror);
				return RedirectToAction("EditUser", new { id = editeuser.Users.UserId });
			}
			ErorVM DeleteUserRole = await _userrolesrepository.Delete(editeuser.Users.UserId);
			if(DeleteUserRole.State == false)
            {
			TempData["message"] = await Utilities.JSON.ToJson(DeleteUserRole);
			return RedirectToAction("EditUser", new { id = editeuser.Users.UserId });
			}
			if(editeuser.UserRoleId != null && editeuser.UserRoleId.Count() > 0)
            {
				ErorVM AddUserRole = await _userrolesrepository.Add(editeuser.Users.UserId, editeuser.UserRoleId);
				if (AddUserRole.State == false)
				{
					TempData["message"] = await Utilities.JSON.ToJson(AddUserRole);
					return RedirectToAction("EditUser", new { id = editeuser.Users.UserId });
				}
				editeuser.UserRoleId.Clear();
			}
			TempData["message"] = await Utilities.JSON.ToJson(new ErorVM() { State = true, Message = " کاربر با موفقیت ویرایش شد ", AlertClass = "success" });
			return RedirectToAction("index");
        }
		/// <summary>
		/// حذف کاربر
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
	    public async Task<IActionResult> DeleteUser(int id)
        {
			if(id == 0)
            {
				TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "شناسه کاربری خالیست", AlertClass = "danger" });
				return RedirectToAction("Index");
			}
			ErorVM DeleteUser = await _usersrepository.Delete(id);
			TempData["message"] = await Utilities.JSON.ToJson(DeleteUser);
		    return RedirectToAction("Index");
        }

	}
}
