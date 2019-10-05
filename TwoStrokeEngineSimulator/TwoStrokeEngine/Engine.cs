using System;
using System.Collections.Generic;
using System.Text;

namespace TwoStrokeEngineSimulator.TwoStrokeEngine
{
    public interface IEngine
    {
        /// <summary>
        /// Set a random RPM to engine
        /// </summary>
        void SetRandomRPM();

        /// <summary>
        /// Print engine current RPM
        /// </summary>
        void PrintRPM();

        /// <summary>
        /// RPM
        /// </summary>
        public int RPM { get; set; }
    }

    public class Engine : IEngine
    {
        public int RPM { get; set; }


        private Random _random;
        private int _maxRPM;

        public Engine(int maxRPM)
        {
            if (maxRPM < 1)
                throw new Exception("Engine max RPM is invalid");

            _random = new Random();
            _maxRPM = maxRPM;
            SetRandomRPM();
        }


        public void SetRandomRPM()
        {
            RPM = _random.Next(1,_maxRPM);
        }

        public void PrintRPM()
        {
            Console.WriteLine(RPM + " rpm");
        }
        
    }
}
