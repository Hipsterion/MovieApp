using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;
using MoviesApp.DAL;
using Microsoft.Extensions.Options;
namespace MoviesApp.BLL
{
    public class MemberService
    {
        private ICrudRepository<Member, int> _repository;
        public AppConfigData ConfigData { get; }

        public MemberService(IOptions<AppConfigData> configData, ICrudRepository<Member, int> repository)
        {
            ConfigData = configData.Value;
            _repository = repository;
        }

        public IEnumerable<Member> GetMembers()
        {
            return _repository.GetAll();
        }

        public Member GetMember(int id)
        {
            return _repository.GetById(id);
        }

        public Member AddMember(Member member)
        {
            return _repository.Insert(member);
        }

        public Member UpdateMember(Member member)
        {
            return _repository.Update(member);
        }

        public void DeleteMember(int id)
        {
            _repository.Delete(id);
        }
    }
}
