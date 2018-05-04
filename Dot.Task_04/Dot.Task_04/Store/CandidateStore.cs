using System.Collections.Generic;
using WebApp.WebUI.Models;

namespace WebApp.WebUI.Store
{
    public class CandidateStore
    {
        /// <summary>
        /// Кандидаты в президенты 2018
        /// </summary>
        public List<User> Candidates { get; } = new List<User>
        {
            new User {Guid = "0", Name = "Владимир Путин"},
            new User {Guid = "1", Name = "Павел Грудинин"},
            new User {Guid = "2", Name = "Владимир Жириновский"},
            new User {Guid = "3", Name = "Ксения Собчак"},
            new User {Guid = "4", Name = "Григорий Явлинский"},
            new User {Guid = "5", Name = "Борис Титов"}
        };
    }
}
