using System.Collections.Generic;

namespace dictionary_learner.error{
    class ErrorBag
    {
        public static List<string> errors=new List<string>();


        public static void ReportError(string error){
                errors.Add(error);
        }

        public static void ClearBag(){
            errors.Clear();
        }     
    }
}