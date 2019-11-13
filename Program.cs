using System;
using dictionary_learner.data_structure;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using dictionary_learner.io;
using System.Threading.Tasks;
using System.Linq;

namespace dictionary_learner
{
    class Program
    {
        private static Tree tree=new Tree(30,"./assets/tree.txt");
        static async Task  Main(string[] args)
        {

            
            while(true){
                Console.ForegroundColor=ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("Commands :");
                Console.WriteLine("read : read the csv");
                Console.WriteLine("load : load tree");
                Console.WriteLine("save : save tree");
                Console.WriteLine("insert word: insert a 'word'");
                Console.WriteLine("search word: search 'word'");
                Console.WriteLine("auto word : prefix search 'word'");
                
                
                Console.Write(">");
                string command=Console.ReadLine();
                string[] commands=command.Split(' ');
                    if(commands[0]=="read"){
                        await initialize();
                    }
                    else if(commands[0]=="load"){
                        tree.load();
                    }
                    else if(commands[0]=="save"){
                        tree.store();
                    }
                    else if(commands[0]=="insert"){
                        var it=commands.GetEnumerator();
                        it.MoveNext();
                        it.MoveNext();
                        var word=it.Current as string;
                        it.MoveNext();
                        var partOfSpeech=it.Current as string;
                        it.MoveNext();
                        var meaning=it.Current as string;
                        
                        tree.insert(word,partOfSpeech,meaning);

                    }else if(commands[0]=="search"){
                        Console.WriteLine(tree.search(commands[1]));
                    }else if(commands[0]=="auto"){
                        List<Info> words=tree.findWordsWithPrefix(commands[1]);
                        foreach (var word in words)
                        {
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.Write(word.word);
                            Console.ForegroundColor=ConsoleColor.DarkGray;
                            Console.Write($" ({word.partOfSpeech}) ");
                            Console.ForegroundColor=ConsoleColor.White;
                            Console.WriteLine($"   -> {word.meaning}");
                            Console.ResetColor();
                            Console.WriteLine();
                            
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else{
                        break;
                    }
            }
            Console.ResetColor();
        }


        static async Task initialize(){
             CSVReader cSVReader=new CSVReader("./assets/dictionary.csv");
             List<IEnumerable<string>> csv=await cSVReader.readCSV();
                foreach (var line in csv)
                {  
                
                    var enumrator=line.GetEnumerator();

                    enumrator.MoveNext();
                    var word=enumrator.Current;
                    
                    enumrator.MoveNext();
                    var partOfSpeech=enumrator.Current;
                    
                    enumrator.MoveNext();
                    var meaning=enumrator.Current;
                    word=word.ToLower();
                    Console.WriteLine(word);
                    tree.insert(word,partOfSpeech,meaning);     
                }
        }
        static void processCommand(string command){
               Queue<string>tokens=new Queue<string>();

               bool previousWhitespace=false;
               command=command+" ";
               string word="";
               foreach (var character in command)
               {
                   if(char.IsWhiteSpace(character))
                   {
                       if(previousWhitespace)continue;
                       tokens.Enqueue(word); 
                       previousWhitespace=true;
                       continue;
                   }
                   previousWhitespace=false;
                   word=word+character;
               }
        }
    }
}
