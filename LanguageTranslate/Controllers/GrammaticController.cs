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
    [Authorize(Roles = "Professor, Administrator")]
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
            return View("Details", grammaticGuid);
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
            return View("Edit",await _ltManager.Update(grammatic));
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Validate(string id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(Guid.Parse(id));
            if (grammatic == null)
            {
                return View("Error");
            }
            return View("Details", grammatic);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Validate(string id, int isvalid)
        {
            Guid grammaticGuid = await _ltManager.VerificiedChanges(Guid.Parse(id), isvalid == 1);
            return View("Index", _ltManager.GetAll());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GenerateFile(string id)
        {
            return View("Details", await _ltManager.GenerataFile(Guid.Parse(id)));
        }
    }
}
