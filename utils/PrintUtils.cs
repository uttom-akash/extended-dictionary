using System;
using System.Collections.Generic;
using System.Threading;
using dictionary_learner.data_structure;

namespace dictionary_learner.utils
{
    class PrintUtils{
        public static bool loading=false;

        public static void printLoading(object message){
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine($"{message} ....");
            Console.ResetColor();
        }
        
        public static void PrintWords(List<Info> infos){
            foreach (var info in infos)
            {
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.WriteLine();
                Console.Write(info.word);
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.Write($" ({info.partOfSpeech}) ->");
                Console.ForegroundColor=ConsoleColor.Gray;
                Console.WriteLine($"  {info.meaning}");
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        public static void ManualPage(){
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Manual");
                Console.WriteLine();
                Console.ForegroundColor=ConsoleColor.White;
                Console.WriteLine("command                                   :    usage");
                Console.WriteLine();
                Console.WriteLine("search [word]                             :    search a word");
                Console.WriteLine("search -tv [word]                         :    search a word with vowel tollarance");
                Console.WriteLine("suggest [word]                            :    suggestion of words starting [word]");
                Console.WriteLine("suggest -l [l] [word]                     :    suggestion of words starting [word] within limit(l)");
                Console.WriteLine("suggest -l [l] -tv [word]                 :    suggestion of words starting [word] within limit(l) with tollarance");
                Console.WriteLine("insert  [word] [part of speeach] [meaning]:    insert a word");
                Console.WriteLine("reset                                     :    reset dictionary");
                Console.WriteLine("read [path]                               :    read custom csv");
                Console.WriteLine("reload                                    :    reload dictionary");
                Console.WriteLine("save                                      :    save current dictionary");
                Console.WriteLine();
                Console.ResetColor();
        }

        public static void HelpHeader(){
                Console.ForegroundColor=ConsoleColor.Cyan;
                Console.WriteLine();
                Console.WriteLine("help : show manual page");
                Console.WriteLine();
                Console.ResetColor();
                
        }

        public static void CliPointer(){
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.Write(">"); 
            Console.ResetColor(); 
        }

        public static void intro(){

        }

        public static void PrintFoundHeader(int numberOfWords){
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine($"{numberOfWords} words found");
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void PrintNoWordFound(string word){
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine($"Extremely sorry,'{word}' doesn't exist in dictionary");
            Console.WriteLine();
            Console.ResetColor();        
        }


        public static void PrintTolaratedSearchHeader(int numberOfWords){
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine($"{numberOfWords} words found with tolarated search!!");
            Console.WriteLine();
            Console.ResetColor();    
        }


        public static void PrintFooter(){
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"you can always add word: insert [word] [part of speeach] [meaning]");
            Console.WriteLine();
            Console.ResetColor();    
        }

        public static void PrintError(List<string> errors){
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine();
            foreach (var error in errors)
            {   
                Console.WriteLine(error);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }    
}