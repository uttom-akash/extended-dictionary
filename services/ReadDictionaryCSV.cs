
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dictionary_learner.data_structure;
using dictionary_learner.io;
using dictionary_learner.token;
using System.Linq;

namespace dictionary_learner.services{
    class ReadDictionaryCSV{

         public static async Task Read(Tree tree,FunctionalToken token){

                if(token.Words.Count>0){
                    await Reset(tree,token.Words.FirstOrDefault());
                    Console.ForegroundColor=ConsoleColor.Blue;
                    Console.WriteLine("read successfully !!");
                }
                else {
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("read [csv path]");
                }
         }
         public static async Task Reset(Tree tree,string path="./assets/dictionary.csv"){
             CSVReader cSVReader=new CSVReader(path);
             List<IEnumerable<string>> csv=await cSVReader.ReadCSV();
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
                    tree.Insert(word,partOfSpeech,meaning);     
                }
        }    
    }
}