using System;
using System.Linq;
using System.Collections.Generic;
namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var quit = false;
            //start with args
            List<string> argslist = new List<string>(args);
            argslist.Insert(0, "COMMANDPARSER.EXE");
            Parse(argslist.ToArray(), "");
            // main loop
            while (quit == false)
            {
             String[] words = new String[0];
             words=Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
             quit = Parse(words,"");
            }
        }
        public static bool Parse(string[] words, string currentcmd)
        {
            
            //current word
            string CurrentWord;
            // int for count params fo option -k
            int countparams=0;
            // list of all commands
            List<string> listcommands = new List<string>(new string[] {"-PING","-PRINT","-K","/?","/HELP","-HELP" });
           
            if (words.Length == 0) CurrentWord = ""; 
            else CurrentWord = words[0].ToUpper();

            switch (CurrentWord)
            {
                case "QUIT": return true;
                case "COMMANDPARSER.EXE":
                    {
                        if (words.Length==1)
                        {
                            PrintHelp(); return false;
                        }
                        else
                        {
                            Parse(words.Skip(1).ToArray(), "COMMANDPARSER.EXE"); return false;
                        }
                        
                    }
                default:
                    {    
                    switch (currentcmd)
                    {
                        case "":
                            {
                                 WriteLineColor("Your input does not contain the name of the program");
                                 PrintHelp();
                                 break;

                            }
                        case "COMMANDPARSER.EXE":
                            {
                                switch (CurrentWord)
                                {

                                    case "-PING":
                                        {
                                            WriteLineColor("Pinging...");
                                            for (int i = 100; i <= 700; i += 50)
                                            {
                                                Console.Beep(i, 100);
                                            }
                                            Parse(words.Skip(1).ToArray(), "COMMANDPARSER.EXE");
                                            break;
                                        }
                                    case "-PRINT": Parse(words.Skip(1).ToArray(), "-PRINT"); break;
                                    case "-K": Parse(words.Skip(1).ToArray(), "-K"); break;
                                     // after find help command ignore another commands - don't call Parser() 
                                    case "": break;
                                    // if "" the end of comand and parameters 
                                    case "/?":
                                    case "/HELP":
                                    case "-HELP": PrintHelp(); break;
                                    // default: command is not supported 
                                    default: WriteLineColor("Command " + CurrentWord + " is not supported, use CommandParser.exe /? to see set of allowed commands"); break;
                                }
                                break;
                            }
                        case "-PRINT":
                            {
                                //check if no params=next command
                                if (listcommands.Contains(CurrentWord))
                                {
                                    WriteLineColor("-PRINT: Nothing to print! You don't input parametr.");
                                    Parse(words, "COMMANDPARSER.EXE");
                                }
                                else
                                {
                                    WriteLineColor(CurrentWord); Parse(words.Skip(1).ToArray(), "COMMANDPARSER.EXE");
                                }
                                break; 
                            }
    
                        case "-K":
                            {
                                // count how many params are
                                while (countparams< words.Length && !listcommands.Contains(words[countparams].ToUpper()))
                                {
                                    countparams++;
                                }
                                if (countparams == 0) WriteLineColor("-K: Nothing to print! You don't input key and value.");
                                for (int i = 0; i < countparams;i=i+2)
                                {
                                    if (i + 1 < countparams) WriteLineColor(words[i] + " - " + words[i + 1]);
                                    else WriteLineColor(words[i] + " - null");
                                      
                                }
                                Parse(words.Skip(countparams).ToArray(), "COMMANDPARSER.EXE");
                                break;
                            }
                        
                    }
                    return false;
                }
                  
            }
           

        }
        static void WriteLineColor(string value)
        {
            //
            // This method writes answer to the console with the specific colors.
            //
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            //
            // Reset the color.
            //
            Console.ResetColor();
        }
        static void PrintHelp()
        {
            //
            // This method writes help to the console.
            //
            WriteLineColor("Syntax of command:");
            WriteLineColor("CommandPareser.exe [/?] [/help] [-help] [-k key value] [-ping] [-print <print a value>]");
            WriteLineColor("[/?] [/help] [-help] : print help");
            WriteLineColor("[-k key value] : print table key-value");
            WriteLineColor("[-ping] : beep and print 'Pinging...'");
            WriteLineColor("[-print <print a value>] : print value");
            WriteLineColor("or 'quit' to exit ");
        }

    }
    
}