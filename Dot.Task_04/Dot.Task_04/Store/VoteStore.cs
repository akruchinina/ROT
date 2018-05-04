using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.WebUI.Models;
using Newtonsoft.Json;


namespace WebApp.WebUI.Store
{
    public class VoteStore
    {
        /// <summary>
        /// Кандидаты в президенты 2018
        /// </summary>
        public Dictionary<User, int> Votes { get; } = new Dictionary<User, int>();

        
        public VoteStore(List<User> bs)
        {
            foreach (User u in bs)
            {
                Votes.Add(u, 0);
            }
        }
        
        public bool AddVote(User user, string candidateGuid)
        {
            if (user.IsVoted)
            {
                return false;
            }
            user.IsVoted = true;
            var item = Votes.Where(i => i.Key.Guid.Equals(candidateGuid)).ElementAt(0);
            Votes[item.Key] += 1;
            return true;
        }

        public Dictionary<string, int> GetVotes()
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();

            foreach (var item in Votes)
            {
                tmp.Add(item.Key.Name, item.Value);
            }
            return tmp;
        }

    }
}
