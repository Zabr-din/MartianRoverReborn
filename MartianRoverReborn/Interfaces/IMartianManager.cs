using System.Collections.Generic;

namespace MartianRoverReborn.Interfaces
{
    public interface IMartianManager
    {
        void InitializeVariables(List<string> input);

        void RunRobot();

        string GetOutput();
    }
}