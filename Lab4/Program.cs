using System;
// This assignment concerns a Compressor class written to fulfill the requirements for Lab #4.
// In each of the tests below, work out the expected results manually and show your work,
// except where indicated that you may write C# code.
// Whenever you write C# code, then show your code along with your answer.

// 0. Research the CompareTo method of the string class in C#,
// and explain why it does not always agree with the ASCII ordering of characters.
// Give an example.
// CompareTo 

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
            string s = "Hello, World!";
            s.CompareTo()
            
            Console.WriteLine("Hello World!");
        }
    }
}
