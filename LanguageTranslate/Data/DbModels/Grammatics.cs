using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class Grammatics
    {
        [Key]
        public Guid GrammaticId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public DateTime EditDate { get; set; }
        public string Text { get; set; }
        public Guid CreateUserId { get; set; }
        public bool IsEdit { get; set; }
        public bool IsValidate { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}
