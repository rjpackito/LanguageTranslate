using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Models
{
    public class Grammatic
    {
        public Guid GrammaticId { get; set; }
        [DisplayName("Дата создания")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Название")]
        public string Title { get; set; }
        [DisplayName("Последняя дата редактирования")]
        public DateTime EditDate { get; set; }
        [DisplayName("Текст грамматики")]
        public string Text { get; set; }
        public Guid CreateUserId { get; set; }
        [DisplayName("Создал")]
        public string CreateUserTitle { get; set; }
        public bool IsEdit { get; set; }
        public bool IsValidate { get; set; }
        public ResultGenerate ResultGenerate { get; set; }
        [DisplayName("Исходный ЯП")]
        public string FromLanguage { get; set; }
        [DisplayName("Целевой ЯП")]
        public string ToLanguage { get; set; }
    }
    public class GrammaticTransform
    {
        public List<SelectListItem> Grammatics { get; set; }
        public ResultGenerate Result { get; set; }
        public Guid SelectGrammatic { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        [DisplayName("Исходный ЯП")]
        public string FromLanguage { get; set; }
        [DisplayName("Целевой ЯП")]
        public string ToLanguage { get; set; }
    }
}
