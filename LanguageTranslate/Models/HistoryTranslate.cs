using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Models
{
    public class HistoryTranslate
    {
        public Guid HistoryTranslateId { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
        public string Grammatic { get; set; }
        public string TranslateDate { get; set; }
    }
}
