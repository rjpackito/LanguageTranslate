using LanguageTranslate.Data;
using LanguageTranslate.Data.DbModels;
using LanguageTranslate.Models;
using LanguageTranslate.Models.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslate.Repository
{

    public class LanguageTranslateRepository
    {
        public LanguageTranslateRepository(LanguageTranslateContext ltContext,IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _ltContext = ltContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        private LanguageTranslateContext _ltContext;
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public async Task<Guid> AddGrammatic(Grammatic grammatic)
        {
            Guid grammaticGuid = Guid.NewGuid();
            Grammatics grammaticDb = new Grammatics()
            {
                GrammaticId = grammaticGuid,
                CreateDate = DateTime.Now,
                LastDateEdit = DateTime.Now,
                CreateUserId = grammatic.CreateUserId,
                LastUserEditId = grammatic.LastUserEditId,
                Text = grammatic.Text,
                Title = grammatic.Title
            };
            _ltContext.Grammatics.Add(grammaticDb);
            await _ltContext.SaveChangesAsync();
            return grammaticGuid;
        }

        public async Task<Grammatic> FindAsync(Guid grammaticGuid)
        {
            Grammatics grammaticsDb = await _ltContext.Grammatics.FindAsync(grammaticGuid);

            return new Grammatic
            {
                CreateDate = grammaticsDb.CreateDate,
                CreateUserId = grammaticsDb.CreateUserId,
                GrammaticId = grammaticsDb.GrammaticId,
                LastDateEdit = grammaticsDb.LastDateEdit,
                LastUserEditId = grammaticsDb.LastUserEditId,
                Text = grammaticsDb.Text,
                Title = grammaticsDb.Title,
                CreateUserTitle=_userManager.FindByIdAsync(grammaticsDb.CreateUserId.ToString()).Result.UserName,
                LastUserEditTitle= _userManager.FindByIdAsync(grammaticsDb.LastUserEditId.ToString()).Result.UserName
            };
        }
        public  IEnumerable<Grammatic> GetAll()
        {
            List<Grammatics> grammaticsDbList = _ltContext.Grammatics.ToList();
            var grammaticList = grammaticsDbList.Select(s => new Grammatic
            {
                CreateDate = s.CreateDate,
                CreateUserId = s.CreateUserId,
                GrammaticId = s.GrammaticId,
                LastDateEdit = s.LastDateEdit,
                LastUserEditId = s.LastUserEditId,
                Text = s.Text,
                Title = s.Title,
                CreateUserTitle = _userManager.FindByIdAsync(s.CreateUserId.ToString()).Result.UserName,
                LastUserEditTitle = _userManager.FindByIdAsync(s.LastUserEditId.ToString()).Result.UserName
            });
            return grammaticList;
        }
    }
}
