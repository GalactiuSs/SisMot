using Microsoft.AspNetCore.Mvc;
using SisMot.Models;
using SisMot.Models.CustomModels;
using SisMot.Repositories;

namespace SisMot.Controllers;

public class RequestController : Controller
{
    private readonly IRequestMotelRepository _requestRepository;
    
    public RequestController(IRequestMotelRepository requestRepository) => _requestRepository = requestRepository;
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SendRequest()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendRequest(NewRequestDTO requestDto)
    {
        requestDto.RequestApplicationDate = DateTime.Today;
        requestDto.StatusRequest = 0;
        Motel motel = new()
        {
            Name = requestDto.MotelName,
            Description = requestDto.MotelDescription,
            Price = requestDto.MotelPrice,
            PhoneNumber = requestDto.MotelPhoneNumber,
            Latitude = requestDto.MotelLatitude,
            Longitude = requestDto.MotelLongitude
        };

        PersonRequest request = new()
        {
            StatusRequest = requestDto.StatusRequest,
            ApplicationDate = requestDto.RequestApplicationDate
        };
        var send = await _requestRepository.AddMotelWithRequest(motel, request);
        if (send is not false)
            return RedirectToAction("Index", "Motel");
        return View();
    }
}