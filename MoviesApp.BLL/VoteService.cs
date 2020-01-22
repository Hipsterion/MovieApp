using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL;
using MoviesApp.Domain;
using Microsoft.Extensions.Options;
namespace MoviesApp.BLL
{
    public class VoteService
    {
        private IVoteRepository _repository;
        public AppConfigData ConfigData { get; }

        public VoteService(IOptions<AppConfigData> configData, IVoteRepository repository)
        {
            ConfigData = configData.Value;
            _repository = repository;
        }

        public IEnumerable<Vote> GetVotes()
        {
            return _repository.GetAll();
        }

        public Vote GetVote((int, int) id)
        {
            return _repository.GetById(id);
        }

        public Vote AddVote(Vote vote)
        {
            return _repository.Insert(vote);
        }

        public Vote UpdateVote(Vote vote)
        {
            return _repository.Update(vote);
        }

        public void DeleteVote((int, int) id)
        {
            _repository.Delete(id);
        }
    }
}

