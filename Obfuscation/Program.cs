using System;

namespace Obfuscation
{
    internal class Program
    {
        static string[] GetStringAndKeyFromUser()
        {
            Console.Write("Enter string: ");
            string stringToBeAltered = Console.ReadLine();
            Console.Write("Enter Encyrption (Leave blank to use random key)\nKey: ");
            string key = Console.ReadLine();
            string[] result = new string[2] {stringToBeAltered, key};
            return result;
        }
        static void DisplayMenu()
        {
            int showMenu = 1;
            do
            {
                int menuSelection = 0;
                Console.WriteLine("What would you like to do?\n1)Encrypt phrase\n2)Decrypt phrase");
                Console.Write("Selection: ");
                try
                {
                    menuSelection = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.Clear();
                switch (menuSelection)
                {
                    case 1:
                        {
                            string[] entry = GetStringAndKeyFromUser();
                            string userString = entry[0];
                            string userKey = entry[1];
                            string[] result = EncryptString(userString, userKey);
                            Console.WriteLine($"Converted: {result[0]}");
                            if (result[2] == "")
                            {
                                Console.WriteLine($"Key: {result[1]}");
                            }
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            string[] entry = GetStringAndKeyFromUser();
                            string userString = entry[0];
                            string userKey = entry[1];
                            Console.WriteLine($"Converted: " + DecryptString(userString, userKey));
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Try again");
                            showMenu = 0;
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                } 
            } while (showMenu == 0);
        }
        static string[] EncryptString(string stringToBeConverted, string sKey)
        {
            int key = 0;
            Random random = new Random();
            string encryptedString = "";
            char[] charArray = stringToBeConverted.ToCharArray();
            int[] valueArray = new int[charArray.Length];

            if (sKey == "")
            {
                key = random.Next(1, 99);
            }
            else
            {
                try
                {
                    key = Int32.Parse(sKey);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (byte b in charArray)
            {
                charArray.CopyTo(valueArray, 0);
            }

            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < valueArray.Length; k++)
                {
                    valueArray[k]++;
                }
            }
            
            foreach (int i in valueArray)
            {
                encryptedString = encryptedString + Convert.ToChar(i);
            }

            string[] result = new string[3] { encryptedString, key.ToString(), sKey };
            return result;
        }
        static string DecryptString(string stringToBeConverted, string sKey)
        {
            int key = 0;
            char[] charArray = stringToBeConverted.ToCharArray();
            int[] valueArray = new int[charArray.Length];
            string decryptedString = "";
            try
            {
                key = Int32.Parse(sKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            foreach (byte b in charArray)
            {
                charArray.CopyTo(valueArray, 0);
            }

            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < valueArray.Length; k++)
                {
                    valueArray[k]--;
                }
            }
            
            foreach (int i in valueArray)
            {
                decryptedString = decryptedString + Convert.ToChar(i);
            }
            return decryptedString;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
            }
        }
    }
}

