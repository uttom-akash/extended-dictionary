using System;
using dictionary_learner.data_structure;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using dictionary_learner.io;
using System.Threading.Tasks;
using System.Linq;
using dictionary_learner.services;
using dictionary_learner.utils;
using dictionary_learner.token;
using dictionary_learner.error;
using System.Threading;

namespace dictionary_learner
{
    class Program
    {
        
        private static Tree tree=new Tree(30,"./assets/tree.txt");
        static async Task  Main(string[] args)
        {
            Thread loading=new Thread(tree.Load);
            loading.Start();

            PrintUtils.printLoading("starting");

            while(true){
                loading.Join();

                PrintUtils.HelpHeader();
                PrintUtils.CliPointer();
    
                string command=Console.ReadLine();
                if(command.Equals("exit"))
                    break;



                Lexer lexer=new Lexer(command);    
                FunctionalToken token=lexer.Lex();
                if(token.Kind==TokenKind.Invalid)
                    ErrorBag.ReportError($"Invalid functional keyword {token}.");


                if(ErrorBag.errors.Count>0){
                    PrintUtils.PrintError(ErrorBag.errors);
                }else{
                    await handleCommand(token);
                }

                
                ErrorBag.ClearBag();   
            }
            tree.Store();
        }


        public static async Task handleCommand(FunctionalToken token){

                switch (token.Kind)
                {
                    case TokenKind.Help:PrintUtils.ManualPage();break;
                    case TokenKind.Clear:Console.Clear();break;
                    case TokenKind.Read: await ReadDictionaryCSV.Read(tree,token);break;
                    case TokenKind.Reset:await ReadDictionaryCSV.Reset(tree);break;
                    case TokenKind.Reload:tree.Load();break;
                    case TokenKind.Save:tree.Store();break;
                    case TokenKind.Insert:TeachService.insertToDictionary(tree,token);break;
                    case TokenKind.Search:SearchService.search(tree,token);break;
                    case TokenKind.Suggest: SuggestionService.suggest(tree,token);break;
                }
        }

        

        

       
        
    }
}
