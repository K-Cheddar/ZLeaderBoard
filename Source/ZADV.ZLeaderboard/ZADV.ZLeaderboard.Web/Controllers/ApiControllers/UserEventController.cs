﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Zadv.ZLeaderboard.Domain;
using Zadv.ZLeaderboard.Domain.IRepositories;
using ZADV.ZLeaderboard.Domain;
using ZADV.ZLeaderboard.Domain.IRepositories;
using ZADV.ZLeaderboard.Web.Models;

namespace ZADV.ZLeaderboard.Web.Controllers.ApiControllers
{
    public class UserEventController : ApiController
    {
        private IEventRepository _eventRepository;
        private IParticipantRepository _participantRepository;
        private IVoterRepository _voterRepository;

        public UserEventController(IEventRepository eventRepository, IParticipantRepository participantRepository, IVoterRepository voterRepository)
        {
            _eventRepository = eventRepository;
            _participantRepository = participantRepository;
            _voterRepository = voterRepository;
        }

        [HttpGet]
        public UserEventsViewModel Get()
        {
            IList<Event> allEvents = _eventRepository.GetAll().Where(e => e.IsActive).OrderBy(p => p.StartAt).ToList();
            UserEventsViewModel model = new UserEventsViewModel();
            foreach (var singleEvent in allEvents)
            {
                if (EventParticipants(singleEvent).Count > 0)
                {

                    if (singleEvent.StartAt <= DateTime.Now && singleEvent.EndAt >= DateTime.Now)
                    {
                        model.ActiveEvents.Add(singleEvent);
                    }
                    else if (singleEvent.StartAt > DateTime.Now)
                    {
                        model.UpcomingEvents.Add(singleEvent);
                    }
                    else if (singleEvent.StartAt < DateTime.Now && singleEvent.EndAt < DateTime.Now)
                    {
                        model.PastEvents.Add(singleEvent);
                    }
                }
            }
            model.PastEvents = model.PastEvents.OrderByDescending(t => t.EndAt).ToList();

            return model;
        }

        public UserEventViewModel Get(int id)
        {
            var test = _eventRepository.Get(id);
            if (test == null)
            {
                UserEventViewModel mod = new UserEventViewModel();
                return mod;
            }
            IList<Participant> participants = EventParticipants(_eventRepository.Get(id)).OrderBy(p => p.Name).ToList();
            List<ParticipantViewModel> winners = new List<ParticipantViewModel>();
            List<string> winnerNames = new List<string>();

            int voteCount = 0, max = 0;

            UserEventViewModel model = new UserEventViewModel()
            {
                Name = _eventRepository.Get(id).Name,
                Description = _eventRepository.Get(id).Description,
                EndAt = _eventRepository.Get(id).EndAt.ToUniversalTime(),
                StartAt = _eventRepository.Get(id).StartAt.ToUniversalTime(),
                MultipleVotes = _eventRepository.Get(id).MultipleVotes
            };


            foreach (var participant in participants)
            {
                //vote count gets the number of participants from the list equal to this one
                var voterList = _voterRepository.GetAll()
                    .Where(p => p.Participant.Id == participant.Id);
                voteCount = voterList.Count();

                var votedFor = false;
                string value = GetCookie();

                foreach (var vo in voterList)
                {
                    if (value.Equals(vo.Email))
                    {
                        votedFor = true;
                        break;
                    }
                }

                ParticipantViewModel part = new ParticipantViewModel()
                {
                    Name = participant.Name,
                    Id = participant.Id,
                    VoteCount = voteCount,
                    Color = participant.Color,
                    VotedFor = votedFor
                };

                if (voteCount > max)
                {
                    max = voteCount;
                    winners.Clear();
                    winners.Add(part);
                }
                else if (voteCount == max)
                {
                    winners.Add(part);
                }
                model.Participants.Add(part);
            }


            foreach (var winner in winners)
            {
                winnerNames.Add(winner.Name);
            }
            model.Winners = string.Join(", ", winnerNames);
            return model;
        }

        public VotesViewModel Put(int id)
        {
            Participant participant = _participantRepository.Get(id);
            VotesViewModel vvm = new VotesViewModel();
            bool multipleVotes = _eventRepository.Get(participant.Event.Id).MultipleVotes;
            IEnumerable<Voter> voters = _voterRepository.GetAll().Where(v => v.Participant.Event.Id == participant.Event.Id);
            //foreach (var voter in voters)
            //{
            //    if (voter.Participant.Id != participant.Id)
            //    {
            //        voters.Remove(voter);
            //    }
            //}
            bool voteAllowed = true;
            string value = GetCookie();
            
            if (!multipleVotes)
            {
                foreach (var voter in voters)
                {
                    //check if Voter voted for a different participant from same event
                    if (voter.Email == value && voter.Participant.Event.Id == participant.Event.Id && voter.Participant.Id != participant.Id)
                    {
                        _voterRepository.Remove(voter);

                    }
                    //check if voter already voted for participant
                    else if (voter.Email == value && voter.Participant.Id == participant.Id)
                    {
                        voteAllowed = false;
                    }
                }
                if (voteAllowed)
                {
                    Voter voter = new Voter()
                    {
                        Email = value,
                        Participant = participant
                    };
                    //var vps = _voterRepository.GetAll();
                    //foreach (var vp in vps)
                    //{
                    //    if()
                    //}
                    _voterRepository.Add(voter);
                }
                _participantRepository.Update(participant);
                vvm.VoteCount = _voterRepository.GetAll().Where(p => p.Participant.Id == participant.Id).Count();
                vvm.Voted = !voteAllowed;
                return vvm;
            }

            else
            {
                foreach (var voter in voters)
                {
                    //check if Voter voted for a different participant from same event
                    //if (voter.Email == value && voter.Participant.Event.Id == participant.Event.Id && voter.Participant.Id != participant.Id)
                    //{
                    //    _voterRepository.Remove(voter);

                    //}
                    //check if voter already voted for participant
                    if (voter.Email == value && voter.Participant.Id == participant.Id)
                    {
                        voteAllowed = false;
                    }
                }
                if (voteAllowed)
                {
                    Voter voter = new Voter()
                    {
                        Email = value,
                        Participant = participant
                    };
                    //var vps = _voterRepository.GetAll();
                    //foreach (var vp in vps)
                    //{
                    //    if()
                    //}
                    _voterRepository.Add(voter);
                }
                _participantRepository.Update(participant);
                vvm.VoteCount = _voterRepository.GetAll().Where(p => p.Participant.Id == participant.Id).Count();
                vvm.Voted = !voteAllowed;
                return vvm;
            }
        }

        public HttpResponseMessage emailCookie(EmailInputViewModel model)
        {
            var response = new HttpResponseMessage();
            var cookie = new CookieHeaderValue("user", model.Email);
            cookie.Expires = DateTime.Now.AddYears(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;
        }

        private IList<Participant> EventParticipants(Event currentEvent)
        {
            IList<Participant> participants = _participantRepository.GetAll();
            IList<Participant> eventParticipants = new List<Participant>();
            foreach (var participant in participants)
            {
                if (participant.Event.Id == currentEvent.Id)
                {
                    eventParticipants.Add(participant);
                }
            }
            return eventParticipants;
        }

        private string GetCookie()
        {
            string value = "";

            IEnumerable<CookieHeaderValue> cookies = this.Request.Headers.GetCookies("user");
            if (cookies.Any())
            {
                IEnumerable<CookieState> cookie = cookies.First().Cookies;
                if (cookie.Any())
                {
                    value = cookie.First().Value;
                }
            }

            return value;
        }
    }
}