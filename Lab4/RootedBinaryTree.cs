using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//In this project, you will write a data compression program implementing Huffman's Algorithm
//with the message unit equal to one character of text. In a namespace called Lab4, write C#
//classes called RootedBinaryTree and Compressor using the supplied code stub. 
//Do not modify the names, return types, visibility, or parameter lists of
//the methods supplied in the code stub, but you may add private methods as appropriate.
//Do not add data members to these classes.

//The huffman method should accept a list of Word objects with the plainWord and probability fields filled in, 
//and return the same array with the codeWord fields also filled in according to Huffman's Algorithm.
//In implementing Huffman's Algorithm, make use of the RootedBinaryTree class, with the generic type parameter
//equal to Word. When Huffman's Algorithm combines two trees, make sure that the plainWord in the left-most leaf
//is copied into the root of the combined tree. In case of a tie in the probabilities of two Words, use the ASCII values of
//the plainWords to determine which Word is smaller. Note that RootedBinaryTree should perform shallow copying 
//to reduce the time- and space-complexity of tree operations.

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
        // Why does not it burst?
        // It is initialized as null.

        private Node root;
        private Node currentPosition;
        public int CompareTo(RootedBinaryTree<T> otherTree)
        {
            return root.nodeData.CompareTo(otherTree.root.nodeData);
        }
        public RootedBinaryTree(T rootData)
        {
            root = new Node();
            currentPosition = root;
            root.nodeData = rootData;
        }
        public void toRoot()
        {
            currentPosition = root;
        }
        public bool moveLeft()
        {
            if(currentPosition.leftChild == null)
            {
            }
            else if (currentPosition.leftChild.parent == currentPosition)
            {
                currentPosition = currentPosition.leftChild;
                return true;
            }
            return false;
        }
        public bool moveRight()
        {
            if (currentPosition.rightChild == null)
            {
            }
            else if (currentPosition.rightChild.parent == currentPosition)
            {
                currentPosition = currentPosition.rightChild;
                return true;
            }
            return false;
        }
        public bool moveUp()
        {
            //if (currentPosition.parent == null)
            if (currentPosition == root)
            {
                return false;
            }
            currentPosition = currentPosition.parent;
            return true;
        }
        public T getData()
        {
            return currentPosition.nodeData;
        }
        public void combineTrees(RootedBinaryTree<T> leftTree, RootedBinaryTree<T> rightTree)
        {
            root.leftChild = leftTree.root;
            root.rightChild = rightTree.root;
            leftTree.root.parent = root;
            rightTree.root.parent = root;
            //?? currentPosition = root;

            //YOUR CODE HERE: combine the two arguments under the root of the invoking tree object.
        }
        public void setNodeData(T nodeData)
        {
            currentPosition.nodeData = nodeData;
        }
    }
    //The compress method of the Compressor class should read the given input file, assign
    //probabilities to characters according to their frequency of occurrence in the file,
    //use the huffman method to translate the characters into codewords, and finally
    //write the output file in binary format as the sequence of codewords of the input file.
    //Note that you must, in general, break up the binary output into bytes in order to store it
    //in the output file, which will entail breaking up codewords across bytes.
    //Do not expand each bit of the codewords into a whole byte of output!
    //Do not attempt to store the entire input or output file in program memory!

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
                if (ret == 0) ret = ((int)plainWord[0] < (int)otherWord.plainWord[0]) ? -1 : 1;//plainWord.CompareTo(otherWord.plainWord);//
                return ret;
            }
        }
        //1. Parse Fplain into individual message units: Fplain = a1 + a2 + a3 + ... + an,
        //where + means concatenation. Set S = { a1, a2, a3, ... an}.
        //2. Compute the probability P(a) for each message a in S.
        //3. Run Huffman’s algorithm on the space X = (S, P) to get a code c.
        //4. Write the output file Fcode = D + c(a1) + c(a2) + c(a3) + ... + c(an),
        //where D is the code dictionary
        public static void compress(string inputFileName, string outputFileName)
        {
            FileStream inputFile = null;
            FileStream outputFile = null;
            try 
            { 
                inputFile = File.OpenRead(inputFileName);
                outputFile = File.Create(outputFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            var WordDictionary = new Dictionary<byte, Word>();
            int EOF = -1;
            int intInput = EOF;

            int total = 0;
            while (true)
            {
                intInput = inputFile.ReadByte(); // C# uses the magic value -1 for EOF
                if (intInput == EOF) break;
                total++;
                byte byteInput = (byte)intInput;
                if(!WordDictionary.ContainsKey(byteInput))
                {
                    Word newWord = new Word();
                    newWord.plainWord = "" + (char)byteInput;
                    newWord.probability = 1; //place-holder
                    WordDictionary.Add(byteInput, newWord);
                }
                else
                {
                    WordDictionary[byteInput].probability++;
                }
            }

            List<Word> theWord = new List<Word>();
            for(byte i=0; i<byte.MaxValue; i++)
            {
                if (WordDictionary.ContainsKey(i))
                {
                    WordDictionary[i].probability /= (double)total;
                    theWord.Add(WordDictionary[i]);
                }
            }

            huffman(theWord);

            // Write Dictionary
            string s = "";
            for (byte i=0; i<byte.MaxValue; i++)
            {
                if (WordDictionary.ContainsKey(i))
                {
                    outputFile.WriteByte(i);
                    s += WordDictionary[i].codeWord;
                    if (s.Length >= bitsPerByte)
                    {
                        string writeString = s.Substring(0, bitsPerByte);
                        outputFile.WriteByte(stringToByte(writeString));
                        s = s.Remove(0, bitsPerByte);
                        
                    }
                }
            }

            s = "";
            inputFile.Seek(0, SeekOrigin.Begin);
            while (true)
            {
                try
                {
                    intInput = inputFile.ReadByte(); // C# uses the maigc value -1 for EOF
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (intInput == EOF) break;
                byte byteInput = (byte)intInput;
                if (WordDictionary.ContainsKey(byteInput))
                {
                    s += WordDictionary[byteInput].codeWord;
                    if( s.Length >= bitsPerByte )
                    {
                        string writeString = s.Substring(0, bitsPerByte);
                        outputFile.WriteByte(stringToByte(writeString));
                        s = s.Remove(0, bitsPerByte);
                    }
                }
            }

            // reconsider - it is so inefficient.
            // s += WordDictionary[byteInput].codeWord;
            // s = s.Substring(bitsPerByte, s.Length);
            // to add or to assign, new string should be allocated.
            // Frequent memory allocation is inefficient. Is there alternative? --- char array

            //char[] cArray = new char[15];
            //int i = 0;
            //inputFile.Seek(0, SeekOrigin.Begin);
            //while (true)
            //{
            //    int intInput = inputFile.ReadByte(); // C# uses the maigc value -1 for EOF
            //    if (intInput == EOF) break;
            //    byte byteInput = (byte)intInput;
            //    if (!WordDictionary.ContainsKey(byteInput))
            //    {
            //        WordDictionary[byteInput].codeWord.ToCharArray().CopyTo(i, cArray);
            //        if(i >= bitsPerByte)
            //        {
            //            // make a byte variable
            //            // convert cArray[from 0 to 7] to byte
            //            // remove cArray[from 0 to 7]
            //            // write converted byte to file

            //            outputFile.WriteByte((byte)char.Parse(s.Substring(0, bitsPerByte)));
            //            s = s.Substring(bitsPerByte, s.Length);
            //        }
            //    }
            //}

            inputFile.Close();
            outputFile.Close();
        }

        private static byte stringToByte(string s)
        {
            int n = 1;
            int ret = 0;
            for(int i = s.Length-1; i >= 0; i--)
            {
                if(s[i] == '1')
                {
                    ret += n * 1;
                }
                n *= 2;
            }
            return (byte)ret;
        }
        //The huffman method should accept a list of Word objects with the plainWord and probability fields filled in, 
        //and return the same array with the codeWord fields also filled in according to Huffman's Algorithm.
        //In implementing Huffman's Algorithm, make use of the RootedBinaryTree class, with the generic type parameter
        //equal to Word. When Huffman's Algorithm combines two trees, make sure that the plainWord in the left-most leaf
        //is copied into the root of the combined tree. In case of a tie in the probabilities of two Words, use the ASCII values of
        //the plainWords to determine which Word is smaller. Note that RootedBinaryTree should perform shallow copying 
        //to reduce the time- and space-complexity of tree operations.
        public static void huffman(List<Word> theWords)
        {
            //YOUR CODE HERE
            //Step 1: Create a collection of RBTs, for each Word in theWords
            //List<RootedBinaryTree<Word>> trees = new List<RootedBinaryTree<Word>>();
            var trees = new List<RootedBinaryTree<Word>>();
            foreach(Word w in theWords)
            {
                var RBT = new RootedBinaryTree<Word>(w);
                trees.Add(RBT);
            }
            while(trees.Count > 1)// handles Step 4
            {
                //Step 2: find the "smallest" trees in our collection
                trees.Sort();
                // It works because RootedBinaryTree class implemented IComparable.
                //Step 3: Combine these two samllest trees
                var t = trees[0];
                var tPrime = trees[1];
                Word rootData = new Word();
                rootData.probability = t.getData().probability + tPrime.getData().probability;
                rootData.plainWord = t.getData().plainWord;
                var RBT = new RootedBinaryTree<Word>(rootData);
                RBT.combineTrees(t, tPrime);
                trees.RemoveRange(0, 2);
                trees.Add(RBT);
            }
            trees[0].toRoot();
            string tmp = "";
            setCodeWord(theWords, trees[0], tmp);
        }   
        private static void setCodeWord(List<Word> theWords, RootedBinaryTree<Word> tree, string s)
        {
            if (tree.moveLeft())
            {
                s += "0";
                setCodeWord(theWords, tree, s);
            }

            if (tree.moveRight())
            {
                s += "1";
                setCodeWord(theWords, tree, s);
            }
            else
            {
                Word word = tree.getData();
                for(int i=0; i<theWords.Count(); i++)
                {
                    if(word.plainWord == theWords[i].plainWord)
                    {
                        theWords[i].codeWord = s;
                        break;
                    }
                }
            }
            tree.moveUp();
        }

        public static void decompress(string inputFileName, string outputFileName)
        {
            //OPTIONAL: YOUR CODE HERE
        }
    }
}
