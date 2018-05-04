using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.WebUI.Store;
using Newtonsoft.Json;

namespace WebApp.WebUI.Controllers
{
    /// <summary>
    /// Дефолтный контроллер
    /// </summary>
    [Route("api")]
    public class ValuesController : Controller
    {

        private static readonly Dictionary<string, User> RegistrationStore = new Dictionary<string, User>();

        public static CandidateStore CandidateStore = new CandidateStore();

        public static VoteStore VotesStore = new VoteStore(CandidateStore.Candidates);

        public static Boolean IsVotingFinished = false;

        /// <summary>
        /// Регистрация избирателя по имени (получение уникального ID)        
        /// </summary>
        /// <returns></returns>

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromQuery] string username)
        {
            if (username.Equals("")) return BadRequest("Введите имя");

            if (RegistrationStore.Count(i => i.Value.Name.Equals(username)) != 0)
            {
                return BadRequest("Вы уже зарегистрированы");
            }
            else
            {
                string guid = RegistrationStore.Count.ToString();
                RegistrationStore.Add(guid, new User { Guid = guid, Name = username });
                return Ok(guid);
            }
        }

        /// <summary>
        /// Запрос бюллетеня (вариантов голосов)
        /// </summary>
        /// <returns></returns>
        [Route("getCandidates")]
        [HttpGet]
        public List<User> GetCandidates()
        {
            return CandidateStore.Candidates;
        }

        /// <summary>
        /// Отправка выбора избирателя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [Route("vote")]
        [HttpPost]
        public IActionResult Vote([FromQuery] string guid, [FromQuery] string candidateGuid)
        {
            if (IsVotingFinished)
            {
                return BadRequest("Голосование завершено");
            }

            
            if (!RegistrationStore.ContainsKey(guid))
            {
                return BadRequest("Вы не зарегистрированы");
            }

            User user = RegistrationStore[guid];

            if (user.IsVoted)
            {
                return BadRequest("Вы уже проголосовали");
            }
            
            if (CandidateStore.Candidates.Count(i => i.Guid.Equals(candidateGuid)) == 0)
            {
                return BadRequest("Указанный кандидат не найден");
            }

            VotesStore.AddVote(user, candidateGuid);

            return Ok("Голос принят. Спасибо!");
        }

        /// <summary>
        /// Завершение голосования
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [Route("finishVoting")]
        [HttpGet]
        public IActionResult FinishVoting()
        {
            IsVotingFinished = true;
            return Ok("Голосование завершено");
        }

        /// <summary>
        /// Запрос результатов
        /// </summary>
        /// <returns></returns>
        [Route("getResult")]
        [HttpGet]
        public string GetVotes()
        {
            return JsonConvert.SerializeObject(VotesStore.GetVotes(), Formatting.Indented);
        }
    }
}
