using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Models;
using System.Diagnostics;

namespace SchoolWeb.Controllers;

// HomeController implements the base class Controller
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // Constructor registers de logger using DI (Dependency Inyection)
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /* 
      The IActionResult interface is an abstraction for multiple return types
      Index action method can return a view, can redirect to some action method or redirect a page, or much more.
      But if you want you can return a ViewResult class thus: public ViewResult Index()
    */
    public IActionResult Index()
    {
        /*
          The view is searched or mapped as: views\<controller name prefix>\<action method name>.cshtml
          In this case it would be ........: views\home\index.cshtml
        */
        return View();
    }

    // Privacy action method
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