using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        [DisplayName("User Status")]
        public string Name { get; set; }

    }
}
