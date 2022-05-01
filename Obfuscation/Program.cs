using System;

namespace Obfuscation
{
    internal class Program
    {
        static string GetStringToBeAltered()
        {
            Console.Write("Enter string: ");
            string entry = Console.ReadLine();
            return entry;
        }
        static int GetEncryptionKey()
        {
            int key = 0;
            Console.Write("Enter key: ");
            try
            {
                key = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return key;
        }
        static void DisplayMenu()
        {
            int showMenu = 1;
            do
            {
                int menuSelection = 0;
                Console.WriteLine("What would you like to do?\n1)Encrypt string, providing a key\n2)Encrypt String using random key\n3)Decrypt string, by providing a key");
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
                            string userString = GetStringToBeAltered();
                            int userKey = GetEncryptionKey();
                            Console.WriteLine($"Converted: " + EncryptString(userString, userKey));
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            string userString = GetStringToBeAltered();
                            string[] result = EncryptStringWithRandomKey(userString);
                            Console.WriteLine($"Converted: {result[0]}\nKey: {result[1]}");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case 3:
                        {
                            string userString = GetStringToBeAltered();
                            int userKey = GetEncryptionKey();
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
        static string EncryptString(string stringToBeConverted, int key)
        {
            string encryptedString = "";
            char[] charArray = stringToBeConverted.ToCharArray();
            int[] valueArray = new int[charArray.Length];

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
            return encryptedString;
        }
        static string[] EncryptStringWithRandomKey(string stringToBeConverted)
        {
            Random random = new Random();
            int key = random.Next(1, 99);
            string encryptedString = "";
            char[] charArray = stringToBeConverted.ToCharArray();
            int[] valueArray = new int[charArray.Length];

            foreach (byte b in charArray) charArray.CopyTo(valueArray, 0);

            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < valueArray.Length; k++) valueArray[k]++;
            }

            foreach (int i in valueArray) encryptedString = encryptedString + Convert.ToChar(i);

            string[] result = new string[2] {encryptedString, key.ToString()};

            return result;
        }
        static string DecryptString(string stringToBeConverted, int key)
        {
            char[] charArray = stringToBeConverted.ToCharArray();
            int[] valueArray = new int[charArray.Length];
            string decryptedString = "";

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

