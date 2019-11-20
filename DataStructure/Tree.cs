using System;
using System.Collections.Generic;
using System.IO;

namespace dictionary_learner.data_structure
{
    class Vowel{
        public static List<int> vowels=new List<int>{0,4,8,14,20};
    }
    class Tree
    {
        private Node root;
        private int alphabetSize; 
        private string path;

        public Tree(int alphabetSize)
        {
            path="";
            this.alphabetSize=alphabetSize;
            root=new Node(this.alphabetSize);
        }

        public Tree(int alphabetSize,string path)
        {
            this.path=path;
            this.alphabetSize=alphabetSize;
            root=new Node(this.alphabetSize);
        }


        public void Insert(string word,string partOfSpeech="",string meaning=""){
            if(word.Length<=0)
                return ;

            Node iterator=root;
            foreach (char character in word)
            {
                int child=(int)character-'a';
                if(child>26 || child<0)
                    child=28;
                if(iterator.childs[child]==null)
                    iterator.childs[child]=new Node(alphabetSize);
                iterator.numberOfChilds++;    
                iterator=iterator.childs[child];
            }
            iterator.numberOfChilds++;
            iterator.word=word;
            iterator.partOfSpeech=partOfSpeech;
            iterator.meaning=meaning;
            iterator.isEnd=true;
        }

        public Info ExactSearch(string word){
            Node iterator=root;
            foreach (char character in word)
            {
                int child=(int)character-'a';
                if(child>26 || child<0)
                    child=28;
                if(iterator.childs[child]==null)
                    return new Info{word=""};   
                
                iterator=iterator.childs[child];
            }
            if(iterator.isEnd)
                return new Info{word=iterator.word,partOfSpeech=iterator.partOfSpeech,meaning=iterator.meaning};
            else
                return new Info{word="",partOfSpeech="",meaning=""};
        }

        public List<Info> TolarateVowelSearch(string word){
                
                List<Info> words=new List<Info>();
                var letters=word.ToCharArray();
                int length=letters.Length;


                Queue<Tuple<Node,int>> bfs=new Queue<Tuple<Node, int>>();
                bfs.Enqueue(new Tuple<Node,int>(root,0));

                while (bfs.Count>0)
                {
                    Tuple<Node,int> currentTupple=bfs.Dequeue();
                    Node currentNode=currentTupple.Item1;
                    int currentIndex=currentTupple.Item2;

                    if(currentIndex<length){

                                char letter=letters[currentIndex];
                                int child=(int)letter-'a';
                                

                                if(child>26 || child<0)
                                    child=28;
                                
                                int bindex=Vowel.vowels.BinarySearch(child);
                                if(bindex>=0){
                                    foreach (var vowel in Vowel.vowels)
                                    {
                                        if(child!=vowel &&  currentNode.childs[vowel]!=null)
                                        {
                                            bfs.Enqueue(new Tuple<Node, int>(currentNode.childs[vowel],currentIndex+1));   
                                        }

                                    }
                                }

                                if(currentNode.childs[child]!=null){
                                    bfs.Enqueue(new Tuple<Node, int>(currentNode.childs[child],currentIndex+1));
                                }

                    }else{

                                if(currentNode.isEnd)
                                    words.Add(new Info{word=currentNode.word,partOfSpeech=currentNode.partOfSpeech,meaning=currentNode.meaning});

                    }
                }
            return words;
        }

        public List<Info> FindWordsWithPrefix(string prefix,int limit=50){
            Node iterator=root;
            foreach (var character in prefix)
            {
                int child=(int)character-'a';
                if(child>26 || child<0)
                    child=28;
                if(iterator.childs[child]==null)
                    return new List<Info>();
                iterator=iterator.childs[child];  
            }
            
            return Traverse(iterator,limit);
        }

