using System;
using MartianRoverReborn.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRoverReborn
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IMartianManager, MartianManager>()
                    .AddSingleton<IInputManager, InputManager>()
                    .AddSingleton<IRobotStart, RobotStart>()
                    .AddSingleton<IRobotBehaviour, RobotBehaviour>()
                    .BuildServiceProvider();

                var bar = serviceProvider.GetService<IRobotStart>();
                bar?.RunTask();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}