using System;
using BigInteger;

// Implementati operatii cu numere naturale mari.
// Operatii: Adunare, scadere, inmultire, impartire, ridicare la putere, radacina patrata.
// Operatorii relationali: <, >, <=, >=, operatorii de egalitate/inegalitate. 
// Precizari: Numar mare = un numar care nu poate fi reprezentat pe int/long etc.
//            Numarul poate avea sute/mii/... de cifre.

namespace ASC_Big_Integer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BigInt b1 = new BigInt("123451234512345");
                BigInt b2 = new BigInt("1000000000000000000000");

                Console.WriteLine($"b1 + b2 = {b1 + b2}");

                // Console.WriteLine($"b1 - b2 = {b1 - b2}");
                Console.WriteLine($"b2 - b1 = {b2 - b1}");

                Console.WriteLine($"b1 * b2 = {b1 * b2}");

                Console.WriteLine($"b1 / b2 = {b1 / b2}");
                Console.WriteLine($"b1 % b2 = {b1 % b2}");

                Console.WriteLine($"b1 ^ 14 = {BigInt.Pow(b1, 14)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}