using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using System.Security.Principal;
using System.Web;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IUserTypeRepository _userTypeRepo;

        public UserProfileController(IUserProfileRepository userProfileRepository, IUserTypeRepository userTypeRepository)
        {
            _userProfileRepo = userProfileRepository;
            _userTypeRepo = userTypeRepository;
        }

        // GET: UserProfileController
        [Authorize]
        public ActionResult Index()
        {
            var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
            var activeUserProfiles = _userProfileRepo.GetAllUserProfilesByStatus(1);
            if (_userProfileRepo.IsAdmin(currentUser))
            {
                return View(activeUserProfiles);
            }
            else
            {
                return NotFound("Not authorized as admin");
            }
        }

        [Authorize]
        public ActionResult ViewDeactivated()
        {
            var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
            var deactivatedUserProfiles = _userProfileRepo.GetAllUserProfilesByStatus(2);
            if (_userProfileRepo.IsAdmin(currentUser))
            {
                return View(deactivatedUserProfiles);
            }
            else
            {
                return NotFound("Not authorized as admin");
            }
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
            var userProfile = _userProfileRepo.GetById(id);
            if (_userProfileRepo.IsAdmin(currentUser))
            {
                return View(userProfile);
            }
            else
            {
                return NotFound("Not authorized as admin");
            }
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {

            var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
            if (_userProfileRepo.IsAdmin(currentUser))
            {
                List<UserType> userTypes = _userTypeRepo.GetAll();
                UserProfile user = _userProfileRepo.GetById(id);
                List<UserProfile> admins = _userProfileRepo.GetAllAdmins();

                UserProfileFormViewModel vm = new UserProfileFormViewModel()
                {
                    User = user,
                    UserTypes = userTypes,
                    Admins = admins
                };
                if (user == null)
                {
                    return NotFound();
                }

                return View(vm);
            }
            else
            {
                return NotFound("Not authorized as admin");
            }
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile user)
        {
                
            try
            {
                _userProfileRepo.UpdateUser(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
            
        }

            // GET: UserProfileController/Deactivate/5
            public ActionResult Deactivate(int id)

            {
                var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
                if (_userProfileRepo.IsAdmin(currentUser))
                {
                List<UserProfile> admins = _userProfileRepo.GetAllAdmins();
                UserProfile user = _userProfileRepo.GetById(id);

                DeactivateViewModel vm = new DeactivateViewModel()
                {
                    User = user,
                    Admins = admins
                };
                if (user == null)
                    {
                        return NotFound();
                    }
                    return View(vm);
                } else
                {
                    return NotFound("Not authorized as admin");
                }
            }

            // POST: UserProfileController/Deactivate/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Deactivate(int id, UserProfile user)
            {
                var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
                if (_userProfileRepo.IsAdmin(currentUser))
                {
                    try
                    {

                        _userProfileRepo.DeactivateUser(user);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        return View(user);
                    }
                } else
                {
                    return NotFound("Not authorized as admin");
                }
            }

            // GET: UserProfileController/Reactivate/5
            public ActionResult Reactivate(int id)

            {
                var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
                UserProfile user = _userProfileRepo.GetById(id);
                if (_userProfileRepo.IsAdmin(currentUser))
                {
                    if (user == null)
                    {
                        return NotFound();
                    }
                    return View(user);
                }
                else
                {
                    return NotFound("Not authorized as admin");
                }
            }

            // POST: UserProfileController/Reactivate/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Reactivate(int id, UserProfile user)
            {
                var currentUser = _userProfileRepo.GetById(GetCurrentUserId());
                if (_userProfileRepo.IsAdmin(currentUser))
                {
                    try
                    {

                        _userProfileRepo.ReactivateUser(user);
                        return RedirectToAction("ViewDeactivated");
                    }
                    catch (Exception ex)
                    {
                        return View(user);
                    }
                }
                else
                {
                    return NotFound("Not authorized as admin");
                }
            }
            private int GetCurrentUserId()
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.Parse(userId);
            }

    }
}

