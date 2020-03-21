using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static string path = "C:\\Users\\HP\\Desktop\\file1.txt";

        public static string[] vopros = { "2 + 2 = ", "2 * 2 = ", "2 / 2 = ", "2 - 2 = ", "2 ^ 2 = ", };
        public static string[] otvetnavopros = { "4", "4", "1", "0", "4", };
        static string otvet = "";
        static void Main(string[] args)
        {
            int i = 0;
            while (i != 5)
            {
                do
                {
                    Console.Write($"Вопрос №{i + 1}: {vopros[i]} ");
                    otvet = Console.ReadLine();
                    save(otvet, vopros[i], i);
                } while (otvetnavopros[i] != otvet);
                i++;
            }

        }
        static void save(string ot, string vop, int i)
        {
            using (StreamWriter text = new StreamWriter(path, true))
            {
                text.Write($"Вопрос №{i + 1}: {vopros[i]} ");
                text.WriteLine(ot);
            }
        }
    }
}
