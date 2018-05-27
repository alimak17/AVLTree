using System;
using System.Collections.Generic;
using AVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AVLTests
    {
        [TestMethod]
        public void Add_Random()
        {
            AVL<int> tree = new AVL<int>();
            Random random = new Random();
            for (int i = 0; i < 1000; ++i)
            {
                tree.Add(random.Next(1000000));
                Assert.IsTrue(tree.CheckAVL());
                List<int> list = tree.InOrder();
                List<int> control = new List<int>(list);
                control.Sort();
                CollectionAssert.AreEqual(control, list);
            }
        }
    }
}
