using System;
using System.Collections.Generic;
using System.IO;
using NemoScript;

namespace NS_KIDE
{
    class KIDE // Kind of Integrated Developer Environment
    {
        public List<string> Code { get; } = new List<string>();

        static void Main()
        {
            KIDE kide = new KIDE();
            Console.WriteLine("NemoScript KIDE 2025");
            kide.StartKIDE();
        }

        public void StartKIDE()
        {
            while (true)
            {
                Console.Write($"{Code.Count + 1} > ");
                string UserInput = Console.ReadLine();

                int IndexOfSpace = UserInput.IndexOf(' ');
                if (IndexOfSpace == -1)
                {
                    switch (UserInput)
                    {
                        case "run":
                            Compiler compiler = new Compiler(Code);
                            compiler.Compile();
                            break;
                        case "list":
                            int i = 1;
                            foreach(string c in Code)
                            {
                                Console.WriteLine($"{i} > {c}");
                                i++;
                            }
                            break;
                        case "new":
                            Code.Clear();
                            break;
                        case "clear":
                            Console.Clear();
                            break;
                        default:
                            Code.Add(UserInput);
                            break;
                    }
                } else
                {
                    string Command = UserInput.Substring(0, IndexOfSpace);
                    string Args = UserInput.Substring(IndexOfSpace + 1);

                    switch (Command)
                    {
                        case "save":
                            string FileNameSAVE = Args.Replace(@"""", "");
                            File.WriteAllLines($"{FileNameSAVE}.nemo", Code);
                            break;
                        case "load":
                            try
                            {
                                string FileNameLOAD = Args.Replace(@"""", "");
                                Code.Clear();
                                foreach (string f in File.ReadAllLines($"{FileNameLOAD}.nemo"))
                                {
                                    Code.Add(f);
                                }
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                Console.WriteLine($"{Args} not found.");
                            }
                            break;
                        case "run":
                            try
                            {
                                string FileNameRUN = Args.Replace(@"""", "");
                                List<string> CodeFromFile = new List<string>();
                                foreach (string f in File.ReadAllLines($"{FileNameRUN}.nemo"))
                                {
                                    CodeFromFile.Add(f);
                                }
                                Compiler compiler = new Compiler(CodeFromFile);
                                compiler.Compile();
                            } 
                            catch(System.IO.FileNotFoundException)
                            {
                                Console.WriteLine($"{Args} not found.");
                            }
                            break;
                        case "remove":
                            {
                                int NumArgs = Convert.ToInt32(Args) - 1;
                                if (NumArgs > Code.Count)
                                {
                                    Console.WriteLine($"Line {NumArgs} does not exist in current context.");
                                }
                                else
                                {
                                    Code.RemoveAt(NumArgs - 1);
                                }
                                break;
                            }
                        case "edit":
                            {
                                int NumArgs = Convert.ToInt32(Args) - 1;
                                if (NumArgs > Code.Count)
                                {
                                    Console.WriteLine($"Line {NumArgs} does not exist in current context.");
                                }
                                else
                                {
                                    Console.Write($"{Args} > ");
                                    Code[NumArgs] = Console.ReadLine();
                                }
                            }
                            break;
                        default:
                            Code.Add(UserInput);
                            break;
                    }
                }
            }
        }
    }
}