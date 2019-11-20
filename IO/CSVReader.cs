using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dictionary_learner.data_structure;

namespace dictionary_learner.io{
    class CSVReader{

        public  StreamReader reader;
        public CSVReader(string path){
            reader=new StreamReader(path);
        }
        public async Task<List<Info>> ReadCSV(){
            List<Info> csv=new List<Info>();
            List<Task<Info>> tasks=new List<Task<Info>>();

            while (!reader.EndOfStream)
            {
                    var line=await reader.ReadLineAsync();
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
            line= Regex.Replace(line, pattern, string.Empty);
            var columns=line.Split(',');
            int length=columns.Length;

            for (int index = 0; index <length; index++)
            {
                switch (index)
                {
                    case 0: info.word=(columns.GetValue(index) as string).ToLower();break;
                    case 1: info.partOfSpeech=(columns.GetValue(index) as string).ToLower();break;
                    default: info.meaning+=columns.GetValue(index) as string;break;
                }
            }
            return info;
        } 
    }
}