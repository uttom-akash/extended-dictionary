
using System;
using System.Collections.Generic;
using dictionary_learner.data_structure;
using dictionary_learner.token;
using dictionary_learner.utils;


namespace dictionary_learner.services
{
    class SuggestionService
    {
        public static void suggest(Tree  tree,FunctionalToken token){
            var options=token.Options;
            int limit=10;
            if(options.ContainsKey(TokenKind.Limit))
                limit=options[TokenKind.Limit];
            
            if(options.ContainsKey(TokenKind.VowelTollrance))
            {
                PrefixSuggestionWithVowelTollarance(tree,token.Words,limit);
            }else{
                PrefixSuggestion(tree,token.Words,limit);
            }
        }

        public static void PrefixSuggestionWithVowelTollarance(Tree tree,List<string> words,int limit=10){
                foreach (var word in words)
                {
                    var foundWords=tree.FindWordsWithPrefixTolaratingeVowel(word,limit);
                    PrintUtils.PrintFoundHeader(foundWords.Count);
                    PrintUtils.PrintWords(foundWords);
                       
                }
        }

                
        public static void PrefixSuggestion(Tree tree,List<string> words,int limit){
            foreach (var word in words)
            {
                List<Info> foundWords=new List<Info>();
                foundWords=tree.FindWordsWithPrefix(word,limit);
                
                Console.ForegroundColor=ConsoleColor.DarkGreen;
                Console.WriteLine($"{foundWords.Count} words found !!");
                Console.WriteLine();
                PrintUtils.PrintWords(foundWords);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }    
}
