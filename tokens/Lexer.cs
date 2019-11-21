using System.Collections.Generic;
using dictionary_learner.error;

namespace dictionary_learner.token{
    public class Lexer{

        private string[] tokensList;
        private int position;
        private int length;

        public Lexer(string text){
            tokensList=text.Split(' ');
            position=0;
            length=tokensList.Length;
        }

        private string Current=>Peek(0);
        private string LookAhead=>Peek(1);

        public string Peek(int offset){
            if(position+offset<length)
                return tokensList[position+offset];
            return "";
        }

        public void Next(){
            position++;
        }


        public  FunctionalToken Lex(){
            
            TokenKind functionalTokenKind=TokenKind.Invalid;
            Dictionary<TokenKind,int> options=new Dictionary<TokenKind, int>();
            List<string> words=new List<string>();
            

            bool isFirstToken=true;
            while (position<length)
            {
                string token=Current;

                if(token.Length<=0){
                    Next();
                    continue;
                }
                if(isFirstToken){
                    functionalTokenKind=GetFunctionalTokenKind(token);
                    isFirstToken=false;
                    Next();
                    continue;
                }

                char firstChar=token[0];

                if(char.IsLetter(firstChar)){
                    words.Add(token);
                }else{
                    
                    switch (token)
                    {
                        case "-l": {
                            string newToken=LookAhead;
                            var status=int.TryParse(newToken,out int limit);
                            if(status){
                                options.Add(TokenKind.Limit,limit);
                                Next();
                            }else{
                                ErrorBag.ReportError($"Invalid optional token {token} as there is no limit value.");
                            }
                        };break;
                        case "-tv": options.Add(TokenKind.VowelTollrance,1);break;
                        default: ErrorBag.ReportError($"Invalid optional token {token}");break;
                    }

                }
                Next();
            }


            FunctionalToken keywordToken=new FunctionalToken(functionalTokenKind,options,words);
            return keywordToken;
        }


        public TokenKind GetFunctionalTokenKind(string token){
            switch (token.ToLower())
            {
                case "help":    return TokenKind.Help;
                case "read":    return TokenKind.Read;
                case "reset":   return TokenKind.Reset;
                case "reload":  return TokenKind.Reload;
                case "save":    return TokenKind.Save;
                case "insert":  return TokenKind.Insert;
                case "search":  return TokenKind.Search;
                case "suggest": return TokenKind.Suggest;
                case "clear"  : return TokenKind.Clear;
                default:        return TokenKind.Invalid; 
            }
        }

    }
}