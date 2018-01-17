using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotatingGridConsole
{
    class RotatingGrid
    {
        public int[,] KeyMatrix
        {
            get
            {
                return new int[4, 4] {
                    { 1, 2, 3, 1 },
                    { 3, 4, 4, 2 },
                    { 2, 4, 4, 3 },
                    { 1, 3, 2, 1 }
            };
            }
        }

        public static int[,] GetIndex(int[,] keyMatrix)
        {
            var size = keyMatrix.GetLength(0);
            int[,] _matrix = new int[size, size];
            var temp = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (keyMatrix[i, j] == temp)
                    {
                        _matrix[i, j] = keyMatrix[i, j];
                        temp--;
                    }
                }
            }
            return _matrix;
        }

        public static string EncryptText(int blocksize, string text, int[,] indexMatrix)
        {
            string encryptedText = "";
            text = text.Replace(" ", "*");
            for (int i = 0; i < text.Length; i += blocksize)
            {
                if (i + blocksize > text.Length)
                {
                    text = text.PadRight(i + blocksize, '*');
                }
                var str = text.Substring(i, blocksize);
                var encrypted = Encrypt(str, indexMatrix);
                encryptedText += encrypted;
            }
            return encryptedText;
        }

        public static string Encrypt(string str, int[,] indexMatrix)
        {
            var size = indexMatrix.GetLength(0);

            var _matrix = new char[size, size];

            var len = str.Length;

            for (int i = 0; i < len; i += size)
            {
                var arr = str.Substring(i, size).ToArray();
                for (int k = 0; k < size; k++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (indexMatrix[k, j] != 0)
                        {
                            _matrix[k, j] = arr[indexMatrix[k, j] - 1];
                        }
                    }
                }
                _matrix = Rotate(_matrix);
            }

            string encrypted = "";

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {                   
                        encrypted += _matrix[i, j];
                }
            }

            return encrypted;
        }

        public static string DecryptText(int blocksize, string text, int[,] indexMatrix)
        {
            string decryptedText = "";
            for (int i = 0; i < text.Length; i += blocksize)
            {
                var str = text.Substring(i, blocksize);
                var decrypted = Decrypt(str, indexMatrix);
                decryptedText += decrypted;
            }
            decryptedText.TrimEnd('*');
            decryptedText = decryptedText.Replace('*', ' ' );
           
            return decryptedText;
        }

        public static string Decrypt(string str, int[,] indexMatrix)
        {
            var size = indexMatrix.GetLength(0);
            var _matrix = new char[size, size];
            var arr = str.ToArray();
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _matrix[i, j] = arr[k];
                    k++;
                }
            }
            string decrypted = "";
            int len = str.Length;

            while (len != 0)
            {
                var decryptedArr = new char[size] ;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        
                        if (indexMatrix[i, j] != 0)
                        {
                            decryptedArr[indexMatrix[i, j] - 1] = _matrix[i, j];
                        }
                    }
                }
                _matrix = Rotate(_matrix);
                len -= size;
                for (int i = 0; i < decryptedArr.Length; i++)
                {
                    decrypted += decryptedArr[i];
                }
            }
            return decrypted;
        }

        public static char[,] Rotate(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            char[,] _matrix = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _matrix[j, size - i - 1] = matrix[i, j];
                }
            }
            return _matrix;
        }
    }
}
