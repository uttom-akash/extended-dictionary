using System.Collections.Generic;

namespace dictionary_learner.token{
    public sealed class FunctionalToken :Token
    {
        public FunctionalToken(TokenKind kind,Dictionary<TokenKind,int> options,List<string> words){
            Options = options;
            Words = words;
            Kind=kind;
        }

        public Dictionary<TokenKind, int> Options { get; }
        public List<string> Words { get; }
    }
}