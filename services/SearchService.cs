using System;
using System.Collections.Generic;
using dictionary_learner.data_structure;
using dictionary_learner.token;
using dictionary_learner.utils;

namespace dictionary_learner.services{
    class SearchService{

        public static void search(Tree  tree,FunctionalToken token){

            var options=token.Options;
            if(options.ContainsKey(TokenKind.VowelTollrance)){
                SearchTolaratingVowel(tree,token.Words);
            }
            else{
                ExactSearch(tree,token.Words);
            }
        }

        public static void ExactSearch(Tree  tree,List<string> words){
            foreach (var word in words)
            {
                var info=tree.ExactSearch(word);
                
                if(info.word.Length>0){
                    PrintUtils.PrintFoundHeader(1);
                    PrintUtils.PrintWords(new List<Info>{info});
                }else
                {
                    PrintUtils.PrintNoWordFound(word);
                    TolarateVowel(tree,word);
                }  
            } 
        }

        public static void SearchTolaratingVowel(Tree tree,List<string> words){
                foreach (var word in words)
                {
                    TolarateVowel(tree,word); 
                } 
        }

        public static void TolarateVowel(Tree tree,string word){
                Console.ResetColor();
                List<Info> words=tree.TolarateVowelSearch(word);

                PrintUtils.PrintTolaratedSearchHeader(words.Count);
                PrintUtils.PrintWords(words);
        }

    }
}