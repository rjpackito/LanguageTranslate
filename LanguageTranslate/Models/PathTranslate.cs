using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Models
{
    public class PathTranslate
    {
        public Guid PathId { get; set; }
        public string Title { get; set; }
        public List<Guid> DLLs { get; set; }
    }

}
