using Microsoft.AspNetCore.Mvc;
using SisMot.Models;
using SisMot.Repositories;
using System.Security.Claims;

namespace SisMot.Controllers
{
    public class MotelController : Controller
    {
        private readonly IMotelRepository _motelRepository;

        public MotelController(IMotelRepository motelRepository) => _motelRepository = motelRepository;

        public IActionResult Index()
        {
            var getMotels = _motelRepository.GetAllMotels();
            return View(getMotels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewMotel = await _motelRepository.GetMotel(id);
            if (viewMotel != null)
            {
                return RedirectToAction("Details", "Motel");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var getMotel = await _motelRepository.GetMotel(id);
            return View(getMotel);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, Motel motel)
        {
            if (ModelState.IsValid)
            {
                var motelUpdated = await _motelRepository.UpdateMotel(id, motel);
                if (motelUpdated is not false)
                {
                    return RedirectToAction("Index", "Motel");
                }
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var motelDeleted = await _motelRepository.DeleteMotel(id);
            if (motelDeleted is not false)
            {
                return RedirectToAction("Index", "Motel");
            }
            return View();
        }

        public async Task<IActionResult> GetMotelsByOwner()
        {
            var ownerID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var motelByOwner = await _motelRepository.GetMotelsByOwner(int.Parse(ownerID));
            return View(motelByOwner);
        }


    }
}
