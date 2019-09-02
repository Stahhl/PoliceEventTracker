# PoliceEventTracker
Tracks events of the Swedish police using their API.

API: https://polisen.se/om-polisen/om-webbplatsen/oppna-data/api-over-polisens-handelser/

Description:
  PoliceEventTracker.App -> Handles frontend presentation.
  PoliceEventTracker.Data -> Handles API and database.
  PoliceEventTracker.Domain -> Handles data classes (models) in the solution.

Install:
  1: migrate/update
  2: call "[localhost]/home/updatedatabase" to fetch data from api.
