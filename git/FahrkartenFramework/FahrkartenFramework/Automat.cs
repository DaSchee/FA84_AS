using System;
using System.Threading;

namespace FahrkartenFramework
{
    public class Fahrkartenautomat
    {
        public double zuZahlen;
        public double eingezahlterGesamtbetrag = 0.0;
        public double eingeworfeneMünze;
        public double rückgabebetrag;
        public bool abbruch = false;

        static void Main(string[] args)
        {
            Fahrkartenautomat automat = new Fahrkartenautomat();
            State currentState = new WaitingState(automat);

            while (true)
            {
                currentState = currentState.Handle();
            }
        }
        static public double Eingabe()
        {
            double zuZahlen;
            Console.Clear();
            Console.Write("Zu zahlender Betrag (EURO): ");
            double.TryParse(Console.ReadLine(), out zuZahlen);
            return zuZahlen;
        }

        static public void FahrscheinDrucken()
        {
            Console.WriteLine("\nFahrschein wird ausgegeben");
            //fahrscheinWirdGedruckt.Play();
            for (int i = 0; i < 5; i++)
            {
                Console.Write("/\b");
                Thread.Sleep(125);
                Console.Write("-\b");
                Thread.Sleep(125);
                Console.Write("\\\b");
                Thread.Sleep(125);
                Console.Write("|\b");
                Thread.Sleep(125);
            }
            Console.WriteLine("\n\n");
        }

        static public double Rückgabe(double rückgabebetrag)
        {
            if (rückgabebetrag > 0.0)
            {
                Console.WriteLine("Der Rückgabebetrag in Höhe von {0:F2} EURO\n", rückgabebetrag);
                Console.WriteLine("wird in folgenden Münzen ausgezahlt:\n");

                while (rückgabebetrag >= 2.0) // 2 EURO-Münzen
                {
                    Console.WriteLine("2 EURO");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 2.0;
                }
                while (rückgabebetrag >= 1.0) // 1 EURO-Münzen
                {
                    Console.WriteLine("1 EURO");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 1.0;
                }
                while (rückgabebetrag >= 0.5) // 50 CENT-Münzen
                {
                    Console.WriteLine("50 CENT");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 0.5;
                }
                while (rückgabebetrag >= 0.2) // 20 CENT-Münzen
                {
                    Console.WriteLine("20 CENT");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 0.2;
                }
                while (rückgabebetrag >= 0.1) // 10 CENT-Münzen
                {
                    Console.WriteLine("10 CENT");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 0.1;
                }
                while (rückgabebetrag >= 0.045)// 5 CENT-Münzen
                {
                    Console.WriteLine("5 CENT");
                    //münzeFällt.PlaySync();
                    if (rückgabebetrag < 0.05)
                    {
                        rückgabebetrag = 0;
                        break;
                    }
                    rückgabebetrag -= 0.05;
                }
            }
            return Math.Round(rückgabebetrag, 2);
        }
        public void Reset()
        {
            Console.Clear();
            zuZahlen = 0;
            eingezahlterGesamtbetrag = 0.0;
            eingeworfeneMünze = 0;
            rückgabebetrag = 0;
            abbruch = false;
        }

    }

}