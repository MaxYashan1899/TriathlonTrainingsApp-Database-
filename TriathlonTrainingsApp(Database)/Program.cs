using System;

enum Sport
{
    running = 1,
    bike,
    swimming
}

namespace DatabaseTrainingsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Sport kindOfSport;
                Console.WriteLine("Enter kind of activity: 1) running 2) bike 3) swimming");
                string enteredData = Console.ReadLine();
                if (!Enum.TryParse(enteredData, out kindOfSport))
                    Console.WriteLine("It is not correct data. Try again");
                else
                    kindOfSport = (Sport)Enum.Parse(typeof(Sport), enteredData, ignoreCase: true);

                double distance, duration;
                Console.WriteLine("Enter activity distance(in kilometers): (for example: 10,5)");
                enteredData = Console.ReadLine();
                if (!Double.TryParse(enteredData, out distance) || distance < 0 || distance > 1000)
                {
                    distance = 0.00000001;
                    Console.WriteLine("It is not correct data");
                }

                Console.WriteLine("Enter time (in minutes):");
                enteredData = Console.ReadLine();
                if (!Double.TryParse(enteredData, out duration) || duration < 0 || duration > 5000)
                {
                    duration = 0.00000001;
                    Console.WriteLine("It is not correct data");
                }

                switch (kindOfSport)
                {
                    case Sport.running:
                        using (var db = new ActivityDatabase())
                        {
                            var running = new Running() { Distance = distance, Duration = duration, Speed = distance / (duration / 60), Pace = 60 / (distance / (duration / 60)), CurrentData = DateTime.Now };
                            db.TriathlonTrainings.Add(running);

                            if (distance != 0.00000001 && duration != 0.00000001)
                                db.SaveChanges();
                            else
                                db.TriathlonTrainings.Remove(running);
                            ActivityResult(running);
                        }
                        break;
                    case Sport.bike:
                        using (var db = new ActivityDatabase())
                        {
                            var bike = new Bicycle() { Distance = distance, Duration = duration, Speed = distance / (duration / 60), Pace = 60 / (distance / (duration / 60)), CurrentData = DateTime.Now };
                            db.TriathlonTrainings.Add(bike);
                            if (distance != 0.00000001 && duration != 0.00000001)
                                db.SaveChanges();
                            else
                                db.TriathlonTrainings.Remove(bike);
                            ActivityResult(bike);
                        }
                        break;
                    case Sport.swimming:
                        using (var db = new ActivityDatabase())
                        {
                            var swimming = new Swimming() { Distance = distance, Duration = duration, Speed = distance / (duration / 60), Pace = 60 / (distance / (duration / 60)), CurrentData = DateTime.Now };
                            db.TriathlonTrainings.Add(swimming);
                            if (distance != 0.00000001 && duration != 0.00000001)
                                db.SaveChanges();
                            else
                                db.TriathlonTrainings.Remove(swimming);
                            ActivityResult(swimming);
                        }
                        break;
                    default:
                        Console.WriteLine("It is not correct data");
                        break;

                }
                void ActivityResult(Triathlon triathlon)
                {
                    var activityEvent = new ActivityEvent();
                    activityEvent.ActivityEventHandler(triathlon, distance, duration);

                    var activityPerfomances = new ActivitiesPerfomances();
                    activityPerfomances.ActivityPerfomances(triathlon, distance, duration);
                }
                Console.WriteLine();
                ConsoleColor color = Console.ForegroundColor;
                Console.WriteLine("For exit press key \"E\" and \"Enter\"");
                Console.WriteLine("For continue press any other key");
                string exit = Console.ReadLine();
                if (exit == "E")
                {
                    break;
                }
                Console.Clear();
                Console.ForegroundColor = color;

            }
        }
    }
}
