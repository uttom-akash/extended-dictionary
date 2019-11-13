using System;
using System.Collections.Generic;
using System.IO;

namespace dictionary_learner.data_structure
{
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


        public void insert(string word,string partOfSpeech="",string meaning=""){
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

        public bool search(string word){
            Node iterator=root;
            foreach (char character in word)
            {
                int child=(int)character-'a';
                if(child>26 || child<0)
                    child=28;
                if(iterator.childs[child]==null)
                    return false;
                iterator=iterator.childs[child];
            }
            return iterator.isEnd;
        }

        public int numberOfWordsWithPrefix(string prefix){
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

        public List<Info> findWordsWithPrefix(string prefix){
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
            
            return traverse(iterator);
        }


        public List<Info> traverse(Node source){
            List<Info> words=new List<Info>();
            Stack<Node> dfs=new Stack<Node>();
            dfs.Push(source);

            while(dfs.Count>0){
                Node currentNode=dfs.Pop();

                
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



        // store tree
        public void store(){
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


        public void load(){
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