namespace dictionary_learner.data_structure{
    class Info{

        public Info(){}
        public Info(string word,string partOfSpeech,string meaning){
            this.word=word;
            this.partOfSpeech=partOfSpeech;
            this.meaning=meaning;
        }
        public string word {get;set;}
        public string partOfSpeech {get;set;}
        public string meaning {get;set;}
    }
}