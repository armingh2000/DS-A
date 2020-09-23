using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(MaxPrefSuf("baababacc", "cabcaab"));
            //Console.ReadLine();
            List<string> ls = new List<string>();
            ls.Add("AAC");
            ls.Add("ACG");
            ls.Add("GAA");
            ls.Add("GTT");
            ls.Add("TCG");
            //string line;
            //for(int i=0;i<1618;i++)
            //{
            //    line = Console.ReadLine();
            //    ls.Add(line);
            //}

            Console.WriteLine(CreateCommonArray(ls));
            Console.ReadKey();


        }

        public static void PrintArray(long[][] array)
        {
            for(long i=0;i<array.Length;i++)
            {
                for(long j=0;j<array[0].Length;j++)
                {
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
            }
        }




        public static string CreateCommonArray(List<string> listOfStr)
        {
            bool[] visited = new bool[listOfStr.Count];
            //string currentStr = listOfStr[0];
            StringBuilder s = new StringBuilder();
            long[][] commonPrefWithSuff = new long[listOfStr.Count][];

            for(long i=0;i<listOfStr.Count;i++)
            {
                commonPrefWithSuff[i] = new long[listOfStr.Count];
                for(long j=0;j<listOfStr.Count;j++)
                {
                    if(i!=j)
                        commonPrefWithSuff[i][j] = FindLongestCommonPrefixAndSUffixOfTwoString(listOfStr[(int)j], listOfStr[(int)i]);
                }
            }

            //return commonPrefWithSuff;
            PrintArray(commonPrefWithSuff);

            long temp = 0;
            while (temp != -1)
            {
                visited[temp] = true;
                long max = 0;
                long maxIdx = -1;
                for (long j = 0; j < listOfStr.Count; j++)
                {
                    if (commonPrefWithSuff[temp][j] > 0 && !visited[j] && commonPrefWithSuff[temp][j] >= max)
                    {
                        max = commonPrefWithSuff[temp][j];
                        maxIdx = j;
                    }
                }

                temp = maxIdx;

                if (maxIdx != -1)
                {
                    if (!visited[maxIdx])
                    {
                        s.Append(listOfStr[(int)maxIdx].ToCharArray(), (int)max, (int)(listOfStr[(int)maxIdx].Length - max));
                        visited[temp] = true;
                    }
                        
                }
            }

            #region
            //visited[0] = true;

            //long temp = 0;
            //while(temp!=-1)
            //{
            //    long max = 0;
            //    long maxIdx = -1;
            //    for (long j=0;j<listOfStr.Count;j++)
            //    {
            //        if(commonPrefWithSuff[temp][j]>0 && !visited[j] && commonPrefWithSuff[temp][j]>=max)
            //        {
            //            max = commonPrefWithSuff[temp][j];
            //            maxIdx = j;
            //        }
            //    }

            //    temp = maxIdx;

            //    if(maxIdx!=-1)
            //    {
            //        if(!visited[maxIdx])
            //        s.Append(listOfStr[(int)maxIdx].ToCharArray(), (int)max, (int)(listOfStr[(int)maxIdx].Length - max));
            //        visited[temp] = true;
            //    }
            //}
            #endregion
            return s.ToString();



            //while (listOfStr.Count > 0)
            //{
            //    long max = 0;
            //    long maxIdx = -1;

            //    for (long j = 0; j < listOfStr.Count; j++)
            //    {
            //        if (/*idx!=j && */
            //            max <= (temp = FindLongestCommonPrefixAndSUffixOfTwoString(listOfStr[(int)j], currentStr)))
            //        {
            //            max = temp;
            //            maxIdx = j;
            //        }
            //    }

            //    s.Append(listOfStr[(int)maxIdx].ToCharArray(), (int)max, (int)(listOfStr[(int)maxIdx].Length - max));
            //    currentStr = listOfStr[(int)maxIdx];
            //    listOfStr.RemoveAt((int)maxIdx);
            //}

            //return s.ToString();
        }


        //public static string Function(List<string> listOfStr)
        //{
        //    string currentStr = listOfStr[0];
        //    StringBuilder s = new StringBuilder(currentStr);

        //    listOfStr.RemoveAt(0);

        //    //long idx = 0;
        //    long temp;

        //    while (listOfStr.Count > 0)
        //    {
        //        long max = 0;
        //        long maxIdx = -1;

        //        for (long j = 0; j < listOfStr.Count; j++)
        //        {
        //            if(/*idx!=j && */
        //                max<=(temp = FindLongestCommonPrefixAndSUffixOfTwoString(listOfStr[(int)j], currentStr)))
        //            {
        //                max = temp;
        //                maxIdx = j;
        //            }
        //        }

        //        s.Append(listOfStr[(int)maxIdx].ToCharArray(), (int)max, (int)(listOfStr[(int)maxIdx].Length - max));
        //        currentStr = listOfStr[(int)maxIdx];
        //        listOfStr.RemoveAt((int)maxIdx);
        //    }

        //    return s.ToString();
        //}

        #region
        public class OverLapGraph
        {

        }

        public class Node
        {
            public string Content;

            public Node() { }

            public Node(string content)
            {
                this.Content = content;
            }
        }
        #endregion

        public static string PatternPlusText(string pattern, string text)
        {
            return pattern + "$" + text;
        }

        public static long[] MyPrefixFunction(string str)
        {
            long length = str.Length;
            long[] Prefixes = new long[length];

            Prefixes[0] = 0;

            for (long i = 1; i < length; i++)
            {
                //if (Prefixes[i - 1] == 0)
                //{
                //    if (str[0] != str[(int)i])
                //    {
                //        Prefixes[i] = 0;
                //    }
                //    else
                //    {
                //        Prefixes[i] = 1;
                //    }
                //    continue;
                //}



                if (str[(int)i] == str[(int)Prefixes[i - 1]])
                {
                    Prefixes[i] = Prefixes[i - 1] + 1;
                }
                else
                {

                    long temp = Prefixes[i - 1];

                    while (temp > 0 && str[(int)temp] != str[(int)i])
                    {
                        temp = Prefixes[temp - 1];
                    }

                    if (temp == 0 && str[0] != str[(int)i])
                    {
                        Prefixes[i] = 0;
                    }
                    else
                    {
                        Prefixes[i] = temp + 1;
                    }
                }
            }

            return Prefixes;
        }

        public static long[] SlidesPrefixFunction(string str)
        {
            long length = str.Length;
            long[] Prefixes = new long[length];
            long border = 0;

            Prefixes[0] = 0;

            for (long i = 1; i < length; i++)
            {
                while (border > 0 && str[(int)i] != str[(int)border])
                {
                    border = Prefixes[border - 1];
                }

                if (str[(int)i] == str[(int)border])
                {
                    border = border + 1;
                }
                else
                {
                    border = 0;
                }
                Prefixes[i] = border;
            }

            return Prefixes;
        }


        public static long FindLongestCommonPrefixAndSUffixOfTwoString(string pattern, string text)
        {
            string str = PatternPlusText(pattern, text);

            long[] LengthOfBorders = MyPrefixFunction(str);
            long lengthOfPattern = pattern.Length;

            return LengthOfBorders[LengthOfBorders.Length - 1];
        }

        #region
        #region
        //public static List<long> FindAllOccurences(string pattern, string text)
        //{
        //    //List<long> ListOfOccurences = new List<long>();
        //    string str = PatternPlusText(pattern, text);

        //    long[] LengthOfBorders = MyPrefixFunction(str);
        //    long lengthOfPattern = pattern.Length;

        //    return LengthOfBorders[LengthOfBorders.Length - 1];

        //    //for (long i = lengthOfPattern + 1; i < str.Length; i++)
        //    //{
        //    //    if (LengthOfBorders[i] == lengthOfPattern)
        //    //    {
        //    //        ListOfOccurences.Add(i - 2 * lengthOfPattern);
        //    //    }
        //    //}

        //    //return ListOfOccurences;
        //}
        #endregion

        #region
        //public static long[] Solve(string s1/*text*/, string s2/*pattern*/)
        //{
        //    var v = FindAllOccurences(s1/*pattern*/, s2/*text*/).ToArray();
        //    if (v.Length == 0)
        //    {
        //        return new long[] { -1 };
        //    }
        //    else
        //    {
        //        return v;
        //    }
        //}
        #endregion
        #endregion

        public static long MaxPrefSuf(string s1, string s2)
        {
            return Math.Max(FindLongestCommonPrefixAndSUffixOfTwoString(s1, s2),
                            FindLongestCommonPrefixAndSUffixOfTwoString(s2, s1));
        }

    }
}
