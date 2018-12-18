using System;
using System.IO;
using System.Runtime.CompilerServices;
using Akka.Actor;
using Akka.Configuration;
using MovieStreaming.Common;

namespace Moviestreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            var hoconString = File.ReadAllText(".\\Akka.config");
            var cfg = ConfigurationFactory.ParseString(hoconString);

            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote console!");
            
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem", cfg);

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate().Wait();
        }
    }
}
