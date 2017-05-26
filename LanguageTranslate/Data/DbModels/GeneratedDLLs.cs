using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class GeneratedDLLs
    {
        [Key]
        public Guid GeneratedDLLId { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public Guid GrammaticId { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}
