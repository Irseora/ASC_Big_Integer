using System;
using System.Text;
using System.Collections.Generic;

namespace BigInteger
{
    /// <summary>  </summary>
    internal class BigInt
    {
        // --------------------------------------------- DATA ----------------------------------------------

        /// <summary> Numarul mare reprezentat printr-un sir de caractere </summary>
        private string asString;

        /// <summary> Numarul mare reprezentat printr-un tablou de bytes, cifrele in ordine inversa </summary>
        List<byte> asByteList;

        // ------------------------------------------ CONSTRUCTORS ------------------------------------------

        /// <summary> Default constructor: 0 </summary>
        public BigInt()
        {
            asString = "";
            asByteList = new List<byte>();
        }
        
        /// <summary> Constructor din string </summary>
        /// <param name="numar"> Un sir de caractere care reprezinta un numar mare </param>
        public BigInt(string numar)
        {
            if (!Valid(numar))
                throw new FormatException();
            
            // Salveaza stringul
            asString = numar;

            // Salveaza numarul ca tablou de bytes, cu cifrele in ordine inversa
            asByteList = new List<byte>();
            for (int i = asString.Length - 1; i >= 0; i--)
                asByteList.Add((byte)(asString[i] - '0'));
        }

        /// <summary> Constructor din tablou de bytes </summary>
        /// <param name="numar"> O lista de bytes care reprezinta un numar mare </param>
        public BigInt(List<byte> numar)
        {
            // Salveaza tabloul de bytes
            asByteList = numar;

            // Salveaza numarul ca string
            asString = asByteList.ToString();
        }

        /// <summary> Verifica daca un string este un numar mare valid </summary>
        /// <param name="numar"> Numarul care va fi verificat </param>
        /// <returns> 
        /// <para> Adevarat - Numarul este valid </para>
        /// <para> Fals - Numarul nu este valid </para>
        /// </returns>
        bool Valid(string numar)
        {
            // Numarul este format din alte caractere inafara de cifre
            string cifreAcceptate = "0123456789";
            foreach (var caracter in numar)
                if (cifreAcceptate.IndexOf(caracter) == -1)
                    return false;

            return true;
        }

        // ---------------------------------------- ARITHMETIC OPERATORS ----------------------------------------

        /// <summary> Adunarea a 2 numere mari </summary>
        /// <returns> Suma celor 2 numere </returns>
        public static BigInt operator + (BigInt b1, BigInt b2)
        {
            BigInt result = new BigInt();

            for (int i = 0, carry = 0; i < b1.asByteList.Count || i < b2.asByteList.Count; i++)
            {
                // Ia cifra daca ea exista, ia 0 altfel
                int digit1 = i < b1.asByteList.Count ? b1.asByteList[i] : 0;
                int digit2 = i < b2.asByteList.Count ? b2.asByteList[i] : 0;

                int sumOfDigits = digit1 + digit2 + carry;
                result.asByteList.Add((byte)(sumOfDigits % 10));
                carry = sumOfDigits / 10;
            }

            result.asString = result.asByteList.ToString();
            return result;
        }

        /// <summary> Scaderea a 2 numere mari </summary>
        /// <returns> Diferenta celor 2 numere </returns>
        public static BigInt operator - (BigInt b1, BigInt b2)
        {
            BigInt result = new BigInt();

            if (b2 > b1)
            {
                throw new Exception("Primul numar trebuie sa fie mai mare decat cel de al doilea.");
            }

            for (int i = 0; i < b1.asByteList.Count; i++)
            {
                // Ia cifra daca ea exista, ia 0 altfel
                int digit1 = i < b1.asByteList.Count ? b1.asByteList[i] : 0;
                int digit2 = i < b2.asByteList.Count ? b2.asByteList[i] : 0;

                // Daca prima cifra este mai mare sau egala, se poate efectua
                if (digit1 >= digit2)
                    result.asByteList.Add((byte)(digit1 - digit2));
                // Daca prima cifra este mai mica, trebuie "imprumutat"
                else
                {
                    // Cauta prima cifra nenula
                    int j = i+1;
                    while (j < b2.asByteList.Count && b2.asByteList[j] == 0)
                        j++;

                    // "Imprumuta" din cifra gasita
                    b2.asByteList[j]--;

                    result.asByteList.Add((byte)(digit1 + 10 - digit2));
                }
            }

            result.asString = result.asByteList.ToString();
            return result;
        }

        /// <summary> Inmultirea a 2 numere mari </summary>
        /// <returns> Rezultatul inmultirii celor 2 numere </returns>
        public static BigInt operator * (BigInt b1, BigInt b2)
        {
            BigInt result = new BigInt();

            for (int j = 0; j < b2.asByteList.Count; j++)
            {
                for (int i = 0, carry = 0; i < b1.asByteList.Count; i++)
                {
                    // Ia cifra daca ea exista, ia 0 altfel
                    int digit1 = i < b1.asByteList.Count ? b1.asByteList[i] : 0;
                    int digit2 = j < b2.asByteList.Count ? b2.asByteList[i] : 0;

                    int mulOfDigits = digit1 * digit2 + carry;
                    carry = mulOfDigits / 10;

                    if (j == 0)
                        result.asByteList.Add((byte)(mulOfDigits % 10));
                    else
                        result.asByteList[j + i] += (byte)(mulOfDigits % 10);
                }
            }

            return result;
        }

        /// <summary> Impartirea a 2 numere mari </summary>
        /// <returns> Catul celor 2 numere </returns>
        public static BigInt operator / (BigInt b1, BigInt b2)
        {
            return Division(b1, b2, "cat");
        }

