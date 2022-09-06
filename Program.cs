using System;

namespace Task3 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] a2 = {55, 25, 89, 34, 12, 19, 78, 95, 1, 100};
            int[] a1 = {90, 49, 200, 32};
        
            Task.GetCountForArray(a1, a2);
            for (int i = 0; i < a1.Length; i++) 
            {
                Console.Write(a1[i] + " ");
            }
        }
    }
}
