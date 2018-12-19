using System;
using Akka.Actor;
using MovieStreaming.Common.Exceptions;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    switch (ex)
                    {
                        case SimulatedTerribleMovieException se:
                            return Directive.Resume;
                        case SimulatedCorruptStateException ce:
                            return Directive.Restart;
                        default:
                            return Directive.Escalate;
                    }
                }

            );
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