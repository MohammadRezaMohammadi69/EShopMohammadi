using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Managment")]
    public class RoleController : Controller
    {
        private Models.Data.Repository.RolesRepository _rolerepository;
        public RoleController(Models.Data.Repository.RolesRepository _rolerepository)
        {
            this._rolerepository = _rolerepository;
        }
        /// <summary>
        /// لیست همه نقش ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Models.Roles> roles = await _rolerepository.RoleList();
            return View(roles);
        }
        /// <summary>
        /// نمایش ویرایش نقش ها 
        /// </summary>
        /// <returns></returns>
        //public async Task<IActionResult> EditRole(int id)
        //{
        //    if(id == 0)
        //    {
        //        TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "شناسه نقش خالیست", AlertClass = "danger" });
        //        return RedirectToAction("Index");
        //    }
        //    List<Models.Roles> roles = await _rolerepository.RoleList();
        //    if(roles == null)
        //    {
        //        TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "لیست نقش ها خالیست", AlertClass = "danger" });
        //        return RedirectToAction("Index");
        //    }
        //    Models.Roles? role = roles.Where(x => x.RoleId == id).FirstOrDefault();
        //    if (role == null)
        //    {
        //        TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "نقشی یاقت نشد ", AlertClass = "danger" });
        //        return RedirectToAction("Index");
        //    }
        //    return View(role);
        //}
        /// <summary>
        ///  ویرایش نقش ها 
        /// </summary>
        /// <returns></returns>
        //public async Task<IActionResult> UpdateRole(Models.Roles role)
        //{
        //    if(role == null)
        //    {
        //        TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " نقش خالیست", AlertClass = "danger" });
        //        return RedirectToAction("Index");
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "لطفا اطلاعات را به درستی وارد کنید", AlertClass = "danger" });
        //        return RedirectToAction("Index");
        //    }
        //    ViewModel.ErorVM EditRole = await _rolerepository.Update(role);
        //    TempData["message"] = await Utilities.JSON.ToJson(EditRole);
        //    return RedirectToAction("Index");
        //}
        /// <summary>
        ///  ویرایش نقش ها 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DeleteRole(int id)      
        {
            if (id == 0)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " شناسه نقش خالیست", AlertClass = "danger" });
                return RedirectToAction("Index");
            }
            ViewModel.ErorVM DeleteRole = await _rolerepository.Delete(id);
            TempData["message"] = await Utilities.JSON.ToJson(DeleteRole);
            return RedirectToAction("Index");
        }
    }
}
