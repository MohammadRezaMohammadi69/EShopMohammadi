using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly Models.Data.Repository.UsersRepository _usersRepository;
        public HomeController(Models.Data.Repository.UsersRepository _usersRepository)
        {
           this._usersRepository = _usersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // public async Task<IActionResult> Users()
        //{
        //    //List<Users> Model=  await _usersRepository.UserList();
        //    //return View(Model);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            return View();
        }
    }
}