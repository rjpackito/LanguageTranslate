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
           
            Guid grammaticGuid = await _ltManager.AddGrammatic(grammatic);
            return View("View", grammaticGuid);
        }
        public async Task<IActionResult> Details(string id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(Guid.Parse(id));
            if (grammatic == null)
            {
                return View("Error");
            }
            return View(grammatic);
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(Guid.Parse(id));
            if (grammatic == null)
            {
                return View("Error");
            }
            return View(grammatic);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Grammatic grammatic)
        {
            if (grammatic == null)
            {
                return View("Error");
            }
            return View(await _ltManager.Update(grammatic));
        }
    }
}
