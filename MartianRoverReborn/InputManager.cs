using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MartianRoverReborn.Interfaces;

namespace MartianRoverReborn
{
    internal sealed class InputManager : IInputManager
    {
        private readonly List<Regex> _regxs;
        private readonly string [] _exampleText;

        public InputManager()
        {
            _regxs = new List<Regex>()
            {
                new Regex(@"^\d{1,2}\ \d{1,2}"),
                new Regex(@"^\d{1,2}\ \d{1,2}\ [NSEW]", RegexOptions.IgnoreCase),
                new Regex(@"^[RLF]+", RegexOptions.IgnoreCase)
            };
            _exampleText = System.IO.File.ReadAllLines("../../Resources/Example.txt");

        }


        public void TypeInfo()
        {
            Console.Clear();

            Console.WriteLine("1 line: the upper-right coordinates [n m]\n" +
                "2,4,6... lines: robot n position [n m direction{NSEW}]\n" +
                "3,5,7... lines: robot n instructions {RLF}\n" +
                "Upper/lower cases are ignored\n" +
                "Type \"run\" to run the application\n" +
                "or \"test\" to start with example settings\n" +
                "\nExample:"
                );
            foreach (var item in _exampleText)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
           
        }

        public List<string> GetInputStrings()
        {
            TypeInfo();
            var output = new List<string>();


            while (true)
            {
                try
                {
                    var types = Console.ReadLine();

                    if (types.ToLower() == "test") 
                    {
                        output = new List<string>(_exampleText);
                        break;
                    }
                    if (types != null && types.ToLower() != "run")
                    {
                        string input;
                        if (output.Count == 0)
                        {
                            input = GetLine(types, _regxs[0]); //1 line

                            output.Add(input);
                        }
                        else
                        {
                            switch (output.Count % 2)
                            {
                                case (1):
                                    input = GetLine(types, _regxs[1]); //2,4,6 line

                                    output.Add(input);
                                    break;
                                case (0):
                                    input = GetLine(types, _regxs[2]); //3,5,7 line

                                    output.Add(input);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (output.Count >= 3)
                        {
                            break;
                        }

                        switch (output.Count)
                        {
                            case (0):
                                Console.WriteLine("You should type surface size first");
                                break;
                            default:
                                Console.WriteLine("There must be at least one robot and one commands line");
                                break;
                        }
                    }
                }
                catch (ArgumentException aex)
                {
                    Console.WriteLine(aex.Message);
                }
            }

            return output;
        }

        private static string GetLine(string input, Regex r)
        {
            input = input.Trim();
            if (r.IsMatch(input) && input.Length <= 100)
            {
                if (input.Length > r.Match(input).Length)
                {
                    Console.WriteLine(
                        $"Do you mean this?: {input.Substring(0, r.Match(input).Length)}\n(type y or press Enter as Yes/n or whatewer else as No)");

                    var input2 = Console.ReadLine();
                    if (input2 != null && (input2.ToLower() == "y" || input2 == ""))
                    {
                        input = input.Substring(0, r.Match(input).Length);
                        Console.WriteLine(input);
                        return input;
                    }

                    if (input2.ToLower() == "n")
                    {
                        throw new ArgumentException("Input string is not valid");
                    }

                    throw new ArgumentException("Input string is not valid");
                }
                
                return input;
            }

            throw new ArgumentException("Input string is not valid");
        }
    }
}