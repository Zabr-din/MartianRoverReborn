using System.Collections.Generic;
using MartianRoverReborn.Models;

namespace MartianRoverReborn.Interfaces
{
    public interface IRobotBehaviour
    {
        void Move(Commands command, List<Position> blackLIst, RobotModel robot);
    }
    
}