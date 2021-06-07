using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;

public class HomeController : Controller
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public HomeController()
    {
    }

    public HomeController(ILogger<HomeController> logger)
    {
        logger = logger;
        logger.LogDebug(1, "NLog injected into HomeController");
    }


    public IActionResult Index()
    {
        logger.Info("Hello, this is the index!");
        return View();
    }

    internal void Info(string v)
    {
        throw new NotImplementedException();
    }
}
