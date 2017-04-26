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
        public DateTime LastDateEdit { get; set; }
        [DisplayName("Текст грамматики")]
        public string Text { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid LastUserEditId { get; set; }
        [DisplayName("Создал")]
        public string CreateUserTitle { get; set; }
        [DisplayName("Отредактировал")]
        public string LastUserEditTitle { get; set; }
    }
}
