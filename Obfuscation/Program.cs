using System;
using System.IO;

namespace Obfuscation
{
    internal class Program
    {
        static void ClearOnKeyPress()
        {
            Console.ReadLine();
            Console.Clear();
        }
        static int GetKey()
        {
            Random random = new Random();
            int key = 0;
            Console.Write("Enter Encryption (Leave blank to use random key)\nKey: ");
            string sKey = Console.ReadLine();

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
            return key;
        }
        static string GetPhrase()
        {
            Console.Write("Enter Phrase: ");

            string phrase = Console.ReadLine();
            return phrase;
        }
        static string GetFilePath()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.Write($"Enter File Name: {desktopPath}\\");
            string fileName = Console.ReadLine();
            string filePath = desktopPath + $"\\{fileName}";

            return filePath;
        }
        static string[] OpenFile(string filePath)
        {
            string[] document = File.ReadAllLines(filePath);
            return document;
        }
        static void SaveFile(string filePath, string[] document)
        {
            File.WriteAllLines(filePath, document);
        }
        static string Encrypt(string textToEncrypt, int key)
        {
            string encryptedString = "";
            char[] charArray = textToEncrypt.ToCharArray();
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
        static string Decrypt(string textToDecrypt, int key)
        {
            char[] charArray = textToDecrypt.ToCharArray();
            int[] valueArray = new int[charArray.Length];
            string decryptedPhrase = "";

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
                decryptedPhrase = decryptedPhrase + Convert.ToChar(i);
            }
            return decryptedPhrase;
        }
        static string[] HandleEncryptPhraseRequest(string stringToBeConverted)
        {
            int key = GetKey();
            string encryptedString = Encrypt(stringToBeConverted, key);
            string[] result = new string[2] {encryptedString, key.ToString()};

            return result;
        }
        static string[] HandleDecryptionRequest(string stringToBeConverted)
        {
            int key = GetKey();
            string decryptedPhrase = Decrypt(stringToBeConverted, key);
            string[] result = new string[2] { decryptedPhrase, key.ToString() };

            return result;
        }
        static void HandleEncryptDocumentRequest(string filePath)
        {
            int key = GetKey();
            int lineCount = 0;
            string[] document = OpenFile(filePath);
            string[] encryptedDocument = new string[document.Length];

            foreach (string line in document)
            {
                encryptedDocument[lineCount] = Encrypt(line, key);
                lineCount++;
            }

            SaveFile(filePath, encryptedDocument);
            Console.WriteLine($"File Encrypted & Saved at: {filePath}");
        }
        static void HandleDecryptDocumentRequest(string filePath)
        {
            int key = GetKey();
            int lineCount = 0;
            string[] document = OpenFile(filePath);
            string[] encryptedDocument = new string[document.Length];

            foreach (string line in document)
            {
                encryptedDocument[lineCount] = Decrypt(line, key);
                lineCount++;
            }

            SaveFile(filePath, encryptedDocument);
            Console.WriteLine($"File Decrypted & Saved at: {filePath}");
        }
        static void DisplayMenu()
        {
            int showMenu = 0;
            do
            {
                int menuSelection = 0;
                Console.WriteLine("What would you like to do?\n1)Encrypt phrase\n2)Encrypt Word Document\n3)Decrypt phrase\n4)Decrypt Word Document");
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
                            string phraseToEncrypt = GetPhrase();
                            string[] result = HandleEncryptPhraseRequest(phraseToEncrypt);
                            Console.WriteLine($"Converted: {result[0]}\nKey: {result[1]}");
                            ClearOnKeyPress();
                            break;
                        }
                    case 2:
                        {
                            string filePath = GetFilePath();
                            HandleEncryptDocumentRequest(filePath);
                            ClearOnKeyPress();
                            break;
                        }
                    case 3:
                        {
                            string phraseToDecrypt = GetPhrase();
                            string[] result = HandleDecryptionRequest(phraseToDecrypt);
                            Console.WriteLine($"Converted: {result[0]}\nKey: {result[1]}");
                            ClearOnKeyPress();
                            break;
                        }
                    case 4:
                        {
                            string filePath = GetFilePath();
                            HandleDecryptDocumentRequest(filePath);
                            ClearOnKeyPress();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Try again");
                            showMenu = 1;
                            ClearOnKeyPress();
                            break;
                        }
                }
            } while (showMenu == 1);
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

