using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class HistoryTranslates
    {
        [Key]
        public Guid HistoryTranslateId { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
        public Guid UserId { get; set; }
        public string Сonveyor { get; set; }
        public DateTime DateTranslate { get; set; }
    }
}
