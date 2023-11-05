using Microsoft.AspNetCore.Mvc;
using SisMot.Models;
using SisMot.Models.CustomModels;
using SisMot.Repositories;

namespace SisMot.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccess _access;

        public AccessController(IAccess iaccess) => _access = iaccess;

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var login = await _access.Login(loginDTO);
            if (login is true)
                return RedirectToAction("Index", "Motel");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            Person person = new Person() 
            {
                FirstName = userDTO.firstName, 
                LastName = userDTO.lastName,
                SecondLastName = userDTO.secondLastName,
                PhoneNumber = userDTO.phoneNumber,
                Ci = userDTO.ci,
                Email = userDTO.email,
            };

            User user = new User()
            {
                Password = userDTO.password,
                Role = "Owner"
            };
            var register = await _access.CreatePerson(person, user, userDTO.confirmPassword);
            if (register is true)
                return RedirectToAction("Login","Access");
            return View();
        }

        public async Task<IActionResult> PasswordRecovery(string email)
        {
            var userId = await _access.RecoveryPassword(email);
            if (userId > 0)
            {
                return RedirectToAction("ChangePassword", "Access", new { id = userId });
            }
            return View();
        }

        [Route("ChangePassword/{id:int}")]
        public IActionResult ChangePassword([FromRoute] int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostPassword(RecoveryDTO recovery)
        {
            var changePassword = await _access.ChangePassword(recovery.password, recovery.confirmPassword, recovery.codeRecovery, recovery.userId);
            if (changePassword is true)
                return RedirectToAction("Login", "Access");
            return View();
        }
    }
}
