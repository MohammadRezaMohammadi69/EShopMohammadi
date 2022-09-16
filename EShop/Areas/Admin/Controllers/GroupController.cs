using Microsoft.AspNetCore.Mvc;
 
namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        private readonly Models.Data.Repository.GroupRepository _repository;
        public GroupController(Models.Data.Repository.GroupRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<IActionResult> Index()
        {
            List<Models.Groups> Groups = await _repository.GroupList();
            if (Groups == null)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = " لیست دسته بندی ها خالیست", AlertClass = "danger" });
                return View();
            }

            List<ViewModel.TreeViewVM> TreeViewNodes = new List<ViewModel.TreeViewVM>();
            foreach (var group in Groups)
            {
                TreeViewNodes.Add(new ViewModel.TreeViewVM()
                {
                    id = group.Id.ToString(),
                    parent = group.ParentId == 0 ? "#" : group.ParentId.ToString(),
                    text = group.Name.ToString()
                });
            }
            if( Utilities.JSON.ToJson(TreeViewNodes).Result !=null && Utilities.JSON.ToJson(TreeViewNodes).Result.Count() > 0)
            {
                ViewBag.JsonTreeData = await Utilities.JSON.ToJson(TreeViewNodes);
            }
            return View();
 
        }
        /// <summary>
        /// گروه جدید
        /// </summary>
        /// <param name="NewGroup"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> New(Models.Groups NewGroup)
		{
            List<Models.Groups> Groups = await _repository.GroupList();
            if (NewGroup == null)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "   گروه جدید خالیست", AlertClass = "danger" });
                return RedirectToAction("Index");
            }
            if (NewGroup.Name == null)
			{
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "  نام گروه جدید خالیست", AlertClass = "danger" });
                return RedirectToAction("Index");
            }
            if (NewGroup.ParentId == 0 && Groups.Count() > 0)
            {
                TempData["message"] = await Utilities.JSON.ToJson(new ViewModel.ErorVM() { State = false, Message = "  نام سرشاخه خالیست ", AlertClass = "danger" });
                return RedirectToAction("Index");
            }
            if (NewGroup.ParentId == 0 && Groups.Count() == 0)
            {// اضافه کردن یه ریشه (parentid = 0)
                NewGroup.ParentId = 0;
            }
            ViewModel.ErorVM Eror = await _repository.New(NewGroup);
            TempData["message"] = await Utilities.JSON.ToJson(Eror);
            return RedirectToAction("Index");
        }
     
    }
}
