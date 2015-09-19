using System;
using System.Linq;
using System.Collections.Generic;
namespace CommandParser
{
    public class Program
    {
        static void Main(string[] args)
        {

            var quit = false;
            //start with args
            quit = Parse(args);
            // main loop
            while (quit == false)
            {
             String[] words = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
             quit = Parse(words);
            }
        }
        public static bool Parse(string[] words)
        {
            
            //current word
            string CurrentWord;
            // int for count params fo option -k and -print
            int countparams;
            // int for while
            int i = 0;
            // list of all commands
            List<string> listcommands = new List<string>(new string[] {"-PING","-PRINT","-K","/?","/HELP","-HELP","-QUIT" });
            
            if (words.Length == 0)
            {
                PrintHelp();
                return false;
            }
            else
            {
                while (i<=words.Length)
                {
                    CurrentWord = words[i].ToUpper();
                    countparams = 0;
                    switch (CurrentWord)
                    {

                        case "-PING":
                            {
                                WriteLineColor("Pinging...");
                                for (int j = 100; j <= 700; j += 50)
                                {
                                    Console.Beep(j, 100);
                                }
                                if (words.Skip(i + 1).ToArray().Length == 0) return false;
                                else i++;
                                break;
                            }
                        case "-PRINT":
                            {
                                // count how many params are
                                countparams = GetNumberParams(i, words, listcommands);
                                // print params
                                WriteLineColor(GetParams(i, countparams, words, CurrentWord));
                                if (words.Skip(i+countparams + 1).ToArray().Length == 0) return false;
                                else i=i+countparams+1;
                                break;
                            }

                        case "-K":
                            {
                                // count how many params are
                                countparams = GetNumberParams(i, words, listcommands);
                                // print params
                                WriteLineColor(GetParams(i, countparams, words, CurrentWord));
                                if (words.Skip(i+countparams + 1).ToArray().Length == 0) return false;
                                else i = i + countparams+1;
                                break;
                            }
                        // after find help command ignore another commands - don't call Parser() 
                        case "/?":
                        case "/HELP":
                        case "-HELP": PrintHelp(); return false;
                        case "-QUIT": return true;
                        // default: command is not supported 
                        default: WriteLineColor("Command " + CurrentWord + " is not supported, use /? to see set of allowed commands"); return false;

                    }
                }
                return false;  
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
            WriteLineColor("or '-quit' to exit ");
        }

        public static int GetNumberParams(int i, string[] words, List<string> listcommands)
        {
            //
            // This method get number of paramtres for -print and -k
            //
            int countparams = 0;
            while (i + countparams < words.Length - 1 && !listcommands.Contains(words[i + countparams + 1].ToUpper()))
            {
                countparams++;
            }
            return countparams;
        }
        public static string GetParams(int i, int countparams,string[] words,string cmd_key)
        {
            string ReturnString="";
            if (countparams == 0) 
            ReturnString=cmd_key+": Nothing to print! You don't input parameters.";
            else
                if (cmd_key == "-PRINT") ReturnString=string.Join(" ", words.Skip(i + 1).Take(countparams).ToArray());
                else
                {
                    for (int j = 0; j < countparams; j = j + 2)
                    {
                        if (j + 1 < countparams)
                        {
                            ReturnString = ReturnString + words[i + j + 1] + " - " + words[i + j + 2];
                            if (j + 2 != countparams) ReturnString = ReturnString + "\n";
                        }
                        else ReturnString = ReturnString + words[i + j + 1] + " - null";

                    }
                }
            return ReturnString;
            
        }


    }
    
}