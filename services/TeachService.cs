using System;
using dictionary_learner.data_structure;
using dictionary_learner.token;

namespace dictionary_learner.services{
    class TeachService{
        public static void insertToDictionary(Tree tree,FunctionalToken token){
                var it=token.Words.GetEnumerator();
                it.MoveNext();
                var word=it.Current as string;
                it.MoveNext();
                var partOfSpeech=it.Current as string;
                it.MoveNext();
                var meaning=it.Current as string;
                Console.WriteLine($"{word} {partOfSpeech} {meaning}");
                tree.Insert(word,partOfSpeech,meaning);
        }
    }
}