using System;
using System.Linq;

namespace DatabaseTrainingsApp
{
    class ActivitiesPerfomances
    {
        public void ActivityPerfomances(Triathlon triathlon, double distance, double duration)
        {
            double maxDistance, totalDistance, totalDuration, averageSpeed;
            int activityQuantity;
            if (distance != 0.00000001 && duration != 0.00000001)
                using (var db = new ActivityDatabase())
                {
                    if (triathlon.Discriminator == "Running")
                    {
                        maxDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Running").Max(p => p.Distance);
                        activityQuantity = db.TriathlonTrainings.Where(n => n.Discriminator == "Running").Count();
                        totalDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Running").Sum(p => p.Distance);
                        totalDuration = db.TriathlonTrainings.Where(n => n.Discriminator == "Running").Sum(p => p.Duration);
                        averageSpeed = db.TriathlonTrainings.Where(n => n.Discriminator == "Running").Average(p => p.Speed);

                        Console.WriteLine($"Best running distance: {maxDistance} km");
                        Console.WriteLine($"Total distance per {activityQuantity} activity: {Math.Round(totalDistance, 2)} km");
                        Console.WriteLine($"Total duration per {activityQuantity} activity: {totalDuration} min ({GetTimeInHours()} hours {GetTimeInMinutes()} min)");
                        Console.WriteLine($"Average speed: {Math.Round(averageSpeed, 2)} km/hour");
                        TwoLastDaysRunningData();
                    }
                    else if (triathlon.Discriminator == "Bicycle")
                    {
                        maxDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Bicycle").Max(p => p.Distance);
                        activityQuantity = db.TriathlonTrainings.Where(n => n.Discriminator == "Bicycle").Count();
                        totalDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Bicycle").Sum(p => p.Distance);
                        totalDuration = db.TriathlonTrainings.Where(n => n.Discriminator == "Bicycle").Sum(p => p.Duration);
                        averageSpeed = db.TriathlonTrainings.Where(n => n.Discriminator == "Bicycle").Average(p => p.Speed);

                        Console.WriteLine($"Best bike distance: {maxDistance} km");
                        Console.WriteLine($"Total distance per {activityQuantity} activity: {Math.Round(totalDistance, 2)} km");
                        Console.WriteLine($"Total duration per {activityQuantity} activity: {totalDuration} min ({GetTimeInHours()} hours {GetTimeInMinutes()} min)");
                        Console.WriteLine($"Average speed: {Math.Round(averageSpeed, 2)} km/hour");
                        TwoLastDaysBikeData();
                    }
                    else
                    {
                        maxDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Swimming").Max(p => p.Distance);
                        activityQuantity = db.TriathlonTrainings.Where(n => n.Discriminator == "Swimming").Count();
                        totalDistance = db.TriathlonTrainings.Where(n => n.Discriminator == "Swimming").Sum(p => p.Distance);
                        totalDuration = db.TriathlonTrainings.Where(n => n.Discriminator == "Swimming").Sum(p => p.Duration);
                        averageSpeed = db.TriathlonTrainings.Where(n => n.Discriminator == "Swimming").Average(p => p.Speed);
                    
                        Console.WriteLine($"Best swim distance: {maxDistance} km");
                        Console.WriteLine($"Total distance per {activityQuantity} activity: {Math.Round(totalDistance, 2)} km");
                        Console.WriteLine($"Total duration per {activityQuantity} activity: {totalDuration} min ({GetTimeInHours()} hours {GetTimeInMinutes()} min)");
                        Console.WriteLine($"Average speed: {Math.Round(averageSpeed, 2)} km/hour");
                        TwoLastDaysSwimmingData();
                    }
                }
            double GetTimeInHours()
            {
                double hours = Math.Floor(totalDuration / 60);
                return hours;
            }

            double GetTimeInMinutes()
            {
                double minutes = Math.Round(totalDuration % 60);
                return minutes;
            }
            void TwoLastDaysSwimmingData()
            {
                using (var db = new ActivityDatabase())
                {
                    var lastTwoDays = DateTime.Today.AddDays(-2);
                    var twoLastDaysData = (from training in db.TriathlonTrainings
                                       where training.Discriminator == "Swimming"
                                       where training.CurrentData > lastTwoDays
                                           select training).ToList();
                    foreach (var training in twoLastDaysData)
                         totalDistance = twoLastDaysData.Sum(p => p.Distance);
                    Console.WriteLine($"Total distance for 2 last days: {Math.Round(totalDistance, 2)} km");
                }
            }
            void TwoLastDaysRunningData()
            {
                using (var db = new ActivityDatabase())
                {
                    var lastTwoDays = DateTime.Today.AddDays(-2);
                    var twoLastDaysData = (from training in db.TriathlonTrainings
                                           where training.Discriminator == "Running"
                                           where training.CurrentData > lastTwoDays
                                           select training).ToList();
                    foreach (var training in twoLastDaysData)
                        totalDistance = twoLastDaysData.Sum(p => p.Distance);
                   Console.WriteLine($"Total distance for 2 last days: {Math.Round(totalDistance, 2)} km");
                }
            }
            void TwoLastDaysBikeData()
            {
                using (var db = new ActivityDatabase())
                {
                    var lastTwoDays = DateTime.Today.AddDays(-2);
                    var twoLastDaysData = (from training in db.TriathlonTrainings
                                           where training.Discriminator == "Bicycle"
                                           where training.CurrentData > lastTwoDays
                                           select training).ToList();
                    foreach (var training in twoLastDaysData)
                        totalDistance = twoLastDaysData.Sum(p => p.Distance);
                    Console.WriteLine($"Total distance for 2 last days: {Math.Round(totalDistance, 2)} km");
                }
            }
        }
    }
}