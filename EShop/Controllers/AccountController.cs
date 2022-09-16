
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly Models.Data.Repository.UsersRepository _usersRepository;
		public AccountController(Models.Data.Repository.UsersRepository _usersRepository)
		{
            this._usersRepository = _usersRepository;   
		}
        /// <summary>
        /// نمایش صفحه ثبت نام 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        /// <summary>
        /// دریافت اطلاعات کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DoRegister(Models.Users user)
		{
             Models.Users ? validation =await _usersRepository.FindUser(user.UserName);
            if(validation != null)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "گاربر تکراری ", AlertClass = "info" });
                return View("Register", user);
            }
			if (!ModelState.IsValid)
			{
				TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() {State=false, Message = "لطفا مقادیر صحیح وارد کنید", AlertClass = "Warning" });
				return View("Register", user);
			}
			user.Name = await Utilities.StringClassConverter.GetPersianWord(user.Name);
			user.UserName = await Utilities.StringClassConverter.GetEnglishNumber(user.UserName);
			user.Password = await Utilities.StringClassConverter.GetEnglishNumber(user.Password);
			Utilities.AESEncryption Aes = new Utilities.AESEncryption(GlobalClass.AppSetting.EncKey);
			user.Password = Aes.EncryptData(user.Password);
			user.Email = user.Email == null ? "" : await Utilities.StringClassConverter.GetEnglishNumber(user.Email);
			user.Age = user.Age.HasValue ? Convert.ToInt32(await Utilities.StringClassConverter.GetEnglishNumber(user.Age.ToString())) : 0;
			user.CodePosti = user.CodePosti == null ? "" : await Utilities.StringClassConverter.GetEnglishNumber(user.CodePosti);
			user.Address = await Utilities.StringClassConverter.GetPersianWord(user.Address);
			bool useradded = await _usersRepository.New(user);
			if (!useradded)
			{
				TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " کاربر جدید ثبت نشد ", AlertClass = "danger" });

				return View("Register", user);
			}
			TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " کاربر جدید با موفقیت ثبت شد ", AlertClass = "success" });
			return RedirectToAction("Register");
		}
        /// <summary>
        /// نمایش صفحه لاگین
        /// </summary>
        /// <returns></returns>
		[HttpGet]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        /// <summary>
        /// چک کردن لاگین کاربر 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DoLogin(ViewModel.LoginVM loginuser)
        {
            if (string.IsNullOrEmpty(loginuser.UserName) || string.IsNullOrEmpty(loginuser.Password))
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "لطفا مقادیر صحیح وارد کنید", AlertClass = "Warning" });
                return RedirectToAction("Login");
            }
            loginuser.UserName = await Utilities.StringClassConverter.GetEnglishNumber(loginuser.UserName);
            loginuser.Password = await Utilities.StringClassConverter.GetEnglishNumber(loginuser.Password);
            Models.Users? findeduser = await _usersRepository.FindUser(loginuser.UserName);
            if (findeduser == null)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " نام کاربری یا رمز عبور اشتباه میباشد ", AlertClass = "danger" });
                return RedirectToAction("Login");
            }
            Utilities.AESEncryption Aes = new Utilities.AESEncryption(GlobalClass.AppSetting.EncKey);
            var decpass = Aes.DecryptData(findeduser.Password);
            if (decpass == null)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " خطا در بررسی رمز عبور ", AlertClass = "Warning" });
                return RedirectToAction("Login");
            }
            if (decpass != loginuser.Password)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " رمز عبور اشتباه است ", AlertClass = "danger" });
                return RedirectToAction("Login");
            }
            if (!findeduser.Active)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " کاربری شما غیر فعال است ", AlertClass = "Warning" });
                return RedirectToAction("Login");
            }
            //لیست نقش کاربران
            List<string> rolenames = new List<string>();
            if (findeduser.UserRole.Count > 0)
            {
                foreach (var item in findeduser.UserRole)
                {
                    rolenames.Add(item.Roles.RoleName);
                }
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, loginuser.UserName));
            if (rolenames != null && rolenames.Count > 0)
            {
                foreach (var role in rolenames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = loginuser.RememberMe });
            if (rolenames != null && rolenames.Contains("Admin"))
            {//مدیر
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
        /// <summary>
        /// خروح از حساب کاربری
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Rigester2()
        {
            return View();
        }
        /// <summary>
        /// کاربر تکراری 
        /// </summary>
        /// <param name = "username" ></ param >
        /// < returns ></ returns >
        public async Task<JsonResult> IsUserExist(string UserName)
        {
            Models.Users? validation = await _usersRepository.FindUser(UserName);
            if (validation != null)
            {   //کاربر تکراری
                return Json(false);
            }
            else
            {  //کاربر جدید
                return Json(true);
            }
        }
    }
}
