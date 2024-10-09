using System;
using System.Numerics;
using System.Windows.Forms;

namespace LiczbyPierwszeForms
{
    public class PrimaryNumbersRepository
    {
        public BigInteger BiggestPrimeNumber { get; set; }
        public BigInteger CurrentNumber { get; set; }
        public int Step { get; set; }

        public PrimaryNumbersRepository()
        {
            Step = 1;
            BiggestPrimeNumber = 2;
            CurrentNumber = 2;
        }

        public void SetCurrentNumber(BigInteger currentNumber)
        {
            CurrentNumber = currentNumber;
        }

        public void SetNextStep()
        {
            Step += 1;
        }

        public bool GetPrimeNumber()
        {
            while (true)
            {
                if (IsPrime(CurrentNumber))
                {
                    BiggestPrimeNumber = CurrentNumber;
                    CurrentNumber++;

                    return true;
                }
                CurrentNumber++;
            }
        }

        public bool IsPrime(BigInteger number)
        {
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (BigInteger i = 3; i <= BigInteger.Divide(number, 2); i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public void Print(RichTextBox richTextBox)
        {
            richTextBox.Text = $"Cykl: {Step}\r\n" +
                               $"Największa liczba pierwsza: {BiggestPrimeNumber}\r\n" +
                               $"Czas wyznaczenia: {DateTime.Now}";
        }

        public void Reset()
        {
            Step = 1;
            CurrentNumber = 2;
            BiggestPrimeNumber = 2;
        }
    }
}
