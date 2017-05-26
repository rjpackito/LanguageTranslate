using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class PathReferences
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GrammaticDllId { get; set; }
        public Guid PathId { get; set; }
    }
}
