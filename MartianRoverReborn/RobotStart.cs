using System;
using MartianRoverReborn.Interfaces;

namespace MartianRoverReborn
{
    public class RobotStart : IRobotStart
    {
        private readonly IMartianManager _martianManager;
        private readonly IInputManager _inputManager;

        public RobotStart(IMartianManager martianManager, IInputManager inputManager)
        {
            _martianManager = martianManager;
            _inputManager = inputManager;
        }

        public void RunTask()
        {
            _inputManager.TypeInfo();
            _martianManager.InitializeVariables(_inputManager.GetInputStrings());

            _martianManager.RunRobot();
            
            Console.WriteLine(_martianManager.GetOutput());
        }
    }
}