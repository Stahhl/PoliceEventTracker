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
            var update = new Update()
            {
                DateTime = DateTime.Now
            };

            var dbEvents = await dbAccess.GetAllEvents();
            var apiEvents = await apiAccess.ApiGet();

            //Get the latest date
            //If empty 01/01/0001
            var latestDateTime = dbEvents.Count() > 0 ? dbEvents.Max(e => e.DateTime) : DateTime.MinValue;

            //If event from api response has a later datetime than the latest one in the db
            //they are new ones and should be added to the db.
            var eventsToAdd = apiEvents.Where(e => DateTime.Compare(latestDateTime, e.DateTime) < 0);

            if(eventsToAdd.Count() > 0)
            {
                //TODO convert it to db models, add and save them
                update = await dbAccess.AddItemsToDb(eventsToAdd.ToList(), update);
            }

            return update;
        }
        public async Task<Update> GetLatestUpdate()
        {
            var updates = await dbAccess.GetAllUpdates();

            return updates.OrderBy(x => x.DateTime).ToList().First();
        }
        public async Task<List<Event>> GetLatestEvents(int amount)
        {
            var events = await dbAccess.GetAllEvents();

            return events.OrderByDescending(x => x.DateTime).Take(amount).ToList();
        }
        public async Task<Event> GetEventById(int id)
        {
            var events = await dbAccess.GetAllEvents();

            return events.FirstOrDefault(e => e.Id == id);
        }
    }
}