using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    class Program
    {
        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        static void Victory(string[] arr,int value,int stepComp)
        {
            Console.WriteLine("Ваш ход: " + arr[value - 1]+ "\nХод компьютера: " + arr[stepComp]);
            int a = (stepComp + arr.Length - value+1) % arr.Length;
            Console.WriteLine(a <= arr.Length / 2 && a > 0 ? "Вы проиграли" : a >= arr.Length - arr.Length / 2 ? "Вы выйграли!" : "Ничья");
        }
        static void DrawMenu(string[] arr)
        {
            Console.WriteLine("Выберите свой ход: ");
            for (int i = 0; i < arr.Length; i++)  Console.WriteLine((i + 1) + "-" + arr[i]);
            Console.WriteLine("0-Выход");
        }
        static void Main(string[] args)
        {
            if (args.Length >= 3 && args.Length % 2 == 1 && args.Length != 0)
            {
                
                while (true)
                {
                    byte[] bytes = new byte[16];
                    new RNGCryptoServiceProvider().GetBytes(bytes);
                    string key = BitConverter.ToString(bytes).Replace("-", "");

                    int stepComp = new Random().Next(0, args.Length - 1), input = -1;
                    bool replay = false;
                    Console.WriteLine("HMAC: " + BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.Unicode.GetBytes(key))).Replace("-", ""));
                    do
                    {
                        DrawMenu(args);
                        Console.Write("Вы выбрали: ");
                        replay = !int.TryParse(Console.ReadLine(), out input);
                        if (!replay) replay = !(input >= 0 && input <= args.Length);
                    } while (replay);

                    if (input > 0 && input <= args.Length)
                        Victory(args, input, stepComp);
                    else
                        break;
                    Console.WriteLine("HMAC key: " + key + "\n");
                }
            }
            else
                Console.WriteLine("Введены неверные данные, введите от 3 и больше нечётных значений \n Например: Камень, ножницы, бумага");
        }
    }
}
