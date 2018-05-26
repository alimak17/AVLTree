using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AVLTree
{
    public class BST<T> where T : IComparable<T>
    {
        BST<T> parent;
        BST<T> left;
        BST<T> right;
        public int size { get; private set; }
        public T value { get; set; }

        public BST()
        {
            size = 0;
            left = null;
            right = null;
            parent = null;
        }

        public BST(BST<T> parent) : this()
        {
            this.parent = parent;
        }

        // returns false, if insert value is already in the tree
        public bool Add(T insert)
        {
            bool ret;
            if (isLeaf())
            {
                value = insert;
                left = new BST<T>(this);
                right = new BST<T>(this);
                ret = true;
            }
            else if (insert.CompareTo(value) == 0)
                ret = false;
            else if (insert.CompareTo(value) < 0)
                ret = left.Add(insert);
            else
                ret = right.Add(insert);
            if (ret)
                size++;
            return ret;
        }

        public bool Delete(T value)
        {
            BST<T> toDelete = Find(value);
            if (toDelete == null)
                return false;

            void decreaseSize(BST<T> curr)
            {
                while (curr != null)
                {
                    curr.size--;
                    curr = curr.parent;
                }
            }

            if (toDelete.left.isLeaf() && toDelete.right.isLeaf())
            {
                toDelete.left = null;
                toDelete.right = null;
                toDelete.size = 0;
                decreaseSize(toDelete.parent);
            }
            else if (toDelete.left.size >= toDelete.right.size)
            {
                BST<T> max = toDelete.left.Max();
                toDelete.value = max.value;
                if (max.parent.right == max)
                    max.parent.right = max.left;
                else
                    max.parent.left = max.left;
                max.left.parent = max.parent;
                decreaseSize(max.parent);
            }
            else
            {
                BST<T> min = toDelete.right.Min();
                toDelete.value = min.value;
                if (min.parent.left == min)
                    min.parent.left = min.right;
                else
                    min.parent.right = min.right;
                min.right.parent = min.parent;
                decreaseSize(min.parent);
            }
            return true;
        }

        public BST<T> Find(T lookUp)
        {
            if (isLeaf())
                return null;
            int compare = lookUp.CompareTo(value);
            if (compare == 0)
                return this;
            else if (compare < 0)
                return left.Find(lookUp);
            else
                return right.Find(lookUp);
        }

        public bool Contains(T lookUp) => Find(lookUp) != null;

        void PrintTree(int depth)
        {
            void printSpaces() { for (int i = 0; i < 2 * depth; ++i) Write(" "); }

            if (isLeaf())
            {
                printSpaces();
                WriteLine($"* ({size})");
            }
            else
            {
                right.PrintTree(depth + 1);
                printSpaces();
                WriteLine($"{value} ({size})");
                left.PrintTree(depth + 1);
            }
        }

        public void Print() => PrintTree(0);

        bool isLeaf() => left == null && right == null;

        public BST<T> Min()
        {
            if (isLeaf())
                return null;

            if (left.isLeaf())
                return this;
            else
                return left.Min();
        }

        public BST<T> Max()
        {
            if (isLeaf())
                return null;

            if (right.isLeaf())
                return this;
            else
                return right.Max();
        }

        void constructPreOrder(ref List<T> list)
        {
            if (!isLeaf())
            {
                list.Add(value);
                left.constructPreOrder(ref list);
                right.constructPreOrder(ref list);
            }
        }

        public List<T> PreOrder()
        {
            List<T> temp = new List<T>(size);
            constructPreOrder(ref temp);
            return temp;
        }

        void constructInOrder(ref List<T> list)
        {
            if (!isLeaf())
            {
                left.constructInOrder(ref list);
                list.Add(value);
                right.constructInOrder(ref list);
            }
        }

        public List<T> InOrder()
        {
            List<T> temp = new List<T>(size);
            constructInOrder(ref temp);
            return temp;
        }

        void constructPostOrder(ref List<T> list)
        {
            if (!isLeaf())
            {
                left.constructPostOrder(ref list);
                right.constructPostOrder(ref list);
                list.Add(value);
            }
        }

        public List<T> PostOrder()
        {
            List<T> temp = new List<T>(size);
            constructPostOrder(ref temp);
            return temp;
        }

        public int Depth()
        {
            if (isLeaf())
                return 0;
            else
                return Math.Max(left.Depth(), right.Depth()) + 1;
        }

        int TotalDepth()
        {
            if (isLeaf())
                return 0;
            else
                return left.TotalDepth() + right.TotalDepth() + size;
        }

        public double AverageDepth() => isLeaf() ? 0 : TotalDepth() / (double)size;
    }

    static class IntBinarySearchTree
    {
        public static int Sum(this BST<int> tree)
            => tree.InOrder().Sum();

        public static BST<int> CopyTreeAndAddOne(this BST<int> tree)
        {
            BST<int> temp = new BST<int>();
            foreach (int number in tree.PreOrder().Select(n => n + 1).ToList())
                temp.Add(number);
            return temp;
        }

        public static int Um(this BST<int> tree, int a)
         => tree.Sum() * a;
    }

    class Program
    {
        static BST<int> CreateTree()
        {
            int[] insert = new int[] { 10, 5, 3, 8, 6, 14, 12, 17 };
            BST<int> tree = new BST<int>();
            Array.ForEach(insert, i => tree.Add(i));
            return tree;
        }

        static void Main(string[] args)
        {
            BST<int> tree = CreateTree();
            WriteLine(tree.Um(2));
            //BST<int> binary = tree.CopyTreeAndAddOne();
            //binary.Print();
            //BST<int> bst = new BST<int>();
            //bst.Print();
            //bst.Add(5);
            //WriteLine();
            //bst.Print();
            //bst.Add(3);
            //WriteLine();
            //bst.Print();
            //bst.Add(7);
            //WriteLine();
            //bst.Print();
            //bst.Add(13);
            //WriteLine();
            //bst.Print();
            //bst.Add(1);
            //WriteLine();
            //bst.Print();
            //bst.Add(0);
            //WriteLine();
            //bst.Print();
            //bst.Add(-5);
            //WriteLine();
            //bst.Print();
            //WriteLine(bst.Contains(3));
            //WriteLine(bst.Contains(100));
            //WriteLine();
        }
    }
}
