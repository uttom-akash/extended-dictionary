namespace dictionary_learner.data_structure
{
    class Node 
    {
        public string word;
        public string meaning;
        public string partOfSpeech;
        public bool isEnd;
        public int numberOfChilds;
        public Node[] childs;
        public Node(int alphabetSize)
        {
            word=null;
            numberOfChilds=0;
            isEnd=false;
            childs=new Node[alphabetSize];
            for(int c=0;c<alphabetSize;c++)
                childs[c]=null;
        }
    }
}