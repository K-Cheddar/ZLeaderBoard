using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zadv.ZLeaderboard.Domain.IRepositories;
using Zadv.ZLeaderboard.Domain;
using ZADV.ZLeaderboard.Data;
using System.Web;
using System.Net.Http.Formatting;
using ZADV.ZLeaderboard.Web.Models;
using System.Configuration;

namespace ZADV.ZLeaderboard.Web.Controllers.ApiControllers
{
    public class EventController : ApiController
    {
        private IEventRepository _eventRepository;
        private IParticipantRepository _participantRepository;

        public EventController(IEventRepository eventRepository, IParticipantRepository participantRepository)
        {
            _eventRepository = eventRepository;
            _participantRepository = participantRepository;
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _eventRepository.GetAll().OrderBy(e => e.IsActive).ThenBy(d => d.StartAt).ToList();
        }

        [HttpGet]
        public EventViewModel Get(int id)
        {
            Event currentEvent = _eventRepository.Get(id);

            EventViewModel model = new EventViewModel()
            {
                Name = currentEvent.Name,
                EndAt = currentEvent.EndAt,
                StartAt = currentEvent.StartAt,
                IsActive = currentEvent.IsActive,
            };

            IList<Participant> participants = EventParticipants(currentEvent);

            foreach (var particiant in participants)
            {
                var currentparticipant = new ParticipantViewModel()
                {
                    Name = particiant.Name,
                    Id = particiant.Id
                };
                model.Participants.Add(currentparticipant);
            }
            return model;
        }

        [HttpPost]
        //[ActionName("Create")]
        public void Post(EventViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                Event newEvent = new Event()
                {
                    Name = model.Name,
                    StartAt = model.StartAt,
                    EndAt = model.EndAt
                };
                _eventRepository.Add(newEvent);
                AddParticipant(model, newEvent);
                foreach (var participant in model.Participants)
                {
                    var newparticipant = new Participant()
                    {
                        Event = newEvent,
                        Name = participant.Name,
                    };

                    newparticipant.ImageId = SaveImage(participant.ImageFile);
                    _participantRepository.Add(newparticipant);
                }
            }
        }


        private Guid SaveImage(HttpPostedFileBase imageFile)
        {
            var imageName = Guid.NewGuid();
            if (imageFile == null || imageFile.ContentLength < 1)
            {
                throw new ApplicationException("Cannot upload the file to the server");
            }
            //if (!imageFile.FileName.EndsWith("png", true, System.Globalization.CultureInfo.CurrentCulture))
            //{
            //    throw new ApplicationException("File must be in png format");
            //}
            string fileName = imageName + imageFile.FileName.Substring(imageFile.FileName.LastIndexOf(".") + 1);
            string filePath = ConfigurationManager.AppSettings["Images"] + fileName;
            string saveLocation = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
            //var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/SomePath");
            if (System.IO.File.Exists(saveLocation))
            {
                System.IO.File.Delete(saveLocation);
                imageFile.SaveAs(saveLocation);
            }
            else
            {
                ModelState.AddModelError("UploadFile", "Cannot save Uploaded File to the Server.");
            }
            return imageName;
            //throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        public void Put(int id, EventViewModel model)
        {

            Event editEvent = _eventRepository.Get(id);
            editEvent.StartAt = model.StartAt;
            editEvent.EndAt = model.EndAt;
            editEvent.IsActive = model.IsActive;
            _eventRepository.Update(editEvent);

            IList<Participant> currentParticipants = EventParticipants(editEvent);
            bool present;
            foreach(var currentParticipant in currentParticipants)
            {
                present = false;
                foreach(var participant in model.Participants)
                {
                    if (participant.Id == currentParticipant.Id)
                    {
                        present = true;
                        model.Participants.Remove(participant);
                        break;
                    }
                }
                if (!present)
                {
                    _participantRepository.Remove(currentParticipant);
                }

            }
            foreach (var participant in model.Participants)
            {
                AddParticipant(model, editEvent);   
            }
         
        }

        private void AddParticipant(EventViewModel model, Event editEvent)
        {
            foreach (var participant in model.Participants)
            {
                var newparticipant = new Participant()
                {
                    Event = editEvent,
                    Name = participant.Name,
                };

                newparticipant.ImageId = SaveImage(participant.ImageFile);
                _participantRepository.Add(newparticipant);
            }

        }

        private IList<Participant> EventParticipants(Event currentEvent)
        {
            IList<Participant> participants = _participantRepository.GetAll();
            foreach (var participant in participants)
            {
                if (participant.Event.Id != currentEvent.Id)
                {
                    participants.Remove(participant);
                }
            }
            return participants;
        }
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}