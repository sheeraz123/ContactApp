using Member.Application.Contracts.Persistence;
using Member.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Member.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly List<MemberInfo> memberInfos;
        public MemberRepository(bool initialize = true)
        {
            memberInfos = new List<MemberInfo>();
            if (initialize)
            {
                memberInfos.AddRange(new List<MemberInfo>()
                {
                    new MemberInfo()
                    {
                        Id=1,
                        FirstName="Alex",
                        LastName="BlaBla",
                        Email="alex.blabla@aol.at",
                      

                    },
                    new MemberInfo()
                    {
                        Id=2,
                        FirstName="otto",
                        LastName="blubb",
                        Email="otto.blubb@dsl.de",
                        

                    },
                    new MemberInfo()
                    {
                        Id=3,
                        FirstName="Peter",
                        LastName="Pan",
                        Email="peter.pan@neverland.com",
                       

                    }
                });

            }
        }
        public async Task<MemberInfo> AddAsync(MemberInfo memberInfo)
        {
     
            memberInfos.Add(memberInfo);
            var resultsTask = Task.Run(() => memberInfos.Where(x => x.Id == memberInfo.Id).SingleOrDefault());
            return await resultsTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result=memberInfos.Where(x=>x.Id==id).SingleOrDefault();        
            var resultsTask = Task.Run(() => memberInfos.Remove(result));
            return await resultsTask;
        }

        public async Task<IReadOnlyCollection<MemberInfo>> GetAllAsync()
        {
            var resultsTask = Task.Run(() => memberInfos.ToList());
            return await resultsTask;

        }

        public async Task<MemberInfo> GetByIdAsync(int id)
        {
            var resultsTask = Task.Run(() => memberInfos.Where(s => s.Id == id).SingleOrDefault());
            return await resultsTask;

        }

        public async Task<IReadOnlyCollection<MemberInfo>> UpdateAsync(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
            MemberInfo? result = memberInfos.FirstOrDefault(temp => temp.Id == memberInfo.Id);
            result.FirstName = memberInfo.FirstName;
            result.Email = memberInfo.Email;
            result.LastName = memberInfo.LastName;
            var resultsTask = Task.Run(() => memberInfos.ToList());
            return await resultsTask;
        }
    }
}
