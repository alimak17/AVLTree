using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AVLTree
{
    public class AVL<T> : BST<T> where T : IComparable<T>
    {
        int balance;

        public AVL() : base()
            => balance = 0;

        public AVL(AVL<T> parent) : base(parent)
            => balance = 0;

        AVL<T> AVLright() => (AVL<T>)right;
        AVL<T> AVLleft() => (AVL<T>)left;
        AVL<T> AVLparent() => (AVL<T>)parent;

        void rebalanceAfterAdd()
        {
            AVL<T> z = this;
            while (z.parent != null)
            {
                AVL<T> x = z.AVLparent();
                if (x.AVLright() == z)
                {
                    if (x.balance == 1)
                    {
                        if (z.balance == -1)
                            x.rotateRightLeft();
                        else
                            x.rotateLeft();
                        return;
                    }
                    else if (x.balance == -1)
                    {
                        x.balance = 0;
                        return;
                    }
                    else
                        x.balance = +1;
                }
                else
                {
                    if (x.balance == -1)
                    {
                        if (z.balance == +1)
                            x.rotateLeftRight();
                        else
                            x.rotateRight();
                        return;
                    }
                    else if (x.balance == +1)
                    {
                        x.balance = 0;
                        return;
                    }
                    else
                        x.balance = -1;
                }
                z = x;
            }
        }

        public new bool Add(T insert)
        {
            bool ret;
            if (isLeaf())
            {
                value = insert;
                left = new AVL<T>(this);
                right = new AVL<T>(this);
                balance = 0;
                rebalanceAfterAdd();
                ret = true;
            }
            else if (insert.CompareTo(value) == 0)
                ret = false;
            else if (insert.CompareTo(value) < 0)
                ret = AVLleft().Add(insert);
            else
                ret = AVLright().Add(insert);
            if (ret)
                size++;
            return ret;
        }

        void rotateLeft()
        {
            AVL<T> bottom = AVLright();
            AVL<T> A = AVLleft();
            AVL<T> B = bottom.AVLleft();
            AVL<T> C = bottom.AVLright();
            T tmp = value;
            value = bottom.value;
            bottom.value = tmp;
            left = bottom;
            right = C;
            C.parent = this;
            bottom.left = A;
            A.parent = bottom;
            bottom.right = B;

            if (bottom.balance == 0)
            {
                balance = -1;
                bottom.balance = +1;
            }
            else
            {
                balance = 0;
                bottom.balance = 0;
            }
            bottom.size = A.size + B.size + 1;
        }

        void rotateRight()
        {
            AVL<T> bottom = AVLleft();
            AVL<T> A = AVLright();
            AVL<T> B = bottom.AVLright();
            AVL<T> C = bottom.AVLleft();
            T tmp = value;
            value = bottom.value;
            bottom.value = tmp;
            right = bottom;
            left = C;
            C.parent = this;
            bottom.right = A;
            A.parent = bottom;
            bottom.left = B;

            if (bottom.balance == 0)
            {
                balance = +1;
                bottom.balance = -1;
            }
            else
            {
                balance = 0;
                bottom.balance = 0;
            }
            bottom.size = A.size + B.size + 1;
        }

        void rotateLeftRight()
        {
            int bottomBalance = AVLleft().AVLright().balance;
            AVLleft().rotateLeft();
            rotateRight();
            if (bottomBalance == -1)
                AVLright().balance = 1;
            else
                AVLright().balance = 0;
            if (bottomBalance == 1)
                AVLleft().balance = -1;
            else
                AVLleft().balance = 0;
            balance = 0;
        }

        void rotateRightLeft()
        {
            int bottomBalance = AVLright().AVLleft().balance;
            AVLright().rotateRight();
            rotateLeft();
            if (bottomBalance == 1)
                AVLleft().balance = -1;
            else
                AVLleft().balance = 0;
            if (bottomBalance == -1)
                AVLright().balance = +1;
            else
                AVLright().balance = 0;
            balance = 0;
        }

        protected override void PrintTree(int depth)
        {
            void printSpaces() { for (int i = 0; i < 2 * depth; ++i) Write(" "); }

            if (isLeaf())
            {
                printSpaces();
                WriteLine($"* ({balance})");
            }
            else
            {
                AVLright().PrintTree(depth + 1);
                printSpaces();
                WriteLine($"{value} ({balance})");
                AVLleft().PrintTree(depth + 1);
            }
        }

        public bool CheckAVL()
        {
            if (isLeaf()) return true;

            int controlBalance = right.Depth() - left.Depth();
            if (controlBalance != balance)
            {
                WriteLine($"Value {value}: Control balance is not actual balance");
                WriteLine($"Control balance: {controlBalance}, actual balance: {balance}");
                PrintRoot();
                return false;
            }
            if (balance < -1 || balance > +1)
            {
                WriteLine($"Value {value}: Balance is out of bound");
                WriteLine($"Balance: {balance}");
                PrintRoot();
                return false;
            }
            return AVLleft().CheckAVL() && AVLright().CheckAVL();
        }
    }
}