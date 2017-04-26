using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LanguageTranslate.Models;
using Microsoft.AspNetCore.Authorization;
using LanguageTranslate.Repository;

namespace LanguageTranslate.Controllers
{

    public class GrammaticController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        LanguageTranslateRepository _ltManager;
        public GrammaticController(UserManager<ApplicationUser> userManager, LanguageTranslateRepository ltManager)
        {
            _ltManager = ltManager;
            _userManager = userManager;
        }
        public IActionResult Create() => View();
        public IActionResult Index() => View(_ltManager.GetAll());
        [HttpPost]
        public async Task<IActionResult> Create(Grammatic grammatic)
        {
            grammatic.CreateUserId = Guid.Parse(_userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id);
            grammatic.LastUserEditId = grammatic.CreateUserId;
            Guid grammaticGuid = await _ltManager.AddGrammatic(grammatic);
            //grammatic = await _ltManager.FindAsync(grammaticGuid);
            return View("View", grammaticGuid);
        }
        //public IActionResult View(Grammatic grammatic) => View(grammatic);
        public async Task<IActionResult> Details(string id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(Guid.Parse(id));
            if (grammatic == null)
            {
                return View("Error");
            }
            return View(grammatic);
        }
    }
}
