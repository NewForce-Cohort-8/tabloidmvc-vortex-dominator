using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class DeactivateViewModel
    {
        public List<UserProfile> Admins { get; set; }
        public UserProfile User { get; set; }
    }
}