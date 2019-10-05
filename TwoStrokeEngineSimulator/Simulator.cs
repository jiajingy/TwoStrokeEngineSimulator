using System;
using System.Threading;
using TwoStrokeEngineSimulator.TwoStrokeEngine;

namespace TwoStrokeEngineSimulator
{
    class Simulator
    {
        static void Main(string[] args)
        {
            // Some configs for simulation
            double pistonStroke = 2;
            int engineMaxRPM = 1000;

            int startCountDown = 3;

            // Below 2 values in reality should be the same, for demo purpose, make engine refresh rate slower so that piston moves slower
            int milSecPerFrame = 300;
            int engineRefreshRatePerMilSec = 10;


            // Initialize all components
            IEngine engine = new Engine(engineMaxRPM);
            IPiston piston = new Piston(pistonStroke);
            ISparkPlug sparkPlug = new SparkPlug();

            // Initialize controller
            Controller controller = new Controller(engine, piston, sparkPlug);


            // Start engine
            controller.StartEngine();

            // Starting ...
            for(int i = 1; i <= startCountDown; i++)
            {
                Console.Write($"\r{startCountDown+1- i}");
                Thread.Sleep(1 * 1000);
            }
            

            // Simulate engine
            while (true)
            {
                Thread.Sleep(milSecPerFrame);
                controller.EngineOperate(engineRefreshRatePerMilSec);
            }
        }
    }
}
