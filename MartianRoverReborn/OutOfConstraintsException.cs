using System;

namespace MartianRoverReborn
{
    public class OutOfConstraintsException : Exception
    {
        public OutOfConstraintsException(string message)
       : base(message)
        { }

    }
}
