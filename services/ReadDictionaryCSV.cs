
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
         public static async Task Reset(Tree tree,string path="./assets/alternateDictionary.csv"){
             AlternateCsvReader cSVReader=new AlternateCsvReader(path);
             List<Info> csv=await cSVReader.ReadCSV();
   
            foreach (var info in csv)
            {  
                tree.Insert(info.word,info.partOfSpeech,info.meaning);     
            }
        }    
    }
}