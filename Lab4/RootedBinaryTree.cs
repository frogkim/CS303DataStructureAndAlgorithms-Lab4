﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//In this project, you will write a data compression program implementing Huffman's Algorithm
//with the message unit equal to one character of text. In a namespace called Lab4, write C#
//classes called RootedBinaryTree and Compressor using the supplied code stub  Download
//supplied code stub. Do not modify the names, return types, visibility, or parameter lists of
//the methods supplied in the code stub, but you may add private methods as appropriate.
//Do not add data members to these classes.

//The huffman method should accept a list of Word objects with the plainWord and probability
//fields filled in, and return the same array with the codeWord fields also filled in according
//to Huffman's Algorithm. In implementing Huffman's Algorithm, make use of the RootedBinaryTree
//class, with the generic type parameter equal to Word. When Huffman's Algorithm combines two
//trees, make sure that the plainWord in the left-most leaf is copied into the root of the
//combined tree. In case of a tie in the probabilities of two Words, use the ASCII values of
//the plainWords to determine which Word is smaller. Note that RootedBinaryTree should perform
//shallow copying to reduce the time- and space-complexity of tree operations.

//The compress method of the Compressor class should read the given input file, assign
//probabilities to characters according to their frequency of occurrence in the file,
//use the huffman method to translate the characters into codewords, and finally
//write the output file in binary format as the sequence of codewords of the input file.
//Note that you must, in general, break up the binary output into bytes in order to store it
//in the output file, which will entail breaking up codewords across bytes.
//Do not expand each bit of the codewords into a whole byte of output!
//Do not attempt to store the entire input or output file in program memory!

//OPTIONAL: At the beginning of the output file in the compress method,
//write a dictionary of character/codeword pairs in a fixed-length format,
//as follows: p + n + b1 + L1 + c1 + b2 + L2 + c2 + ...+ bn + Ln + cn,
//where: p is a 3 - bit number indicating how many bits of the final byte
//in the output file are padding (note that p can be between 0 and 7);
//n is a 1 - byte number indicating how many character/codeword pairs will follow;
//b1, b2, ..., bn are the 1-byte ASCII character values which appear as messages
//in the original file, in increasing order; L1, L2, ..., Ln are 1-byte values
//of the lengths (in bits) of the codewords; and c1, c2, ..., cn are the codewords
//associated with b1, b2, ..., bn, respectively. Note that p should only use
//the first 3 bits of the first byte of output (not the entire first byte);
//similarly, do not pad individual codewords to make full bytes.
//Then complete the decompress method of the Compressor class.

namespace Lab4
{
    public class RootedBinaryTree<T> : IComparable<RootedBinaryTree<T>>
        where T : IComparable<T>
    {
        private class Node
        {
            public T nodeData;
            public Node leftChild;
            public Node rightChild;
            public Node parent;
        };
        private Node root;
        private Node currentPosition;
        public int CompareTo(RootedBinaryTree<T> otherTree)
        {
            return root.nodeData.CompareTo(otherTree.root.nodeData);
        }
        public RootedBinaryTree(T rootData)
        {
            //YOUR CODE HERE
        }
        public void toRoot()
        {
            //YOUR CODE HERE 
        }
        public bool moveLeft()
        {
            //YOUR CODE HERE
            return false;
        }
        public bool moveRight()
        {
            //YOUR CODE HERE
            return false;
        }
        public bool moveUp()
        {
            //YOUR CODE HERE
            return false;
        }
        public T getData()
        {
            return default(T);//Replace this line!
                              //YOUR CODE HERE
        }
        public void combineTrees(RootedBinaryTree<T> leftTree, RootedBinaryTree<T> rightTree)
        {
            //YOUR CODE HERE: combine the two arguments under the root of the invoking tree object.
        }
        public void setNodeData(T nodeData)
        {
            //YOUR CODE HERE
        }
    }
    public class Compressor
    {
        static byte bitsPerByte = 8;
        public class Word : IComparable<Word>
        {
            public string plainWord;
            public double probability;
            public string codeWord;
            public int CompareTo(Word otherWord)
            {
                int ret = probability.CompareTo(otherWord.probability);
                if (ret == 0) ret = ((int)plainWord[0] < (int)otherWord.plainWord[0]) ? -1 : 1;
                //plainWord.CompareTo(otherWord.plainWord);//
                return ret;
            }
        }
        public static void compress(string inputFileName, string outputFileName)
        {
            //YOUR CODE HERE
        }
        public static void huffman(List<Word> theWords)
            //input conditions: theWords contains plainWord and probability fields; output conditions: codeWord fields are filled in
        {
            //YOUR CODE HERE
        }
        public static void decompress(string inputFileName, string outputFileName)
        {
            //OPTIONAL: YOUR CODE HERE
        }
    }
}