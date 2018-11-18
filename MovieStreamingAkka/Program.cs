using System;
using Akka.Actor;
using MovieStreamingAkka.Actors;
using MovieStreamingAkka.Messages;

namespace MovieStreamingAkka
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props userActor = Props.Create<UserActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(userActor, "UserActor");

            Console.ReadKey();
            playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: The movie", 42));
            Console.ReadKey();
            playbackActorRef.Tell(new PlayMovieMessage("Partial Recall", 99));
            Console.ReadKey();
            playbackActorRef.Tell(new StopMovieMessage());
            Console.ReadKey();
            playbackActorRef.Tell(new StopMovieMessage());

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate().Wait();

            Console.WriteLine("Actor System Shutdown");
            Console.ReadKey();

        }
    }
}
