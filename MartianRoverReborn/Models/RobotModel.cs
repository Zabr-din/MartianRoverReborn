namespace MartianRoverReborn.Models
{
    public class RobotModel
    {
        public Position Position { get; set; }
        public bool IsLost { get; set; }
        public RobotModel(int x, int y, Directions direction)
        {
            Position = new Position(x, y, direction);
        }
        public RobotModel()
        {
            IsLost = false;
        }
    }
}