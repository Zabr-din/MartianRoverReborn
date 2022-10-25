using System;
using System.Collections.Generic;
using MartianRoverReborn.Interfaces;
using MartianRoverReborn.Models;

namespace MartianRoverReborn
{
    internal sealed class MartianManager : IMartianManager
    {
        private Surface Surface { get; set; }
        private List<RobotModel> Robots { get; set; }
        private List<List<Commands>> RobotsCommands { get; set; }
        private List<Position> BlackListPositions { get; set; }

        private Position CurrentPosition { get; set; }

        private readonly IRobotBehaviour _robotBehaviour;
        
        public MartianManager(IRobotBehaviour robotBehaviour)
        {
            _robotBehaviour = robotBehaviour;
        }

        public string GetOutput()
        {
            var output = "";
            foreach (var robot in Robots)
            {
                output += robot.Position.ToString();
                if (robot.IsLost)
                {
                    output += " LOST\n";
                }
                else
                {
                    output += "\n";
                }
            }

            return output;
        }

        public void RunRobot()
        {
            BlackListPositions = new List<Position>();

            for (var i = 0; i < Robots.Count; i++)
            {
                foreach (var command in RobotsCommands[i])
                {
                    SaveCurrentPosition(Robots[i].Position);
                    _robotBehaviour.Move(command, BlackListPositions, Robots[i]);
                    if (Robots[i].Position.X > Surface.XAxisMax || Robots[i].Position.X < 0
                                                                || Robots[i].Position.Y > Surface.YAxisMax ||
                                                                Robots[i].Position.Y < 0)
                    {
                        Robots[i].IsLost = true;
                        Robots[i].Position = CurrentPosition;
                        BlackListPositions.Add(CurrentPosition);
                        break;
                    }
                }
            }
        }

        private void SaveCurrentPosition(Position p)
        {
            CurrentPosition = new Position(p.X, p.Y, p.Direction);
        }

        public void InitializeVariables(List<string> input)
        {
            Surface = new Surface(
                Convert.ToInt32(input[0].Split(' ')[0]),
                Convert.ToInt32(input[0].Split(' ')[1]));

            Robots = new List<RobotModel>();


            RobotsCommands = new List<List<Commands>>();


            for (var i = 1; i < input.Count; i += 2)
            {
                var directions = new Directions();
                switch (input[i].Split(' ')[2].ToUpper())
                {
                    case ("N"):
                        directions = Directions.N;
                        break;
                    case ("E"):
                        directions = Directions.E;
                        break;
                    case ("S"):
                        directions = Directions.S;
                        break;
                    case ("W"):
                        directions = Directions.W;
                        break;
                } //Set a robot direction

                var robot = new RobotModel
                {
                    Position = new Position
                    (
                        Convert.ToInt32(input[i].Split(' ')[0]),
                        Convert.ToInt32(input[i].Split(' ')[1]),
                        directions
                    )
                };

                Robots.Add(robot);

                var commandLine = new List<Commands>();
                foreach (var item in input[i + 1].ToUpper().ToCharArray())
                {
                    switch (item)
                    {
                        case ('R'):
                            commandLine.Add(Commands.R);
                            break;
                        case ('L'):
                            commandLine.Add(Commands.L);
                            break;
                        case ('F'):
                            commandLine.Add(Commands.F);
                            break;
                    } //Commands parse
                }

                RobotsCommands.Add(commandLine);
            }
        }
    }
}