using System;
using System.IO;
// This assignment concerns a Compressor class written to fulfill the requirements for Lab #4.
// In each of the tests below, work out the expected results manually and show your work,
// except where indicated that you may write C# code.
// Whenever you write C# code, then show your code along with your answer.

// 0. Research the CompareTo method of the string class in C#,
// and explain why it does not always agree with the ASCII ordering of characters.
// Give an example.
// CompareTo two types parameters, one is string and the other is object.
// ASCII is also a number. For example, 65 means 'A' and 97 means 'a'. If a parameter is not a number, it interprets input as matched character.
// However, if the input parameter is a reference type like class, it receives address value not its own value.

// some text 0x73
// Test Class 0x54 : -1
// 123 0x31 : 1 
// Some text 0x53 : -1

//1.Consider an input file consisting of the following text:
//ADBEBDABCBDBEBABDDBBEEBDABCDBEBABDBC
//Make a table with 3 columns showing:
//(i)each distinct character that appears in the input file;
//(ii)the frequency that the character appears (that is, the number of times the character appears);
//(iii)the probability of the character (that is, the frequency divided by the total number of characters in the file).
//You may write C# code to answer this question.

//2. BY HAND, use Huffman's Algorithm to generate code words for each character in the input file based on your answer to Question 1 above.
//Show each step in the algorithm separately, 
//drawing a new collection of trees at each step.

//3.
//(a) Write at least the first 30 bits of the encoded output corresponding
//   to the original input file using the code you found in Question 2 above.
//(b) Compute the average codeword length for the code you found in Question 2 above.
//   You may write C# code for this.
//(c) Compute the entropy of the input file.
//   You may write C# code for this.
//   Compare this to your answer to question 3(b) above.

//4, 5, 6: Repeat problems 1, 2, 3 using the input file which consists of the following text:
//DCEFDBECEGCCFFAGCBAAEGGAEADCECGHBHAHHDDGGBECFFEHAGCHBHECBEGECEGDAEDHGFDEGHHGAACHGACGCCCGGDHDFAAGBGFF


namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("First Test");
            //new Tester("ADBEBDABCBDBEBABDDBBEEBDABCDBEBABDBC");

            //Console.WriteLine("\n\nSeconde Test");
            //new Tester("DCEFDBECEGCCFFAGCBAAEGGAEADCECGHBHAHHDDGGBECFFEHAGCHBHECBEGECEGDAEDHGFDEGHHGAACHGACGCCCGGDHDFAAGBGFF");
            string testFile = "testfile.txt";
            string resultFile = "result";
            StreamWriter tF = File.CreateText(testFile);
            tF.Write("DCEFDBECEGCCFFAGCBAAEGGAEADCECGHBHAHHDDGGBECFFEHAGCHBHECBEGECEGDAEDHGFDEGHHGAACHGACGCCCGGDHDFAAGBGFF");
            tF.Close();
            Compressor.compress(testFile, resultFile);
        }
    }

    class Tester
    {
        public Tester(string inputString)
        {
            int alphabetSize = 26;
            char[] character = inputString.ToCharArray();
            int[] count = CountInput(character, alphabetSize);
            new Real(character, count);

        }
        private int[] CountInput(char[] character, int alphabetSize)
        {
            int[] count = new int[alphabetSize];
            for (int i = 0; i < character.Length; i++)
            {
                count[(int)(character[i] - 'A')]++;
            }
            return count;
        }
        class Real
        {
            private int size;
            private char[] character;
            private int[] count;
            private double[] probability;
            private string[] codeWord;

            public Real(char[] inputCharacter, int[] inputCount)
            {
                size = getSize(inputCount);
                character = new char[size];
                count = new int[size];
                probability = new double[size];
                setCharacterAndCount(inputCount);
                setProbability(inputCharacter.Length);
                setCodeWord();
                PrintCodeWord(inputCharacter);
                Entropy();
            }


            private int getSize(int[] inputCount)
            {
                int tmp = 0;
                for (int i = 0; i < inputCount.Length; i++)
                {
                    if (inputCount[i] > 0) tmp++;
                }
                return tmp;
            }
            private void setCharacterAndCount(int[] inputCount)
            {
                // To save original data, use copy one instead original one.
                int[] tmpArray = new int[inputCount.Length];
                inputCount.CopyTo(tmpArray, 0);

                for (int i = 0; i < size; i++)
                {
                    int maxValue = 0;
                    int maxIndex = 0;
                    for (int j = 0; j < inputCount.Length; j++)
                    {
                        if (tmpArray[j] > maxValue)
                        {
                            maxValue = tmpArray[j];
                            maxIndex = j;
                        }
                    }
                    character[i] = (char)(maxIndex + 'A');
                    count[i] = maxValue;
                    tmpArray[maxIndex] = 0;
                }
            }
            private void setProbability(int totalLength)
            {
                double tmp2 = (double) totalLength;
                for (int i = 0; i < size; i++)
                {
                    double tmp1 = (double) count[i];
                    Console.WriteLine(character[i] + " | " + count[i] + " | " + count[i] + "/" + totalLength);
                    probability[i] = tmp1 / tmp2;
                }
            }
            private void setCodeWord()
            {
                codeWord = new string[size];
                for (int i = 0; i < size; i++)
                {
                    codeWord[i] = "";
                    for (int j = 0; j < i; j++)
                    {
                        codeWord[i] += "1";
                    }
                    if (i < size - 1)
                    {
                        codeWord[i] += "0";
                    }
                }
                Console.WriteLine();
            }
            private void PrintCodeWord(char[] inputCharacter)
            {
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine(character[i] + " : " + codeWord[i]);
                }
                Console.WriteLine();

                string printing = "";
                for (int i = 0; i < inputCharacter.Length; i++)
                {
                    printing += codeWord[(int)(inputCharacter[i] - 'A')];
                }
                Console.WriteLine(printing);

                Console.WriteLine("Calculating average code word length:");
                double avgLength = 0.0;
                Console.Write(" = ");
                Console.Write(codeWord[0].Length + " * " + probability[0]);
                avgLength += (double)codeWord[0].Length * probability[0];
                for (int i = 1; i < size; i++)
                {
                    Console.Write(" + " + codeWord[i].Length + " * " + probability[i]);
                    avgLength += (double)codeWord[i].Length * probability[i];
                }
                Console.Write(" = " + avgLength + "\n");
            }
            private void Entropy()
            {
                Console.WriteLine("Calculating Entropy:");
                double entropy = 0.0;
                Console.WriteLine(" = sum ( - prob * log_2(prob) )");
                Console.WriteLine(" = - " + probability[0] + " * log_2 (" + probability[0] + ")");
                entropy -= probability[0] * Math.Log2(probability[0]);
                for (int i = 1; i < size; i++)
                {
                    Console.WriteLine(" - " + probability[i] + " * log_2 (" + probability[i] + ")");
                    entropy -= probability[i] * Math.Log2(probability[i]);
                }
                Console.Write(" = " + entropy + "\n");
            }
        } // END CLASS REAL
    }// END CLASS TESTER
}// END NAMESPACE
