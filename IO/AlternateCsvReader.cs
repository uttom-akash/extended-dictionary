using System.IO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using dictionary_learner.data_structure;
using System.Text.RegularExpressions;

namespace dictionary_learner.io{
    class AlternateCsvReader
    {
        public  StreamReader reader;
        public AlternateCsvReader(string path){
            reader=new StreamReader(path);
        }
        
        public async Task<List<Info>> ReadCSV(){

            List<Info> csv=new List<Info>();
            List<Task<Info>> tasks=new List<Task<Info>>();

            while (!reader.EndOfStream)
            {
                    var line=await reader.ReadLineAsync();
                    if(line.Length>0)
                        tasks.Add(Task.Run(()=>NormalizeLine(line)));
            }

            var resolvedTasks=await Task.WhenAll(tasks);
            csv=new List<Info>(resolvedTasks);
            reader.Close();
            return csv;
        }



        public Info NormalizeLine(string line){
            
            Info info=new Info("","","");

            string pattern="\"";
            char[] separator = {'(', ')'}; 

            line= Regex.Replace(line, pattern, string.Empty);
            
            var columns=line.Split(separator);
            int length=columns.Length;
           
            for (int index = 0; index <length; index++)
            {
                string wordPart=(columns.GetValue(index) as string).Trim().ToLower();
                switch (index)
                {
                    case 0: info.word=wordPart;break;
                    case 1: info.partOfSpeech=wordPart;break;
                    default: info.meaning+=wordPart;break;
                }
            }
            return info;
        }  
    }
}