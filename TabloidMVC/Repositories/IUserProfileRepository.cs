using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        UserProfile GetById(int id);
        List <UserProfile> GetAllUserProfilesByStatus(int id);
        bool IsAdmin(UserProfile userProfile);
        void DeactivateUser(UserProfile user);
        void ReactivateUser(UserProfile user);
    }
}