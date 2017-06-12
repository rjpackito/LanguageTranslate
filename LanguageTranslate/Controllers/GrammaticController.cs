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
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index() => View(await _ltManager.GetAll());
        [HttpPost]
        public async Task<IActionResult> Create(Grammatic grammatic)
        {

            Guid grammaticGuid = await _ltManager.AddGrammatic(grammatic);
            return View("Details", await _ltManager.FindAsync(grammaticGuid));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(id);
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
            return View("Edit", await _ltManager.Update(grammatic));
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Validate(string id)
        {
            Grammatic grammatic = await _ltManager.FindAsync(Guid.Parse(id));
            if (grammatic == null)
            {
                return View("Error");
            }
            ViewBag.Validate = true;
            return View("Details", grammatic);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Validate(string id, int isvalid)
        {
            Guid grammaticGuid = await _ltManager.VerificiedChanges(Guid.Parse(id), isvalid == 1);
            return RedirectToAction("Index", await _ltManager.GetAll());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GenerateFile(string id)
        {
            ViewBag.Validate = true;
            return View("Details", await _ltManager.GenerataFile(Guid.Parse(id)));
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _ltManager.Remove(Guid.Parse(id));
            return RedirectToAction("Index", await _ltManager.GetAll());
        }
        public async Task<IActionResult> CreateWithTransform()
        {
            GrammaticTransform grammaticTransform = new GrammaticTransform
            {
                Grammatics = (await _ltManager.GetAll())
                .Select(s =>
                new SelectListItem
                {
                    Value = s.GrammaticId.ToString(),
                    Text = s.Title.ToString(),
                    Selected = true
                })
                    .ToList()
            };
            return View(grammaticTransform);

        }
        [HttpPost]
        public async Task<IActionResult> CreateWithTransform(GrammaticTransform grammaticTransform)
        {
            var result = await _ltManager.AddGrammaticWithTransform(grammaticTransform);
            if (result is Grammatic)
            {
                Grammatic grammatic = result as Grammatic;
                return RedirectToAction("Details", new { id = grammatic.GrammaticId });
            }
            else
            {
                grammaticTransform = result as GrammaticTransform;
                grammaticTransform.Grammatics = (await _ltManager.GetAll())
                .Select(s =>
                new SelectListItem
                {
                    Value = s.GrammaticId.ToString(),
                    Text = s.Title.ToString(),
                    Selected = true
                })
                    .ToList();
                return View(grammaticTransform);
            }

        }
    }
}
