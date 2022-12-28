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

        /// <summary> <para> Semnul numarului mare </para>
        /// <para> Adevarat - Numarul este pozitiv </para>
        /// <para> Fals - Numarul este negatic </para>
        /// </summary>
        private bool sign;

        // ------------------------------------------ CONSTRUCTORS ------------------------------------------

        /// <summary> Default constructor: +0 </summary>
        public BigInt()
        {
            asString = "0";
            asByteList = new List<byte>();
            asByteList.Add(0);

            sign = true;
        }
        
        /// <summary> Constructor din string </summary>
        /// <param name="numar"> Un sir de caractere care reprezinta un numar mare </param>
        public BigInt(string numar)
        {
            if (!Valid(numar))
                throw new FormatException();
            
            // Salveaza stringul
            asString = numar;

            // Salveaza semnul
            if (asString[0] == '-')
                sign = false;
            else
                sign = true;

            // Elimina semnul din string
            if (asString[0] == '+' || asString[0] == '-')
                asString = asString.Substring(1);

            // Salveaza numarul ca tablou de bytes, cu cifrele in ordine inversa
            asByteList = new List<byte>();
            for (int i = asString.Length - 1; i >= 0; i--)
                asByteList.Add((byte)(asString[i] - '0'));
        }

        /// <summary> Constructor din tablou de bytes </summary>
        /// <param name="semn"> Semnul numarului mare </param>
        /// <param name="numar"> O lista de bytes care reprezinta un numar mare </param>
        public BigInt(char semn, List<byte> numar)
        {
            // Salveaza semnul
            if (semn == '+')
                sign = true;
            else if (semn == '-')
                sign = false;
            else
                throw new FormatException();

            // Salveaza tabloul de bytes
            asByteList = numar;

            // Salveaza numarul ca string
            asString = asByteList.ToString();
        }

        /// <summary> Verifica daca un string este un numar mare valid </summary>
        /// <param name="numar"> Numarul care va fi verificat </param>
        /// <returns> 
        /// <para> Adevarat - Este un numar mare valid </para>
        /// <para> Fals - Nu este un numar mare valid </para>
        /// </returns>
        bool Valid(string numar)
        {
            string cifreAcceptate = "0123456789";
            string semneAcceptate = "+-";

            // Semnul apare in interiorul numarului / de mai multe ori
            foreach (var semn in semneAcceptate)
                if (numar.LastIndexOf(semn) > 0)
                    return false;

            // Numarul este format din alte caractere inafara de semn si cifre
            foreach (var caracter in numar)
                if (cifreAcceptate.IndexOf(caracter) == -1 && semneAcceptate.IndexOf(caracter) == -1)
                    return false;

            return true;
        }

        // ------------------------------------------- OPERATORS -------------------------------------------

        /// <summary> Adunarea a 2 numere mari </summary>
        /// <returns> Suma celor 2 numere </returns>
        public static BigInt operator + (BigInt b1, BigInt b2)
        {
            BigInt result = new BigInt();

            if (b1.sign == b2.sign)
            {
                
            }

            return result;
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

        // TODO: -------------------------------------------- EXTRAS ---------------------------------------------

    }
}