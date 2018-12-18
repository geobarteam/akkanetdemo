using System;
using Akka.Actor;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _userId = userId;

            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => ColorConsole.WriteLineRed(
                    $"UserActor {_userId} Error: cannot start playing another movie before stopping existing one"));
           
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow($"UserActor {_userId} has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
          
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed($"UserActor {_userId} Error: cannot stop if nothing is playing"));

            ColorConsole.WriteLineYellow($"UserActor {_userId} has now become Stopped");
        }
        
        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            ColorConsole.WriteLineYellow($"UserActor {_userId} is currently watching '{_currentlyWatching}'");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} has stopped watching '{_currentlyWatching}'");

            _currentlyWatching = null;

            Become(Stopped);
        }



        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} PostRestart because: {reason}");

            base.PostRestart(reason);
        } 
        #endregion
    }
}
