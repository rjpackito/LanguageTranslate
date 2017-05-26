using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class VerifiedGrammars
    {
        [Key]
        public Guid VerifiedGrammarId { get; set; }
        public string Title { get; set; }
        public DateTime LastDateEdit { get; set; }
        public string Text { get; set; }
        public Guid LastUserEditId { get; set; }
        public string Path { get; set; }
        public Guid GrammaticId { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}
