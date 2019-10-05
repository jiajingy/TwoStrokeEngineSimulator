using System;
using System.Collections.Generic;
using System.Text;

namespace TwoStrokeEngineSimulator.TwoStrokeEngine
{
    public interface IPiston
    {

        /// <summary>
        /// Get Piston State
        /// </summary>
        /// <returns></returns>
        PistonState GetState();

        /// <summary>
        /// Get stroke (inch)
        /// </summary>
        /// <returns></returns>
        double GetStroke();


        /// <summary>
        /// Get speed (MPH)
        /// </summary>
        /// <returns></returns>
        double GetSpeed();


        /// <summary>
        /// Update piston states
        /// </summary>
        /// <param name="traveledDistance"></param>
        /// <param name="speed"></param>
        void UpdatePistonStates(double traveledDistance, double speed);
    }

    public class Piston : IPiston
    {
        

        private PistonState _state;
        private double _displacement;
        private double _curDisplacement;
        private double _speed;


        public Piston(double displacement)
        {
            if (displacement <= 0)
                throw new Exception("Piston stroke is invalid");

            // Set initial state of piston to bottom
            _state = PistonState.Bot;

            // Set position of piston
            _curDisplacement = -displacement/2;

            // Set speed
            _speed = 0;

            // Set stroke
            _displacement = displacement;

            
        }


      
        public PistonState GetState()
        {
            return _state;
        }

        public double GetStroke()
        {
            return _displacement;
        }

        public double GetSpeed()
        {
            return _speed;
        }

        public void UpdatePistonStates(double traveledDistance,double speed)
        {
            _curDisplacement += traveledDistance;
            _speed = speed;
            if (_curDisplacement >= _displacement/2)
            {
                _state = PistonState.Top;
                _curDisplacement = _displacement / 2;
                _speed = 0;
            }
            else if (_curDisplacement <= -1)
            {
                _state = PistonState.Bot;
                _curDisplacement = -_displacement / 2;
                _speed = 0;
            }
            else if (_curDisplacement >= 0 && _curDisplacement < _displacement / 2 && traveledDistance > 0)
                _state = PistonState.MidToTop;
            else if (_curDisplacement <= 0 && _curDisplacement > -_displacement / 2 && traveledDistance < 0)
                _state = PistonState.MidToBot;
            else if (_curDisplacement >= 0 && _curDisplacement < _displacement / 2 && traveledDistance < 0)
                _state = PistonState.TopToMid;
            else if (_curDisplacement <= 0 && _curDisplacement > -_displacement / 2 && traveledDistance > 0)
                _state = PistonState.BotToMid;

            

        }



    }


    /// <summary>
    /// Set 6 Piston States
    /// 1 - Top
    /// 2 - Move from top to middle
    /// 3 - Move from middle to bottom
    /// 4 - Bottom
    /// 5 - Move from bottom to mid
    /// 6 - Move from mid to top
    /// </summary>
    public enum PistonState
    {
        Top,
        TopToMid,
        MidToBot,
        Bot,
        BotToMid,
        MidToTop
    }
}
