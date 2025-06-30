using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DeviceRegistrationBS.Models;

namespace DeviceRegistrationBS.Controllers;

public class DeviceRegistrationController : Controller
{
    private readonly ILogger<DeviceRegistrationController> _logger;

    public DeviceRegistrationController(ILogger<DeviceRegistrationController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
