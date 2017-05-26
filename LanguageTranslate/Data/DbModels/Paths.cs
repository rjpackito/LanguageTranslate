using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Data.DbModels
{
    public class Paths
    { 
        [Key]
        public Guid PathId { get; set; }
        public string Title { get; set; }
    }
}
