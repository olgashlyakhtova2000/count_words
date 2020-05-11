using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace поиск_слов
{
    public class Node: IComparable
    {
        public string Key;
        public int Count;
        public int key;
        public Node left;
        public Node right;
        public int height;
        public Node( string srt, int x)
        {
            Key = srt;
            Count = x;
        }

        public int CompareTo(object obj)
        {
            return Key.CompareTo(obj);
        }
        public static bool  operator >(Node c1, Node c2)
        {
            return c1.Key.CompareTo(c2.Key)==1?true:false;
        }
        public static bool operator <(Node c1, Node c2)
        {
            return c1.Key.CompareTo(c2.Key) == -1 ? true : false;
        }

        //public int Compare()
        //{
        //    return Key.CompareTo(obj);
        //}
    }

    public class AVL: IEnumerable
    {
        public Node root;

        public int Compare (Node p, Node p1)
        {
            if (p.Key == p1.Key) return 0;
            //else if (p.Key < p1.Key) return -1;
            else return 1;
        }
        public int height(Node p)
        {
            return p == null ? 0 : p.height;
        }
        public void Insert(string x)
        {

            root = insert(root, x);
        }
        private Node insert(Node p, string x)
        {
            if (p == null)
                return new Node(x, 1);
            if (x.CompareTo(p.Key) < 0)
                p.left = insert(p.left, x);
            else if (x.CompareTo(p.Key) > 0)
                p.right = insert(p.right, x);
            else p.Count++;
            p.height = 1 + Math.Max(height(p.left), height(p.right));
            int balance = p == null ? 0 : height(p.left) - height(p.right);

            if (balance > 1 && x.CompareTo( p.left.Key)>0)//left left 
                return rightRotate(p);
            if (balance < -1 && x.CompareTo(p.right.Key)<0)//right right
                return leftRotate(p);
            if (balance > 1 && x.CompareTo(p.left.Key) > 0)//lr
            {
                p.left = leftRotate(p.left);
                return rightRotate(p);
            }
            if (balance < -1 && x.CompareTo(p.right.Key) < 0)//rl
            {
                p.right = rightRotate(p.right);
                return leftRotate(p);
            }
            // big K
            return p;
        }
        public bool Contains(int x)
        {

            return false;
        }
        private Node rightRotate(Node y)
        {
            var x = y.left;
            var T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = 1 + Math.Max(height(y.left), height(y.right));
            x.height = 1 + Math.Max(height(x.left), height(x.right));

            return x;
        }
        private Node leftRotate(Node x)
        {
            var y = x.right;
            var T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = 1 + Math.Max(height(x.left), height(x.right));
            y.height = 1 + Math.Max(height(y.left), height(y.right));


            return y;
        }
        public Node rr(Node rt)
        {
            var piv = rt.left;
            rt.left = piv.right;
            piv.right = rt;
            return piv;
        }

        public Node rl(Node rt)
        {
            var piv = rt.right;
            rt.right = piv.left;
            piv.left = rt;
            return piv;
        }
        public void Print()
        {
            if (root != null)
            {
                print(root, 0);
                Console.WriteLine();
            }
        }
        private void print(Node p, int shift)
        {
            if (p.right != null)
                print(p.right, shift + 1);

            for (int i = 0; i != shift; i++)
                Console.Write("  ");
            Console.WriteLine("{0} {1}", p.Count, p.Key);

            if (p.left != null)
                print(p.left, shift + 1);

        }


        public void Print2()
        {
            Console.WriteLine(tostr(root));
        }
        private string tostr(Node p)
        {
            if (p == null)
                return "\n";
            return "";
        }

        public IEnumerator GetEnumerator()
        {
            Node curr = root;
            for (int i = 1; curr != null; i++)
            {
                string s = Convert.ToString(i);
                s += ". ";
                s += Convert.ToString(curr.key);
                yield return s;
                
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            AVL a = new AVL();
            string input_text = System.IO.File.ReadAllText(@"big.txt");
            string[] input_check = System.IO.File.ReadAllLines(@"check.txt");
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs;
            string str = "";
            foreach (var i in input_text)
            {
                if (i >= 'a'&&i<='z'||i>='A'&&i<='Z')
                {
                    str += i;
                }
                else if ((i==' '||i=='\'')&&str.Length!=0)
                {
                    a.Insert(str);
                    str = "";
                }


            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine("time:  " + elapsedMs);
            // a.Print();

        }
    }
}
