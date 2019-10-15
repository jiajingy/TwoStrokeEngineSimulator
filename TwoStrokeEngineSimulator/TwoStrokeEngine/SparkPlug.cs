using System;
using System.Collections.Generic;
using System.Text;

namespace TwoStrokeEngineSimulator.TwoStrokeEngine
{
    public interface ISparkPlug
    {
        /// <summary>
        /// Turn on ignition
        /// </summary>
        void Ignition();


        /// <summary>
        /// Turn off ignition
        /// </summary>
        void StopIgnition();


        /// <summary>
        /// Get State
        /// </summary>
        /// <returns></returns>
        SparkPlugState GetState();
        
    }


    public class SparkPlug : ISparkPlug
    {
        

        private SparkPlugState _sparkPlugState;
        
        public SparkPlug()
        {
            _sparkPlugState = SparkPlugState.Off;
        }

        public void Ignition()
        {
            _sparkPlugState = SparkPlugState.On;
        }

        public void StopIgnition()
        {
            _sparkPlugState = SparkPlugState.Off;
        }

        public SparkPlugState GetState()
        {
            return _sparkPlugState;
        }
    }

    public enum SparkPlugState
    {
        Off,On
    }
}
