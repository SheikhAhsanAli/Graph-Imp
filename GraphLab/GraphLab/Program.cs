using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("        -_-_-_-_-_WELCOME-_-_-_-_-_-\n");
            Graph gr = new Graph(5);
            //gr.Addedge(0, 1);
            //gr.Addedge(0, 2);
            //gr.Addedge(0, 3);
            //gr.Addedge(1, 5);
            //gr.Addedge(1, 4);
            //gr.Addedge(2, 6);

             gr.AddDirEdge(0, 1, 10);
             gr.AddDirEdge(0, 1, 4);
             gr.AddDirEdge(1, 2, 1);
             gr.AddDirEdge(1, 4, 2);
            gr.Dijikstra(0);

            //gr.Printdir();
            //PQueue obj = new PQueue();
            //obj.Enqueue(10);
            //obj.Enqueue(110);
            //obj.Enqueue(101);
            //obj.Enqueue(1011);
            //obj.Enqueue(111);
            //obj.Dequeue();
            //int ele = obj.Extractmin();
            //Console.WriteLine("Minimum is {0}",ele); // minimum element returned by extract min

            //PriorityQueue q = new PriorityQueue();
            //q.Enqueue(new Vertex(0, 10));
            //q.Enqueue(new Vertex(1, 20));
            //q.Enqueue(new Vertex(2, 2));
            //q.Enqueue(new Vertex(3, 120));
            //Vertex a = q.Extractmin();
            //Console.WriteLine("ID  {0}   Weight  {1}", a.id, a.weight);



            //  int[] arr = gr.GetdirNei(1);

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine(arr[i]);
            //}

            //Console.WriteLine("\nNow DFS\n");
          //  gr.DFS(0);
            //Console.WriteLine("\n\n             BFS              \n\n");
           // gr.BFS(0);
        }
    }
    class Graph
    {
        public int[,] adjmatrix;
        public int[,] adjdirmatrix;
        public int numofnodes;
        public Graph()
        {
        }
        public Graph(int numofnodes)
        {
            this.numofnodes = numofnodes;
            adjmatrix = new int[numofnodes, numofnodes];
            adjdirmatrix = new int[numofnodes, numofnodes];
        }
        public void Addedge(int source, int destination)
        {
            adjmatrix[source, destination] = 1;
            adjmatrix[destination, source] = 1;
        }
        public void AddDirEdge(int source, int destination,int weight = 0)//(weight = 0 is the default weight)
        {
            adjdirmatrix[source,destination]= weight;
        }
        public int[] GetNei(int vertex)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < numofnodes; i++)
            {
                if (adjmatrix[vertex, i] > 0)                 //iterate each column for row=vertex
                    list.Add(i);//if value of col is 1; then add index of this col;into list of neighbours
            }                         //return final list of neighbours



            return list.ToArray();

        }

        public int[] GetdirNei(int vertex)
        {
            List<int> listdir = new List<int>();
            for (int i = 0; i < numofnodes; i++)
            {
                if (adjdirmatrix[vertex, i] > 0)                 //iterate each column for row=vertex
                    listdir.Add(i);//if value of col is 1; then add index of this col;into list of neighbours
            }                         //return final list of neighbours



            return listdir.ToArray();

        }
        public void Print()
        {
            for (int i = 0; i < numofnodes; i++)
            {
                for (int j = 0; j < numofnodes; j++)
                {
                    Console.Write("{0}\t", adjmatrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Printdir()
        {
            for (int i = 0; i < numofnodes; i++)
            {
                for (int j = 0; j < numofnodes; j++)
                {
                    Console.Write("{0}\t", adjdirmatrix[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void DFS(int source)
        {
            Stack<int> st = new Stack<int>();//create a stack S 
            List<int> l = new List<int>();
            st.Push(source);
            l.Add(source);
            while (st.Count() != 0)
            {
                int var = st.Pop();
                Console.WriteLine(var);
                int[] arr = GetNei(var);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (l.Contains(arr[i]) == false)
                    {
                        l.Add(arr[i]);
                        st.Push(arr[i]);
                    }
                }

            }





            //Push source in S
            // Pop from stack
            // get neighbours
            // if neighbour is not visited,
            //(if node is not in list of visited)
            //print node for testing.
            //mark visited.(add in list of visited nodes)
            //push in S
        }
        public void BFS(int source)
        {
            Queue<int> st = new Queue<int>();//create a stack S 
            List<int> l = new List<int>();
            st.Enqueue(source);
            l.Add(source);
            while (st.Count() != 0)
            {
                int var = st.Dequeue();
                Console.WriteLine(var);
                int[] arr = GetNei(var);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (l.Contains(arr[i]) == false)
                    {
                        l.Add(arr[i]);
                        st.Enqueue(arr[i]);
                    }
                }

            }

        }

        public void Dijikstra(int source)
        {
            PriorityQueue p = new PriorityQueue();

            ///LINE 1
            // initialize
            //     create an array of vertex type for distance of vertex
            Vertex[] disver = new Vertex[numofnodes];
            //   for each vertex, create an object : Verrtex, set weight to infinity
            for (int i = 0; i < numofnodes; i++)
            {
                disver[i] = new Vertex(i, int.MaxValue);
                
            }
            // set source =0
            disver[source].weight = 0;

            ///LINE 3
            //for each vertex 
            //    store each vertex in priority queue
            //for (int j = 0; j < numofnodes; j++)
            //    p.Enqueue(disver[j]);
            foreach (var d in disver)
                p.Enqueue(d);

            // LINE 4
            // while priority queue is not empty
            while (!p.IsEmpty())
            {
                //  Extract minimum vertex
                Vertex X = p.Extractmin();
                // getneighbours 
                int[] Neg = GetdirNei(X.id);  // array to store neighbours
                

                foreach (var neg in Neg)
                {
                    int sw = X.weight; // source weight
                    int dw = disver[neg].weight; // destination weight
                    int ew = adjdirmatrix[X.id,neg]; // edge weight
                    // relax each neighbour
                    if (dw > sw + ew)
                    {
                        disver[neg].weight = sw + ew;
                    }
                            // = sw + dw;


                }
                
            }
            foreach (var n in disver)
            {
                Console.WriteLine("ID: {0} WEIGHT: {1}",n.id,n.weight);
            }
             


        }
    }
    public class PQueue
    {
        

               List<int> elements;
        public PQueue()
        {
            elements = new List<int>();
        }

        public int Extractmin()
        {
            // find loc of min element
            int loc = FindMIn();
            // store the element at x-th position;
            int y = elements[loc];
            // remove the element from x-th location
            elements.RemoveAt(loc);
            // return y
              return y;
        }
        private int FindMIn()
        {
            

             int min = elements[0];
             int loc = 0;
             for (int i = 0; i < elements.Count; i++)
             {
                 if (elements[i]<min)
                 {
                     min = elements[i];
                     loc = i;
                     
                 }
             }
              return loc;


            //initially first element is min
            //iterate each element
            //   if element is less than minimum
            //      update min, and update loc
        }

        
        public void Enqueue(int e)
        {
            //add at last
            //list.Add()
            elements.Add(e);


        }
        public int Dequeue()
        {
            //remove at 0th location
            int x = elements[0];

            elements.RemoveAt(0);
            return x;

        }
        
    }

    public class PriorityQueue
    {


        List<Vertex> element;
        public PriorityQueue()
        {
            element = new List<Vertex>();
        }

        public Vertex Extractmin()
        {
            // find loc of min element
            int loc = FindMIn();
            // store the element at x-th position;
            Vertex y = element[loc];
            // remove the element from x-th location
            element.RemoveAt(loc);
            // return y
            return y;
        }
        private int FindMIn()
        {


            int min = element[0].weight;
            int loc = 0;
            for (int i = 0; i < element.Count; i++)
            {
                if (element[i].weight < min)
                {
                    min = element[i].weight;
                    loc = i;

                }
            }
            return loc;


            //initially first element is min
            //iterate each element
            //   if element is less than minimum
            //      update min, and update loc
        }
        public bool IsEmpty()
        {
            return element.Count() == 0 ? true: false;

        }
        public void Enqueue(Vertex e)
        {
            //add at last
            //list.Add()
            element.Add(e);


        }
        public Vertex Dequeue()
        {
            //remove at 0th location
            Vertex x = element[0];

            element.RemoveAt(0);
            return x;

        }

    }


    public class Vertex
    {
        public int id;
        public int weight;

        public Vertex(int id, int w) // agr vertex ki class bana li to jahan jahan int use kr rhy usy pqueue m vertex krden gy pq ki class me
        {
            this.id = id;
            this.weight = w;
        }













    }
}
