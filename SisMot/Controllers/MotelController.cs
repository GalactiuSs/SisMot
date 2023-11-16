using Microsoft.AspNetCore.Mvc;
using SisMot.Models;
using SisMot.Repositories;
using System.Security.Claims;
using SisMot.Models.CustomModels;

namespace SisMot.Controllers
{
    public class MotelController : Controller
    {
        private readonly IMotelRepository _motelRepository;
        private readonly IPhotoRepository _photoRepository;

        public MotelController(IMotelRepository motelRepository, IPhotoRepository photoRepository)
        {
            _motelRepository = motelRepository;
            _photoRepository = photoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var getMotels = await _motelRepository.GetMotelsWithPhotos();
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

        [HttpPost]
        public async Task<IActionResult> Edit(EditMotelDTO motelPhotosDto)
        {
            if(motelPhotosDto.MotelPhotos != null)
                await _photoRepository.LoadedImages(motelPhotosDto.MotelPhotos, motelPhotosDto.MotelId);
            var motelUpdated = await _motelRepository.UpdateMotel(motelPhotosDto);
            if (motelUpdated is not false)
            {
                return RedirectToAction("Index", "Motel");
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

        public async Task<IActionResult> RemoveImageMotel(string imageId)
        {
            var deleted = await _photoRepository.RemoveImage(imageId);
            if (deleted)
                return Ok();
            return NotFound();
        }
    }
}
