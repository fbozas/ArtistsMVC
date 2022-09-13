using ArtistsMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtistsMVC.Controllers
{
    public class CustomRoleManagementController : Controller
    {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;

        public CustomRoleManagementController()
        {
            _context = new ApplicationDbContext();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        public ActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            _roleManager.Create(role);
            return RedirectToAction("Index", "CustomRoleManagement");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}