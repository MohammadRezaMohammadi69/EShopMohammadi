using EShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModel
{
	public class EditeUserVM
	{
        public Users? Users { get; set; }
  
        public List<int>? UserRoleId { get; set; }   
    }
}
