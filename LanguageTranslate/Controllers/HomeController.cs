using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageTranslate.Repository;
using LanguageTranslate.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LanguageTranslate.Controllers
{
    public class HomeController : Controller
    {
        LanguageTranslateRepository _ltManager;

        public HomeController(LanguageTranslateRepository ltManager)
        {
            _ltManager = ltManager;
        }
        public IActionResult Index()
        {
            Translate translate = new Translate()
            {
               Grammatics  = _ltManager.GetPaths()
                .Select(s =>
                new SelectListItem
                {
                    Value = s.PathId.ToString(),
                    Text = s.Title.ToString(),
                    Selected=true
                })
                    .ToList()
            };
            return View(translate);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Translate translate)
        {
            translate =await  _ltManager.TranslateCode(translate);
            ModelState.Clear();
            return View(translate);
        }
        [Authorize]
        public IActionResult HistoryTranslate()
        {
            return View(_ltManager.GetHistoryTranslates());
        }
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            
            return View(await _ltManager.FindTranslate(Guid.Parse(id)));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