        /// <summary> Impartirea cu rest a 2 numere mari </summary>
        /// <returns> Restul impartirii celor 2 numere </returns>
        public static BigInt operator % (BigInt b1, BigInt b2)
        {
            return Division(b1, b2, "rest");
        }

        // ---------------------------------------- LOGICAL OPERATORS ----------------------------------------

        /// <summary> Determina daca 2 numere mari sunt egale </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 = b2 </para>
        /// <para> Fals - daca b1 != b2 </para>
        /// </returns>
        public static bool operator == (BigInt b1, BigInt b2)
        {
            if (b1.asString.Length != b2.asString.Length)
                return false;
            else if (b1.asString.CompareTo(b2.asString) == 0)
                return true;

            return false;
        }

        /// <summary> Determina daca 2 numere mari sunt diferite </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 != b2 </para>
        /// <para> Fals - daca b1 = b2 </para>
        /// </returns>
        public static bool operator != (BigInt b1, BigInt b2)
        {
            return !(b1 == b2);
        }

        /// <summary> Determina daca primul numar este strict mai mare decat cel de al doilea </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 > b2 </para>
        /// <para> Fals - daca b1 <= b2 </para>
        /// </returns>
        public static bool operator > (BigInt b1, BigInt b2)
        {
            if (b1.asString.Length > b2.asString.Length)
                return true;
            else if (b1.asString.Length < b2.asString.Length)
                return false;
            else if (b1.asString.CompareTo(b2.asString) > 0)
                return true;
                
            return false;
        }

        /// <summary> Determina daca primul numar este strict mai mic decat cel de al doilea </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 < b2 </para>
        /// <para> Fals - daca b1 >= b2 </para>
        /// </returns>
        public static bool operator < (BigInt b1, BigInt b2)
        {
            if (b1 > b2 || b1 == b2)
                return false;

            return true;
        }

        /// <summary> Determina daca primul numar este mai mic sau egal cu cel de al doilea </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 <= b2 </para>
        /// <para> Fals - daca b1 > b2 </para>
        /// </returns>
        public static bool operator <= (BigInt b1, BigInt b2)
        {
            return !(b1 > b2);
        }

        /// <summary> Determina daca primul numar este mai mare sau egal cu cel de al doilea </summary>
        /// <returns> 
        /// <para> Adevarat - daca b1 >= b2 </para>
        /// <para> Fals - daca b1 < b2 </para>
        /// </returns>
        public static bool operator >= (BigInt b1, BigInt b2)
        {
            return !(b1 < b2);
        }

        // ---------------------------------------- ARITHMETIC OPERATIONS ----------------------------------------

        /// <summary> Ridicarea unui numar mare la o putere (numar simplu) </summary>
        /// <returns> Rezultatul ridicarii la putere </returns>
        public static BigInt Pow(BigInt baza, int putere)
        {
            BigInt result = baza;

            for (int i = 2; i <= putere; i++)
                result *= baza;

            return result;
        }

        /// <summary> Ridicarea unui numar mare la o putere (numar mare) </summary>
        /// <returns> Rezultatul ridicarii la putere </returns>
        public static BigInt Pow(BigInt baza, BigInt putere)
        {
            return new BigInt();
        }

        /// <summary> Calculeaza radacina patrata a unui numar mare </summary>
        /// <returns> Radacina patrata a numarului mare </returns>
        public static BigInt Sqrt(BigInt b)
        {
            // return Pow(b, 0.5);
            return new BigInt();
        }
        
        // ------------------------------------------- OVERRIDES -------------------------------------------

        /// <summary> Transforma un tablou de bytes intr-un string </summary>
        /// <returns> Un string care reprezinta tabloul de bytes </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = asByteList.Count - 1; i >= 0; i--)
                sb.Append(asByteList[i].ToString());

            return sb.ToString();
        }

        // -------------------------------------------- HELPERS ---------------------------------------------

        /// <summary> Determina daca un numar mare are toate cifrele egale cu 0 </summary>
        /// <returns> 
        /// <para> Adevarat - Numarul are toate cifrele 0 </para>
        /// <para> Fals - altfel </para>
        /// </returns>
        static bool AllZero(BigInt numar)
        {
            for (int i = 0; i < numar.asByteList.Count; i++)
                if (numar.asByteList[i] != 0)
                    return false;

            return true;
        }

        /// <summary> Determina catul si restul a 2 numere mari, si returneaza cel cerut </summary>
        /// <param name="b1">  </param>
        /// <param name="b2">  </param>
        /// <param name="ret"> Ce trebuie returnat: cat / rest </param>
        /// <returns> 
        /// <para> Pentru ret="cat": Catul impartirii </para>
        /// <para> Pentru ret="rest": Restul impartirii </para>
        /// </returns>
        static BigInt Division(BigInt b1, BigInt b2, string ret)
        {
            // Nu se poate imparti cu 0
            if (AllZero(b2))
                throw new DivideByZeroException();

            // 0 impartit la oricat este 0
            if (AllZero(b1))
                return new BigInt("0");

            BigInt cat = b1;
            BigInt rest = new BigInt("0");

            while (rest >= b2)
            {
                rest -= b2;
                cat += new BigInt("1");
            }

            if (ret == "cat")
                return cat;
            else if (ret == "rest")
                return rest;
            else
                return new BigInt("0");
        }
    
        public static void Write(BigInt b)
        {
            Console.Write(b.asString);
        }
    }
}