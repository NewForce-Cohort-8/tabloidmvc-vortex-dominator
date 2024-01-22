using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime CreateDateTime { get; set; }
        public string ImageLocation { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string GetImage()
        {
            if (ImageLocation is not null)
            {
                return ImageLocation;
            }
            else
            {
                return "https://upload.wikimedia.org/wikipedia/commons/2/2c/Default_pfp.svg";
            }
        }
    }
}
