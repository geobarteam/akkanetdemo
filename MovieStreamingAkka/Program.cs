using System;
using System.Threading;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreamingAkka.Messages;

namespace MovieStreamingAkka
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColorConsole.WriteLine(ConsoleColor.Gray, "Creating MovieActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            ColorConsole.WriteLine(ConsoleColor.Gray, "Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

                do
                {
                    ShortPause();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    ColorConsole.WriteLineGray("enter a command and hit enter");

                    var command = Console.ReadLine();

                    if (command.StartsWith("play"))
                    {
                        int userId = int.Parse(command.Split(',')[1]);
                        string movieTitle = command.Split(',')[2];

                        var message = new PlayMovieMessage(movieTitle, userId);
                        MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                    }

                    if (command.StartsWith("stop"))
                    {
                        int userId = int.Parse(command.Split(',')[1]);

                        var message = new StopMovieMessage(userId);
                        MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                    }

                    if (command == "exit")
                    {
                        MovieStreamingActorSystem.Terminate().Wait();
                        ColorConsole.WriteLineGray("Actor system shutdown");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }

                } while (true);

            
        }

        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
