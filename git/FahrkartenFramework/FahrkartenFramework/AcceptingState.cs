using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FahrkartenFramework
{
    class AcceptingState : State
    {
        public AcceptingState(Fahrkartenautomat automat) : base(automat) { }

        public override State Handle()
        {
            return Pay();
        }

        private State Pay()
        {
            while (automat.eingezahlterGesamtbetrag < automat.zuZahlen)
            {
                double input = Münzeinwurf(automat.zuZahlen, automat.eingezahlterGesamtbetrag);
                if (input == 6.66)
                {
                    if (automat.eingezahlterGesamtbetrag > 0)
                    {
                        automat.abbruch = true;
                        return new PaybackState(automat);
                    } else
                    {
                        Console.WriteLine("Zahlungsvorgang abgebrochen!");
                        Thread.Sleep(1000);
                        return new WaitingState(automat);
                    }
                }
                automat.eingeworfeneMünze = input;
                automat.eingezahlterGesamtbetrag += automat.eingeworfeneMünze;
            }
            return new PrintingState(automat);
        }

        static public double Münzeinwurf(double zuZahlen, double eingezahlterGesamtbetrag)
        {
            bool correctInput = false;
            double eingeworfeneMünze = 0;
            Console.WriteLine("Noch zu zahlen: {0:F2} EURO", zuZahlen - eingezahlterGesamtbetrag);
            while (!correctInput)
            {
                Console.WriteLine("Eingabe (mind. 5Ct, höchstens 20 Euro. (X für Abbruch): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "x")
                {
                    eingeworfeneMünze = 6.66;
                    break;
                }
                double.TryParse(input, out eingeworfeneMünze);
                if (eingeworfeneMünze >= 0.05 && eingeworfeneMünze <= 20)
                {
                    correctInput = true;
                    break;
                }
                Console.WriteLine("Falsche Münze/Schein!");
            }
            return eingeworfeneMünze;
        }
    }
}
