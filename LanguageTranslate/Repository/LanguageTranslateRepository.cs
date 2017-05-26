using LanguageTranslate.Data;
using LanguageTranslate.Data.DbModels;
using LanguageTranslate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageTranslate.Repository
{

    public class LanguageTranslateRepository
    {
        public LanguageTranslateRepository(LanguageTranslateContext ltContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
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
                CreateUserId = Guid.Parse(_userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id),
                LastUserEditId = Guid.Parse(_userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id),
                Text = grammatic.Text,
                Title = grammatic.Title,
                FromLanguage = grammatic.FromLanguage,
                ToLanguage = grammatic.ToLanguage
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
                CreateUserTitle = _userManager.FindByIdAsync(grammaticsDb.CreateUserId.ToString()).Result.UserName,
                LastUserEditTitle = _userManager.FindByIdAsync(grammaticsDb.LastUserEditId.ToString()).Result.UserName,
                IsValidate = grammaticsDb.IsValidate,
                IsEdit = grammaticsDb.IsEdit,
                FromLanguage = grammaticsDb.FromLanguage,
                ToLanguage = grammaticsDb.ToLanguage

            };
        }
        public IEnumerable<Grammatic> GetAll()
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
                LastUserEditTitle = _userManager.FindByIdAsync(s.LastUserEditId.ToString()).Result.UserName,
                IsValidate = s.IsValidate,
                IsEdit = s.IsEdit,
                FromLanguage = s.FromLanguage,
                ToLanguage = s.ToLanguage
            });
            return grammaticList;
        }
        public async Task<Grammatic> Update(Grammatic grammatic)
        {
            Grammatics grammaticDb = await _ltContext.Grammatics.FindAsync(grammatic.GrammaticId);
            if (grammaticDb.Text != grammatic.Text || grammaticDb.Title != grammatic.Title ||
                grammaticDb.FromLanguage != grammatic.FromLanguage || grammaticDb.ToLanguage != grammatic.ToLanguage
                )

            {
                grammaticDb.Text = grammatic.Text;
                grammaticDb.Title = grammatic.Title;
                grammaticDb.IsEdit = true;
                grammaticDb.LastUserEditId = Guid.Parse(_userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id);
                grammaticDb.LastDateEdit = DateTime.Now;
                grammaticDb.FromLanguage = grammatic.FromLanguage;
                grammaticDb.ToLanguage = grammatic.ToLanguage;

            }
            _ltContext.Entry(grammaticDb).State = EntityState.Modified;
            await _ltContext.SaveChangesAsync();
            return await FindAsync(grammaticDb.GrammaticId);
        }
        public async Task<Guid> VerificiedChanges(Guid id, bool isVerificied)
        {
            VerifiedGrammars verificiedGrammarDB = FindVerifiedGrammar(id);
            Grammatics grammaticsDb = _ltContext.Grammatics.Find(id);
            if (isVerificied)
            {
                if (grammaticsDb != null)
                {
                    if (verificiedGrammarDB == null)
                    {
                        string dir = Directory.GetDirectories(Directory.GetCurrentDirectory()).FirstOrDefault(s => s == "Grammatics");
                        if (dir == null)
                        {
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Grammatics");
                        }
                        string path = @Directory.GetCurrentDirectory() + "/Grammatics/" + grammaticsDb.Title + ".ATG";
                        File.AppendAllText(path, grammaticsDb.Text);
                        verificiedGrammarDB = new VerifiedGrammars
                        {
                            GrammaticId = id,
                            VerifiedGrammarId = Guid.NewGuid(),
                            LastDateEdit = grammaticsDb.LastDateEdit,
                            LastUserEditId = grammaticsDb.LastUserEditId,
                            Text = grammaticsDb.Text,
                            Title = grammaticsDb.Title,
                            Path = path,
                            FromLanguage = grammaticsDb.FromLanguage,
                            ToLanguage = grammaticsDb.ToLanguage
                        };
                        _ltContext.VerifiedGrammars.Add(verificiedGrammarDB);

                    }
                    else
                    {

                        string path = verificiedGrammarDB.Path;
                        File.WriteAllText(path, grammaticsDb.Text);
                        verificiedGrammarDB.Title = grammaticsDb.Title;
                        verificiedGrammarDB.Text = grammaticsDb.Text;
                        verificiedGrammarDB.LastDateEdit = grammaticsDb.LastDateEdit;
                        verificiedGrammarDB.LastUserEditId = grammaticsDb.LastUserEditId;
                        verificiedGrammarDB.ToLanguage = grammaticsDb.ToLanguage;
                        verificiedGrammarDB.FromLanguage = grammaticsDb.FromLanguage;

                        _ltContext.Entry(verificiedGrammarDB).State = EntityState.Modified;
                    }
                    grammaticsDb.IsValidate = true;
                    grammaticsDb.IsEdit = false;
                    _ltContext.Entry(grammaticsDb).State = EntityState.Modified;
                }

                else return id;
            }
            else
            {
                if (grammaticsDb != null)
                {
                    if (verificiedGrammarDB != null)
                    {
                        grammaticsDb.Title = verificiedGrammarDB.Title;
                        grammaticsDb.Text = verificiedGrammarDB.Text;
                        grammaticsDb.LastDateEdit = verificiedGrammarDB.LastDateEdit;
                        grammaticsDb.LastUserEditId = verificiedGrammarDB.LastUserEditId;
                        grammaticsDb.ToLanguage = verificiedGrammarDB.ToLanguage;
                        grammaticsDb.FromLanguage = verificiedGrammarDB.FromLanguage;
                        _ltContext.Entry(grammaticsDb).State = EntityState.Modified;
                    }
                }

            }
            await _ltContext.SaveChangesAsync();
            return id;
        }

        public VerifiedGrammars FindVerifiedGrammar(Guid grammaticId)
        {
            return _ltContext.VerifiedGrammars.FirstOrDefault(s => s.GrammaticId == grammaticId);

        }
        public async Task<Grammatic> GenerataFile(Guid grammaticId)
        {
            VerifiedGrammars vgDb = FindVerifiedGrammar(grammaticId);
            if (vgDb != null)
            {
                string path = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/Grammatics/").FirstOrDefault(s => s == vgDb.Title);
                if (path == null)
                    path = Directory.GetCurrentDirectory() + "/Grammatics/" + vgDb.Title;
                Directory.CreateDirectory(path);
                string strCmdText = "/C coco " + vgDb.Path + " -frames " + Directory.GetCurrentDirectory() + " -o " + path;
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = strCmdText
                };
                process.StartInfo = startInfo;
                process.Start();
                Grammatic grammatic = await FindAsync(grammaticId);
                grammatic.ResultGenerate = GenerateDLL(path, vgDb.Title);
                if (grammatic.ResultGenerate.ResultCode == 1)
                {
                    GeneratedDLLs generatedDLL = _ltContext.GeneratedDLLs.FirstOrDefault(s => s.GrammaticId == grammaticId);
                    if (generatedDLL != null)
                    {
                        generatedDLL.Image = grammatic.ResultGenerate.Result;
                        generatedDLL.FromLanguage = grammatic.FromLanguage;
                        generatedDLL.ToLanguage = grammatic.ToLanguage;
                        generatedDLL.Title = grammatic.Title;
                        _ltContext.Entry(generatedDLL).State = EntityState.Modified;
                    }
                    else
                    {
                        generatedDLL = new GeneratedDLLs()
                        {
                            GeneratedDLLId = Guid.NewGuid(),
                            GrammaticId = grammaticId,
                            Image = grammatic.ResultGenerate.Result,
                            Title = grammatic.Title,
                            FromLanguage = grammatic.FromLanguage,
                            ToLanguage = grammatic.ToLanguage
                        };
                        _ltContext.GeneratedDLLs.Add(generatedDLL);
                    }
                    await _ltContext.SaveChangesAsync();
                    CalculatePath();
                }
                return grammatic;
            }
            else return null;

        }
        public static ResultGenerate GenerateDLL(string path, string assemblyName)
        {
            var scanner = path + "/Scanner.cs";
            var parser = path + "/Parser.cs";
            var sourceScanner = File.ReadAllText(scanner);
            var sourceParser = File.ReadAllText(parser);
            var parsedSyntaxTreeScanner = Parse(sourceScanner, "", CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp7));
            var parsedSyntaxTreeParser = Parse(sourceParser, "", CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp7));
            using (MemoryStream ms = new MemoryStream())
            {
                var compilation
                = CSharpCompilation.Create(assemblyName, new SyntaxTree[] { parsedSyntaxTreeScanner, parsedSyntaxTreeParser }, DefaultReferences, DefaultCompilationOptions);
                try
                {
                    var result = compilation.Emit(ms);
                    ResultGenerate resultGenerate = new ResultGenerate()
                    {
                        ResultCode = result.Success ? 1 : 0
                    };
                    if (!result.Success)
                    {
                        foreach (var item in result.Diagnostics)
                        {
                            resultGenerate.ErrorsMessage.Add(item.ToString());
                        }
                    }
                    else
                        resultGenerate.Result = ms.ToArray();
                    return resultGenerate;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        private static readonly IEnumerable<string> DefaultNamespaces =
        new[]
        {
                    "System",
                    "System.IO",
                    "System.Net",
                    "System.Linq",
                    "System.Text",
                    "System.Text.RegularExpressions",
                    "System.Collections.Generic"
        };
        private static readonly IEnumerable<MetadataReference> DefaultReferences =
            new[]
            {
                   MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(Regex).GetTypeInfo().Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(WebResponse).GetTypeInfo().Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location),
            };
        private static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOverflowChecks(true).WithOptimizationLevel(OptimizationLevel.Release)
                    .WithUsings(DefaultNamespaces);
        public static SyntaxTree Parse(string text, string filename = "", CSharpParseOptions options = null)
        {
            var stringText = SourceText.From(text, Encoding.UTF8);
            return SyntaxFactory.ParseSyntaxTree(stringText, options, filename);
        }
        public async Task<Translate> TranslateCode(Translate translate)
        {
            List<PathReferences> references = _ltContext.PathReferences.Where(s => s.PathId == translate.SelectGrammatic).ToList();
            List<GeneratedDLLs> dlls = new List<GeneratedDLLs>();
            foreach (PathReferences item in references)
            {
                dlls.Add(_ltContext.GeneratedDLLs.First(s => s.GeneratedDLLId == item.GrammaticDllId));
            }
            string fromLanguage = translate.FromLanguage;
            translate.Grammatics = GetPaths()
                                  .Select(f =>
                                  new SelectListItem
                                  {
                                      Value = f.PathId.ToString(),
                                      Text = f.Title.ToString(),
                                      Selected = true
                                  })
                              .ToList();
            foreach (GeneratedDLLs dll in dlls)
            {
                var asl = new AssemblyLoader();
                Stream streamDLL = new MemoryStream(dll.Image);
                var asm = asl.LoadFromStream(streamDLL);
                var scanner = asm.GetType("Scanner");
                using (Stream s = GenerateStreamFromString(translate.FromLanguage))
                {
                    dynamic objScanner = Activator.CreateInstance(scanner, s);
                    var parser = asm.GetType("Parser");
                    dynamic objParser = Activator.CreateInstance(parser, objScanner);
                    var errorStr = asm.GetType("Errors");
                    dynamic objErrorStr = Activator.CreateInstance(errorStr);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (StreamWriter sw = new StreamWriter(ms))
                        {
                            objParser.fout = sw;
                            objParser.Parse(); // запустить анализ
                            translate.Result = new ResultGenerate()
                            {
                                ResultCode = objParser.errors.count > 0 ? 0 : 1
                            };
                            if (objParser.errors.count > 0)
                            {

                                translate.Result.ResultCode = 0;
                                translate.Result.ErrorsMessage = new List<string>
                                  {
                                    "число ошибок=" + objParser.errors.count
                                  };
                                foreach (var item in objParser.errors.errorStream)
                                {
                                    translate.Result.ErrorsMessage.Add(item);
                                }
                                translate.FromLanguage = fromLanguage;
                                return translate;
                            }
                            else
                            {

                                objParser.fout.Close();
                                using (MemoryStream msReOpen = new MemoryStream(ms.ToArray()))
                                {
                                    msReOpen.Position = 0;
                                    var sr = new StreamReader(msReOpen);
                                    translate.ToLanguage = sr.ReadToEnd();
                                }
                                translate.FromLanguage = translate.ToLanguage;
                            }

                        }
                    }
                }
            }
            translate.FromLanguage = fromLanguage;
            if (translate.Result.ResultCode == 1 && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                _ltContext.HistoryTranslates.Add(new HistoryTranslates
                {
                    HistoryTranslateId = Guid.NewGuid(),
                    FromLanguage = translate.FromLanguage,
                    ToLanguage = translate.ToLanguage,
                    DateTranslate = DateTime.Now,
                    GrammaticId = translate.SelectGrammatic,
                    UserId = Guid.Parse(_userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id)
                });
                await _ltContext.SaveChangesAsync();
            }
            return translate;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public List<HistoryTranslate> GetHistoryTranslates()
        {
            Guid userId = Guid.Parse(_userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result.Id);
            List<HistoryTranslates> history = _ltContext.HistoryTranslates.Where(s => s.UserId == userId).ToList();
            List<HistoryTranslate> historyTranslates = new List<HistoryTranslate>();
            if (history.Count > 0)
            {
                foreach (HistoryTranslates item in history)
                {
                    historyTranslates.Add(new HistoryTranslate
                    {
                        FromLanguage = item.FromLanguage,
                        HistoryTranslateId = item.HistoryTranslateId,
                        ToLanguage = item.ToLanguage,
                        TranslateDate = item.DateTranslate.ToString("dd.MM.yy  HH:mm"),
                        Grammatic = _ltContext.Paths.First(s=>s.PathId==item.GrammaticId).Title
                    });
                }
            }
            return historyTranslates;
        }
        public async Task<HistoryTranslate> FindTranslate(Guid id)
        {
            HistoryTranslates translate = await _ltContext.HistoryTranslates.FindAsync(id);
            if (translate != null)
                return new HistoryTranslate
                {
                    FromLanguage = translate.FromLanguage,
                    HistoryTranslateId = translate.HistoryTranslateId,
                    ToLanguage = translate.ToLanguage,
                    TranslateDate = translate.DateTranslate.ToString("dd.MM.yy  HH:mm"),
                    Grammatic = _ltContext.Paths.First(s => s.PathId == translate.GrammaticId).Title
                };
            return null;
        }
        public void CalculatePath()
        {
            _ltContext.PathReferences.RemoveRange(_ltContext.PathReferences.ToList());
            _ltContext.Paths.RemoveRange(_ltContext.Paths.ToList());
            Dictionary<string, List<string>> matrixDict = new Dictionary<string, List<string>>();
            List<GeneratedDLLs> dlls = _ltContext.GeneratedDLLs.ToList();
            foreach (GeneratedDLLs item in dlls)
            {
                if (!matrixDict.ContainsKey(item.FromLanguage))
                    matrixDict.Add(item.FromLanguage, new List<string>());
                if (!matrixDict.ContainsKey(item.ToLanguage))
                    matrixDict.Add(item.ToLanguage, new List<string>());
                matrixDict[item.FromLanguage].Add(item.ToLanguage);
            }
            List<string> languages = matrixDict.Keys.ToList();
            Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();
            foreach (var item in matrixDict)
            {
                g.Add(languages.IndexOf(item.Key), new List<int>());
                foreach (var value in item.Value)
                {

                    g[languages.IndexOf(item.Key)].Add(languages.IndexOf(value)); languages.IndexOf(value);
                }
            }


            for (int s = 0; s < languages.Count; s++)
            {
                Queue<int> q = new Queue<int>();
                int[] depth = new int[languages.Count], path = new int[languages.Count];
                bool[] used = new bool[languages.Count];

                q.Enqueue(s);
                used[s] = true;
                path[s] = -1;
                while (q.Count != 0)
                {
                    int v = q.Dequeue();
                    for (int i = 0; i < g[v].Count; i++)
                    {
                        int to = g[v][i];
                        if (!used[to])
                        {
                            used[to] = true;
                            q.Enqueue(to);
                            depth[to] = depth[v] + 1;
                            path[to] = v;
                        }
                    }
                }

                for (int k = 0; k < languages.Count; k++)
                {
                    int t0 = k;
                    if (used[t0])
                    {
                        List<int> recoveryPath = new List<int>();
                        recoveryPath.Add(t0);
                        while (path[t0] != -1)
                        {
                            t0 = path[t0];
                            recoveryPath.Add(t0);
                        }
                        recoveryPath.Reverse();
                        if (recoveryPath.Count > 1)
                        {
                            string pathTitle = "";
                            for (int i = 0; i < recoveryPath.Count; ++i)
                            {
                                pathTitle += "->";
                                pathTitle += languages[(recoveryPath[i])];
                            }
                            pathTitle = pathTitle.Substring(2);
                            Guid pathGuid = Guid.NewGuid();
                            _ltContext.Paths.Add(
                                new Paths
                                {
                                    PathId = pathGuid,
                                    Title = pathTitle
                                });
                            for (int i = 0; i < recoveryPath.Count - 1; i++)
                            {
                                _ltContext.PathReferences.Add(
                                    new PathReferences
                                    {
                                        Id = Guid.NewGuid(),
                                        PathId = pathGuid,
                                        GrammaticDllId = _ltContext.GeneratedDLLs.First(f => f.FromLanguage == languages[(recoveryPath[i])] && f.ToLanguage == languages[(recoveryPath[i + 1])]).GeneratedDLLId
                                    });
                            }
                        }
                    }
                }
            }
            _ltContext.SaveChangesAsync();
        }
        public List<PathTranslate> GetPaths()
        {
            List<PathTranslate> paths = new List<PathTranslate>();
            List<Paths> pathsDb = _ltContext.Paths.ToList();
            foreach (Paths item in pathsDb)
            {
                paths.Add(new PathTranslate
                {
                    PathId = item.PathId,
                    Title = item.Title,
                    DLLs = _ltContext.PathReferences.Where(s => s.PathId == item.PathId).Select(g => g.GrammaticDllId).ToList()
                });
            }
            return paths;
        }
    }
    public class AssemblyLoader : AssemblyLoadContext
    {
        // Not exactly sure about this
        protected override Assembly Load(AssemblyName assemblyName)
        {
            var dependecy = DependencyContext.Default;
            var res = dependecy.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            var assembly = Assembly.Load(new AssemblyName(res.First().Name));
            return assembly;
        }
    }
}
