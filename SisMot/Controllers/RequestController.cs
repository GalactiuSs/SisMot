using Microsoft.AspNetCore.Mvc;
using SisMot.Helpers;
using SisMot.Models;
using SisMot.Models.CustomModels;
using SisMot.Repositories;

namespace SisMot.Controllers;

public class RequestController : Controller
{
    private readonly IRequestMotelRepository _requestRepository;
    private readonly IRequestRepository _requestSingleRepository;
    private readonly BingMap bingMap;

    public RequestController(IRequestMotelRepository requestRepository, BingMap bingMap, IRequestRepository requestSingleRepository)
    {
        _requestRepository = requestRepository;
        this.bingMap = bingMap;
        _requestSingleRepository = requestSingleRepository;
    }

    
    // GET
    public async Task<IActionResult> Index()
    {
        var getRequest = await _requestSingleRepository.GetAllRequest();
        return View(getRequest);
    }

    public IActionResult SendRequest()
    {
        ViewBag.ApiKey = bingMap.Key;
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
        var send = await _requestRepository.AddMotelWithRequest(motel, request, requestDto.Photos);
        if (send is not false)
            return RedirectToAction("Index", "Motel");
        return View();
    }
}