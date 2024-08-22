using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Business.Tools
{
    public class Crypt
    {
        static char[] chars = new char[37] { '7','D', 'S', 'N','0', 'L', 'G', 'İ','8', 'O', 'V', 'B','3', 'X', 'M', '5','A', 'P', 'R', 'C', '2','Q', 'W', 'Y','4','6', 'F', 'Z', 'U', 'I', 'E','1', 'H', 'T', 'J', 'K','9' };

        //static char[] letters= new char[27]{'D','S','N','L','G','İ','O','V','B','X','M','A','P','R','C','Q','W','Y','F','Z','U','I','E','H','T','J','K'};
        //static int[] numbers = new int[10] {8,5,2,6,0,1,7,3,4,9};
        public static string Encrypt(string notEncryptedWord)
        {
            Random random = new Random();

            string encryptedWord = "";
            //+ numbers[random.Next(0,9)] + letters[random.Next(0,26)];
            for(int i =0; i < 13; i++)
            {
                encryptedWord += chars[random.Next(0, 36)];
            }
            var index = 0;
            foreach (char item in notEncryptedWord.ToCharArray())
            {
                index++;

                int letterIndex = Array.IndexOf(chars, item);
                int cryptIndex = letterIndex + index;
                if (cryptIndex >= chars.Length)
                {
                    cryptIndex -= chars.Length;
                }
                encryptedWord += chars[cryptIndex];

            }

            for(int i = 0; i < 23; i++)
            {
                encryptedWord += chars[random.Next(0, 36)];
            }
            
            return encryptedWord;
        }

        public static string Decrypt(string encryptedWordx) 
        {
            string decryptedWord = "";
            string baseWord = encryptedWordx.Substring(13, 12);
            var index = 0;
            foreach (char item in baseWord.ToCharArray())
            {
                index++;
                
                    int cryptIndex = Array.IndexOf(chars, item) - index;
                    if (cryptIndex < 0)
                    {
                        cryptIndex += chars.Length;
                    }
                    decryptedWord += chars[cryptIndex];
                
                                             
            }
            return decryptedWord; 
        }
    }
}
