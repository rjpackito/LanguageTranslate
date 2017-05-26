using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Models
{
    public class ResultGenerate
    {
        public int ResultCode { get; set; }
        public List<string> ErrorsMessage { get; set; }
        public byte[] Result { get; set; }
    }
}
