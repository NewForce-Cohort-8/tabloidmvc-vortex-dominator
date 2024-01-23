using TabloidMVC.Models;
using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class UserProfileFormViewModel
    {
        public List<UserType> UserTypes { get; set; }
        public UserProfile User {  get; set; }
    }
}
