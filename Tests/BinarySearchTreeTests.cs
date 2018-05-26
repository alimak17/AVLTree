using System;
using System.Collections.Generic;
using AVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        BST<int> CreateTree()
        {
            int[] insert = new int[] { 10, 5, 3, 8, 6, 14, 12, 17 };
            BST<int> tree = new BST<int>();
            Array.ForEach(insert, i => tree.Add(i));
            return tree;
        }

        BST<int> CreateEmptyTree() => new BST<int>();

        BST<int> CreateRandomTree()
        {
            BST<int> tree = new BST<int>();
            Random random = new Random();
            for (int i = 0; i < 1000; ++i)
                tree.Add(random.Next(1000000));
            return tree;
        }

        [TestMethod]
        public void Contains()
        {
            BST<int> tree = CreateTree();
            int[] isInTree = new int[] { 3, 10, 17 };
            int[] notInTree = new int[] { 1, 20 };

            Array.ForEach(isInTree, a => Assert.IsTrue(tree.Contains(a)));
            Array.ForEach(notInTree, a => Assert.IsFalse(tree.Contains(a)));
        }

        [TestMethod]
        public void MinAndMax()
        {
            BST<int> tree = CreateTree();
            BST<int> empty = CreateEmptyTree();
            Assert.AreEqual(3, tree.Min().value);
            Assert.AreEqual(null, empty.Min());
            Assert.AreEqual(17, tree.Max().value);
            Assert.AreEqual(null, empty.Max());
        }

        [TestMethod]
        public void PreOrder()
        {
            BST<int> tree = CreateTree();
            List<int> control = new List<int>() { 10, 5, 3, 8, 6, 14, 12, 17 };
            CollectionAssert.AreEqual(control, tree.PreOrder());
        }

        [TestMethod]
        public void InOrder()
        {
            BST<int> tree = CreateTree();
            List<int> control = new List<int>() { 3, 5, 6, 8, 10, 12, 14, 17 };
            CollectionAssert.AreEqual(control, tree.InOrder());
        }

        [TestMethod]
        public void InOrder_Random()
        {
            BST<int> tree = CreateRandomTree();
            List<int> list = tree.InOrder();
            List<int> control = new List<int>(list);
            control.Sort();
            CollectionAssert.AreEqual(control, list);
        }

        [TestMethod]
        public void PostOrder()
        {
            BST<int> tree = CreateTree();
            List<int> control = new List<int>() { 3, 6, 8, 5, 12, 17, 14, 10 };
            CollectionAssert.AreEqual(control, tree.PostOrder());
        }

        [TestMethod]
        public void Depth()
        {
            BST<int> tree = CreateTree();
            BST<int> emptyTree = CreateEmptyTree();
            Assert.AreEqual(4, tree.Depth());
            Assert.AreEqual(0, emptyTree.Depth());
        }

        [TestMethod]
        public void AverageDepth()
        {
            BST<int> tree = CreateTree();
            BST<int> emptyTree = CreateEmptyTree();
            Assert.AreEqual(2.625, tree.AverageDepth(), 0.001);
            Assert.AreEqual(0, emptyTree.AverageDepth(), 0.001);
        }

        [TestMethod]
        public void Size()
        {
            BST<int> tree = CreateTree();
            BST<int> emptyTree = CreateEmptyTree();
            Assert.AreEqual(8, tree.size);
            Assert.AreEqual(0, emptyTree.size);
        }

        [TestMethod]
        public void Delete()
        {
            BST<int> tree = CreateTree();
            List<int> inorder = tree.InOrder();
            int[] toDelete = new int[] { 10, 5, 3, 8, 6, 14, 12, 17 };
            foreach (int del in toDelete)
            {
                tree.Delete(del);
                inorder.Remove(del);
                CollectionAssert.AreEqual(inorder, tree.InOrder());
                Assert.AreEqual(inorder.Count, tree.size);
            }
        }

        [TestMethod]
        public void Delete_Random()
        {
            BST<int> tree = CreateRandomTree();
            List<int> inorder = tree.InOrder();
            List<int> toDelete = tree.PreOrder();
            foreach (int del in toDelete)
            {
                tree.Delete(del);
                inorder.Remove(del);
                CollectionAssert.AreEqual(inorder, tree.InOrder());
                Assert.AreEqual(inorder.Count, tree.size);
            }
        }
    }
}