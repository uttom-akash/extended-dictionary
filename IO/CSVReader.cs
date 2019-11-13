using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dictionary_learner.io{
    class CSVReader{

        public  StreamReader reader;
        public CSVReader(string path){
            reader=new StreamReader(path);
        }
        public async Task<List<IEnumerable<string>>> readCSV(){
            List<IEnumerable<string>> csv=new List<IEnumerable<string>>();
            string pattern="\"";
            while (!reader.EndOfStream)
            {
                    var line=await reader.ReadLineAsync();
                    line= Regex.Replace(line, pattern, string.Empty);
                    var splits=line.Split(',');
                    csv.Add(new List<string>(splits));
            }
            reader.Close();
            return csv;
        } 
    }
}