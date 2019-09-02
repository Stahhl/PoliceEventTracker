using PoliceEventTracker.Domain.Models;
using PoliceEventTracker.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PoliceEventTracker.Domain.Other;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PoliceEventTracker.Data.Models
{
    internal class DbAccess
    {
        public DbAccess(ApplicationSettings settings)
        {
            context = new PoliceEventDbContext(settings);
        }

        private PoliceEventDbContext context;

        #region Add
        //Add items from api respons to db
        internal async Task<Update> AddItemsToDb(List<ApiEvent> apiItems, Update update)
        {
            try
            {
                var events = new List<Event>();

                foreach (var apiItem in apiItems)
                {
                    var addedLocation = await GetOrCreateLocation(apiItem.Location);
                    var addedEvent = CreateEvent(apiItem, apiItem.Location, addedLocation);

                    update.Events.Add(addedEvent);

                    await context.Events.AddAsync(addedEvent);
                }

                update.Count = update.Events.Count();
                await context.Updates.AddAsync(update);
                await context.SaveChangesAsync();

                return update;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        internal Event CreateEvent(ApiEvent apiEvent, ApiLocation apiLocation, Location location)
        {
            try
            {
                //Create a new event
                var addedEvent = new Event
                {
                    EventId = apiEvent.Id,
                    DateTime = apiEvent.DateTime,
                    Name = apiEvent.Name,
                    Summary = apiEvent.Summary,
                    Url = apiEvent.Url,
                    Type = apiEvent.Type,
                    Coordinate = apiLocation.Gps,

                    Location = location
                };

                //Add the event to the location
                location.Events.Add(addedEvent);

                return addedEvent;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        internal async Task<Location> GetOrCreateLocation(ApiLocation apiLocation)
        {
            try
            {
                //Search for location by name
                Location location = await GetLocationByName(apiLocation.Name);

                //If location doesn't exist in db create it
                //If location exists keep using that instance
                if (location == null)
                {
                    location = new Location
                    {
                        Name = apiLocation.Name
                    };

                    await context.Locations.AddAsync(location);
                    await context.SaveChangesAsync();
                }

                return location;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Get
        //Get all events in db
        internal async Task<List<Event>> GetAllEvents()
        {
            return await context.Events
                .Include(e => e.Location)
                .ToListAsync();
        }
        internal async Task<Location> GetLocationByName(string name)
        {
            return await context.Locations
                .Include(l => l.Events)
                .FirstOrDefaultAsync(l => l.Name.ToUpper() == name.ToUpper());
        }
        internal async Task<List<Update>> GetAllUpdates()
        {
            return await context.Updates
                .Include(u => u.Events)
                .ToListAsync();
        }
        #endregion
    }
}
    