using System;
using System.Collections.Generic;
using System.Text;

namespace TwoStrokeEngineSimulator.TwoStrokeEngine
{
    /// <summary>
    /// Engine Control Class
    /// </summary>
    public class Controller
    {
        private IEngine _engine;
        private IPiston _piston;
        private ISparkPlug _sprakPlug;

        
        public Controller(IEngine engine, IPiston piston, ISparkPlug sparkPlug)
        {
            _engine = engine;
            _piston = piston;
            _sprakPlug = sparkPlug;
        }


        /// <summary>
        /// Start Engine
        /// </summary>
        public void StartEngine()
        {
            Console.WriteLine("Engine Starting!");
        }


        /// <summary>
        /// Simulate Engine Operation
        /// </summary>
        public void EngineOperate(double milSecPerFrame)
        {

            // Every frame we have a random piston speed because of random engine RPM
            double MPH = CalculatePistonSpeed();

            // Just a conversion from MPH to IPMS
            double IPMS = ConvertMPHToIPMS(MPH);

            // Calculate piston traveled distance per frame
            double distanceTraveled = IPMS * milSecPerFrame;

            // Before updating with new piston state, get previous state
            PistonState prevPistonState = _piston.GetState();

            // Negative travel distance if piston is moving from top to bottom
            if(prevPistonState == PistonState.TopToMid || prevPistonState == PistonState.MidToBot || prevPistonState == PistonState.Top)
                distanceTraveled *= -1;


            // Update piston state
            _piston.UpdatePistonStates(distanceTraveled,MPH);


            // Activate spark plug if new piston state is at top, else deactivate spark plug
            if (_piston.GetState() == PistonState.Top)
                _sprakPlug.Ignition();
            else
                _sprakPlug.StopIgnition();


            // Print info. to console
            PrintParams();

            

        }


        /// <summary>
        /// Display engine running stats
        /// </summary>
        /// <param name="distanceTraveled"></param>
        public void PrintParams()
        {
            Console.Clear();

            Console.WriteLine("======= Engine is running =======");

            Console.WriteLine();

            Console.WriteLine($"Piston: {_piston.GetState().ToString()}, {_piston.GetSpeed()} mph");
            Console.WriteLine($"Engine: {_engine.RPM.ToString()} rpm");
            Console.WriteLine($"Spark Plug: {(_sprakPlug.GetState().ToString())}");


            Console.WriteLine();

            Console.WriteLine("=================================");
        }



        /// <summary>
        /// Calculate Piston Speed
        /// </summary>
        /// <returns>Piston Speed in MPH</returns>
        public double CalculatePistonSpeed()
        {

            double FPM = CalPistonFPM();

            double MPH = ConvertFPMToMPH(FPM);

            return MPH;

        }

        /// <summary>
        /// Calculate Piston speed based on Random engine RPM
        /// </summary>
        /// <returns>Piston Speed in FPM</returns>

        private double CalPistonFPM()
        {
            _engine.SetRandomRPM();

            return _piston.GetStroke() * _engine.RPM / 6;
        }


        /// <summary>
        /// Convert from FPM to MPH
        /// </summary>
        /// <param name="FPM"></param>
        /// <returns>MPH</returns>
        private double ConvertFPMToMPH(double FPM)
        {
            return Math.Round(FPM * 60 / 5280, 2);
        }

        /// <summary>
        /// Convert from MPH to inch per milisecond
        /// </summary>
        /// <param name="MPH"></param>
        /// <returns></returns>
        private double ConvertMPHToIPMS(double MPH)
        {
            return Math.Round(MPH * 17.6/1000, 10);
        }

    }
}
