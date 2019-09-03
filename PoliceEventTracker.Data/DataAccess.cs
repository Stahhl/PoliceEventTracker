using System;
using System.Threading.Tasks;
using PoliceEventTracker.Domain.Other;
using PoliceEventTracker.Domain.Models;
using PoliceEventTracker.Data.Models;
using System.Linq;
using System.Collections.Generic;

namespace PoliceEventTracker.Data
{
    public class DataAccess
    {
        public DataAccess(ApplicationSettings settings)
        {
            dbAccess = new DbAccess(settings);
            apiAccess = new ApiAccess(settings);
        }

        private DbAccess dbAccess;
        private ApiAccess apiAccess;

        //Gets items from api, compares them to items in db.
        //If there are items in api response that dont exist in the db add them.
        public async Task<Update> UpdateDatabase()
        {
            Update update = null;

            var dbEvents = await dbAccess.GetAllEvents();
            var apiEvents = await apiAccess.ApiGet();

            //Get the latest date
            //If null set it to 0
            var highestId = await GetHighestEventId();

            //If event from api response has a higher EventId than "highestId"
            //they are new ones and should be added to the db.
            var eventsToAdd = apiEvents.Where(e => e.Id > highestId);

            if(eventsToAdd.Count() > 0)
            {
                update = new Update()
                {
                    DateTime = DateTime.Now
                };

                //convert it to db models, add and save them
                update = await dbAccess.AddItemsToDb(eventsToAdd.ToList(), update);
            }

            return update;
        }

        #region Public operations
        public async Task<int> GetHighestEventId()
        {
            var events = await dbAccess.GetAllEvents();

            return events != null ? events.Max(e => e.EventId) : 0;
        }
        public async Task<List<Location>> GetTopLocations()
        {
            var locations = await dbAccess.GetAllLocations();

            locations = locations.OrderByDescending(x => x.Events.Count).ToList();

            return locations;
        }
        public async Task<Update> GetLatestUpdate()
        {
            var updates = await dbAccess.GetAllUpdates();

            return updates.OrderByDescending(x => x.DateTime).First();
        }
        public async Task<List<Event>> GetLatestEvents(int amount)
        {
            var events = await dbAccess.GetAllEvents();

            return events.OrderByDescending(x => x.DateTime).Take(amount).ToList();
        }
        public async Task<Event> GetEventById(int id)
        {
            return await dbAccess.GetEventById(id);
        }
        public async Task<List<Event>> RemoveAllErrors()
        {
            var events = await dbAccess.GetAllEvents();
            var result = new List<Event>();

            var errors = from e in events
                         group e by e.EventId into grp
                         where (grp.Count() > 1)
                         select (grp)
                         .ToList();

            foreach (var e1 in errors)
            {
                foreach (var e2 in e1)
                {
                    result.Add(e2);
                    //dbAccess.RemoveItem(e2.Id);
                }
            }

            dbAccess.RemoveRange(result);
            return result;
        }
        #endregion
    }
}