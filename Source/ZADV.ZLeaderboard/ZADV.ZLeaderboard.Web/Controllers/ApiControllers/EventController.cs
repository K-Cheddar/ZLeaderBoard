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

namespace ZADV.ZLeaderboard.Web.Controllers.ApiControllers
{
    public class EventController : ApiController
    {      

        private IEventRepository _eventRepository;
        public EventController(IEventRepository eventRepository)
        {           
            _eventRepository = eventRepository;            
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Event> Get()
        {       
            return _eventRepository.GetAll().OrderByDescending(e => e.StartAt);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(FormDataCollection form)
        {
            string name = form.Get("Name");
            DateTime startAt = Convert.ToDateTime(form.Get("StartAt"));
            DateTime endAt = Convert.ToDateTime(form.Get("EndAt"));
            
            
            Event obj = new Event()
            {
                Name = name,
                StartAt = startAt,
                EndAt = endAt
            };
            _eventRepository.Add(obj);

        }
        
        [HttpPost]
        [ActionName("Create")]
        public void Post(Event _event)
        {
            //if (ModelState.IsValid && _event != null)
            //{
            //    Event obj = new Event()
            //    {
            //        Name = _event.Name,
            //        StartAt = _event.StartAt,
            //        EndAt = _event.EndAt
            //    };
                
                
            //}

            _eventRepository.Add(_event);

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}