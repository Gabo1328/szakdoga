﻿using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MavAutoKozm.Controllers
{
    public class HomeController : Controller
    {
        //Elírás megelőzés
        private readonly string _felhasznaloId = "FelhasznaloId";
        private readonly MavAutoKozmDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MavAutoKozmDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Index()
        {
            var AspNetUserId = User.Claims.FirstOrDefault
             (x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var Felhasznalo = _context.AppUsers.FirstOrDefault(x => x.AspNetUserId == AspNetUserId);

            if (Felhasznalo != null)
                HttpContext.Session.SetInt32(_felhasznaloId, Felhasznalo.ID);
            else
                HttpContext.Session.Remove(_felhasznaloId);

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
}