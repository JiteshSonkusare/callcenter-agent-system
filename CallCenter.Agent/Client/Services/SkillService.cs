using CallCenter.Agent.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Agent.Client.Services
{
    //public interface ISkillService
    //{
    //    Task<Skill[]> GetSkills();
    //    Task<string> GetSkillById(int id);
    //}

    public class SkillService
    {
        public Task<Skill[]> GetSkills()
        {
            return Task.FromResult(
                new Skill[]{
                    new Skill{ Id=1, Name="Helpdesk" },
                    new Skill{ Id=2,Name="Loan" },
                    new Skill{ Id=3,Name="Savings" },
                    new Skill{ Id=4,Name="Pension " }
                });
        }

        public string GetSkillById(int id)
        {
            var skills = Task.FromResult(
                new Skill[] {
                new Skill{ Id=1, Name="Helpdesk" },
                new Skill{ Id=2,Name="Loan" },
                new Skill{ Id=3,Name="Savings" },
                new Skill{ Id=4,Name="Pension " }
                });

            var text= skills.Result.FirstOrDefault(v => v.Id.Equals(id)).Name;
            return text;
        }
    }
}
