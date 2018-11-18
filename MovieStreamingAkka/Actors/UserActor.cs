using System;
using Akka.Actor;
using MovieStreamingAkka.Messages;

namespace MovieStreamingAkka.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
            ColorConsole.WriteLineBlue("Setting initial behavior to Stopped!");
            Stopped();
        }

        void Playing()
        {
            Receive<PlayMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: cannot start a movie when one is playing!"));
            Receive<StopMovieMessage>(message => StopPlayingMovie());
        }

        void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayTheMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message =>
                ColorConsole.WriteLineRed("Error: Can't stop watching a movie when no movie is playing!"));
        }

       
        private void StartPlayTheMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteLineBlue($"User is currently watching '{_currentlyWatching}");

            Become(Playing);
        }      

        private void StopPlayingMovie()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, $"Stopping playing movie {_currentlyWatching}!");
            _currentlyWatching = null;

            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart because:" + reason);
            base.PreRestart(reason, message);
        }
    }
}
