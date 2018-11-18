using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using MovieStreamingAkka.Messages;

namespace MovieStreamingAkka.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
            this.Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        protected void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow , $"Received movie => title {message.MovieTitle} User ID: {message.UserId}"); 
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreRestart because:" + reason);
            base.PreRestart(reason, message);
        }
    }
}
