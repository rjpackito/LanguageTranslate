using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Models
{
    public class Translate
    {
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
        public ResultGenerate Result { get; set; }
        public List<SelectListItem> Grammatics { get; set; }
        public Guid SelectGrammatic { get; set; }
    }
}
