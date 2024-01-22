using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        UserProfile GetById(int id);
        List <UserProfile> GetAllUserProfiles();
        bool IsAdmin(UserProfile userProfile);
    }
}