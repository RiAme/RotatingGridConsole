using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotatingGridConsole;

namespace RotatingGridConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           var key = new RotatingGrid();
            var keymatrix = key.KeyMatrix;
            var blocksize = key.KeyMatrix.Length;
            var matrix = RotatingGrid.GetIndex(keymatrix);

            Console.WriteLine("Enter text to be encrypted:\n");
            var text = Console.ReadLine();
           // var str = text.Replace(" ", "_");
            var encrypted = RotatingGrid.EncryptText(blocksize, text, matrix);

            Console.WriteLine("Encrypted text:");
            Console.WriteLine(encrypted);
            Console.WriteLine();

            var decrypted = RotatingGrid.DecryptText(blocksize, encrypted, matrix);

            Console.WriteLine("Decrypted text:");
            Console.WriteLine(decrypted);
            Console.ReadLine();


        }
    }
}
