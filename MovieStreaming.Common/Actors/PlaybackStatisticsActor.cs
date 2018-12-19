using System;
using Akka.Actor;
using Akka.Routing;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            var props = Props.Create<MoviePlayCounterActor>().WithRouter(new RoundRobinPool(5));
            Context.ActorOf(props, "MoviePlayCounter");
        }


        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PostRestart because: {reason.Message} ");

            base.PostRestart(reason);
        }
        #endregion
    }
}