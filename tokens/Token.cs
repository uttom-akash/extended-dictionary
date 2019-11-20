namespace dictionary_learner.token{
    public abstract class Token
    {
        public TokenKind Kind {get;set;}
        public object Value {get;set;}
    }
}