        public List<Info> FindWordsWithPrefixTolaratingeVowel(string word,int limit =10){
                
                List<Info> words=new List<Info>();
                List<Node> leaves=new List<Node>();

                var letters=word.ToCharArray();
                int length=letters.Length;


                Queue<Tuple<Node,int>> bfs=new Queue<Tuple<Node, int>>();
                bfs.Enqueue(new Tuple<Node,int>(root,0));

                while (bfs.Count>0)
                {
                    Tuple<Node,int> currentTupple=bfs.Dequeue();
                    Node currentNode=currentTupple.Item1;
                    int currentIndex=currentTupple.Item2;

                    if(currentIndex<length){

                                char letter=letters[currentIndex];
                                int child=(int)letter-'a';
                                

                                if(child>26 || child<0)
                                    child=28;
                                if(currentNode.childs[child]!=null){
                                    bfs.Enqueue(new Tuple<Node, int>(currentNode.childs[child],currentIndex+1));
                                }else{
                                    int bindex=Vowel.vowels.BinarySearch(child);
                                    if(bindex>=0){
                                        foreach (var vowel in Vowel.vowels)
                                        {
                                            if(child!=vowel &&  currentNode.childs[vowel]!=null)
                                            {
                                                bfs.Enqueue(new Tuple<Node, int>(currentNode.childs[vowel],currentIndex+1));   
                                            }

                                        }
                                    }
                                }

                    }else{

                                if(currentNode.isEnd)
                                    words.Add(new Info{word=currentNode.word,partOfSpeech=currentNode.partOfSpeech,meaning=currentNode.meaning});
                                leaves.Add(currentNode);
                    }
                }
            if(words.Count>=limit)
                return words;
            foreach (var leaf in leaves)
            {
                words.AddRange(Traverse(leaf,limit));    
            }    
            return words;
        }

        public List<Info> Traverse(Node source,int limit){
            List<Info> words=new List<Info>();
            Stack<Node> dfs=new Stack<Node>();
            dfs.Push(source);

            while(dfs.Count>0){
                Node currentNode=dfs.Pop();

                if(words.Count>=limit)
                    return words;

                foreach (var child in currentNode.childs)
                {
                    if(child==null)continue;
                    dfs.Push(child);
                }
                
                if(currentNode.isEnd)
                {
                    words.Add(new Info{word=currentNode.word,partOfSpeech=currentNode.partOfSpeech,meaning=currentNode.meaning});
                }
            }
            return words;
        }

        public int NumberOfWordsWithPrefix(string prefix){
            Node iterator=root;
            foreach (char character in prefix)
            {
                int child=(int)character-'a';
                if(child>26 || child<0)
                    child=28;
                if(iterator.childs[child]==null)
                    return 0;
                iterator=iterator.childs[child];
            }
            return iterator.numberOfChilds;
        }



        // store tree
        public void Store(){
            Queue<Node> bfs=new Queue<Node>();
            bfs.Enqueue(root);
            StreamWriter writer=new StreamWriter(path);
            while (bfs.Count>0)
            {
                Node current=bfs.Dequeue();

                for (int childIndex = 0; childIndex < alphabetSize; childIndex++)
                {
                    var child=current.childs[childIndex];
                    if(child==null)continue;

                    writer.WriteLine($"{childIndex},{child.isEnd},{child.numberOfChilds},{child.word},{child.partOfSpeech},{child.meaning}");
                    bfs.Enqueue(child);
                }
                writer.WriteLine($"{-1},{-1},{-1},{-1},{-1},{-1}");
            }
            writer.Close();
        }


        public void Load(){
            Queue<Node> bfs=new Queue<Node>();
            bfs.Enqueue(root);
            StreamReader reader=new StreamReader(path);
            while (bfs.Count>0)
            {
                Node current=bfs.Dequeue();

                while (true)
                {
                    var line=reader.ReadLine();
                    var words=line.Split(',');

                    if(words.Length<=0)
                        break;
                    int childIndex=Convert.ToInt16(words[0]);
                    if(childIndex<0)
                        break;

                    Node childNode=new Node(alphabetSize);
                    childNode.isEnd=Convert.ToBoolean(words[1]);
                    childNode.numberOfChilds=Convert.ToInt16(words[2]);
                    childNode.word=words[3];
                    childNode.partOfSpeech=words[4];
                    childNode.meaning=words[5];
                    
                    current.childs[childIndex]=childNode;
                    bfs.Enqueue(childNode);
                }
            }
            reader.Close();
        }

    }
}