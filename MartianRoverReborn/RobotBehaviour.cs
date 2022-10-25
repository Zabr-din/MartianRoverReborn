using System;
using System.Collections.Generic;
using System.Linq;
using MartianRoverReborn.Interfaces;
using MartianRoverReborn.Models;

namespace MartianRoverReborn
{
    public class RobotBehaviour : IRobotBehaviour
    {
        private void RotateRight(RobotModel robot)
        {
            switch (robot.Position.Direction)
            {
                case Directions.N:
                    robot.Position.Direction = Directions.E;
                    break;
                case Directions.E:
                    robot.Position.Direction = Directions.S;
                    break;
                case Directions.S:
                    robot.Position.Direction = Directions.W;
                    break;
                case Directions.W:
                    robot.Position.Direction = Directions.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void RotateLeft(RobotModel robot)
        {
            switch (robot.Position.Direction)
            {
                case Directions.N:
                    robot.Position.Direction = Directions.W;
                    break;
                case Directions.W:
                    robot.Position.Direction = Directions.S;
                    break;
                case Directions.S:
                    robot.Position.Direction = Directions.E;
                    break;
                case Directions.E:
                    robot.Position.Direction = Directions.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void MoveForward(RobotModel robot)
        {
            switch (robot.Position.Direction)
            {
                case Directions.N:
                    robot.Position.Y += 1;
                    break;
                case Directions.S:
                    robot.Position.Y -= 1;
                    break;
                case Directions.E:
                    robot.Position.X += 1;
                    break;
                case Directions.W:
                    robot.Position.X -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void Move(Commands command, List<Position> blackLIst, RobotModel robot)
        {
            switch (command)
            {
                case Commands.R:
                    RotateRight(robot);
                    break;
                case Commands.L:
                    RotateLeft(robot);
                    break;
                case Commands.F:
                    {
                        var blackMarker = blackLIst.Any(blackPosition =>
                            robot.Position.X == blackPosition.X && robot.Position.Y == blackPosition.Y &&
                            robot.Position.Direction == blackPosition.Direction);
                        if (!blackMarker)
                        {
                            MoveForward(robot);
                        }
                        
                        break;
                    }
                    
                default:
                    throw new ArgumentException("Command is not valid");
                    
            }
        }
    }
}