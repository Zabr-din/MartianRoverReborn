using System.Collections.Generic;

namespace MartianRoverReborn.Interfaces
{
    public interface IInputManager
    {
        List<string> GetInputStrings();
        void TypeInfo();
    }
}