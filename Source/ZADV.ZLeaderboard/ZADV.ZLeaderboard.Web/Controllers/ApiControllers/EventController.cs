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
using System.Web.Hosting;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ZADV.ZLeaderboard.Web.Security;
using System.Drawing;

namespace ZADV.ZLeaderboard.Web.Controllers.ApiControllers
{
    //[BasicAuthenticationAttribute("zAdmin", "zPassword",
    //BasicRealm = "Zimm")]
    //[IdentityBasicAuthentication] // Enable Basic authentication for this controller.
    [Authorize] // Require authenticated requests.
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
            IList<Event> currentEvents = _eventRepository.GetAll().Where(x => x.EndAt >= DateTime.Now).OrderByDescending(e => e.IsActive).ThenBy(d => d.StartAt).ToList();
            IList<Event> pastEvents = _eventRepository.GetAll().Where(x => x.EndAt < DateTime.Now).OrderByDescending(e => e.IsActive).ThenBy(d => d.StartAt).ToList();
            foreach (var e in pastEvents)
            {
                currentEvents.Add(e);
            }
            return currentEvents;
        }

        [HttpGet]
        public EventViewModel Get(int id)
        {
            Event currentEvent = _eventRepository.Get(id);

            EventViewModel model = new EventViewModel()
            {
                Name = currentEvent.Name,
                EndAt = currentEvent.EndAt.ToUniversalTime(),
                StartAt = currentEvent.StartAt.ToUniversalTime(),
                IsActive = currentEvent.IsActive,
                Description = currentEvent.Description
            };

            IList<Participant> participants = EventParticipants(currentEvent);

            foreach (var particiant in participants)
            {
                var currentparticipant = new ParticipantViewModel()
                {
                    Name = particiant.Name,
                    Id = particiant.Id
                };
                if (model.Participants == null)
                {
                    model.Participants = new List<ParticipantViewModel>();
                }
                model.Participants.Add(currentparticipant);
            }
            return model;
        }

        [HttpPost]
        //[ActionName("Create")]
        public void Post([FromBody]EventViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                Event newEvent = new Event()
                {
                    Name = model.Name,
                    StartAt = model.StartAt,
                    EndAt = model.EndAt,
                    IsActive = model.IsActive,
                    Description = model.Description
                   
                };
                _eventRepository.Add(newEvent);
                AddParticipants(model, newEvent);
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
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpPut]
        public void Put(int id, [FromBody] EventViewModel model)
        {

            Event editEvent = _eventRepository.Get(id);
            editEvent.Name = model.Name;
            editEvent.StartAt = model.StartAt;
            editEvent.EndAt = model.EndAt;
            editEvent.IsActive = model.IsActive;
            editEvent.Description = model.Description;
            _eventRepository.Update(editEvent);

            IList<Participant> currentParticipants = EventParticipants(editEvent);
            bool present;
            foreach (var currentParticipant in currentParticipants)
            {
                present = false;
                foreach (var participant in model.Participants)
                {
                    if (participant.Id == currentParticipant.Id)
                    {
                        present = true;
                        model.Participants.Remove(participant);
                        break;
                    }
                }

                if(!present)
                {
                    _participantRepository.Remove(currentParticipant);
                }

            }
            AddParticipants(model, editEvent);
        }

        private void AddParticipants(EventViewModel model, Event editEvent)
        {
            List<Color> colors = createColors();
            Random rand = new Random();
            int randColor;

            foreach (var participant in model.Participants)
            {
                randColor = (rand.Next(0, colors.Count - 1));
                var newparticipant = new Participant()
                {
                    Event = editEvent,
                    Name = participant.Name,
                    Color = colors[randColor].ToString()
                };

                if(colors.Count > 0)
                {
                    colors.RemoveAt(randColor);
                }
                else
                {
                    colors = createColors();
                }

                //newparticipant.ImageId = SaveImage(participant.ImageFile);
                _participantRepository.Add(newparticipant);
            }

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
        
        private List<Color> createColors()
        {
            List<Color> colors = new List<Color>() {
                ColorTranslator.FromHtml("#EC1818"),
                ColorTranslator.FromHtml("#FF8B00"),
                ColorTranslator.FromHtml("#FBFF00"),
                ColorTranslator.FromHtml("#74FF00"),
                ColorTranslator.FromHtml("#00FF55"),
                ColorTranslator.FromHtml("#00D1FF"),
                ColorTranslator.FromHtml("#000CFF"),
                ColorTranslator.FromHtml("#C100FF"),
                ColorTranslator.FromHtml("#FF00AA"),
                ColorTranslator.FromHtml("#FF0049")
            };

            return colors;
        } 
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        
    }
}