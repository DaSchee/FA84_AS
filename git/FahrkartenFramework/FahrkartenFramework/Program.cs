using System;
using System.Threading;

namespace Fahrkartenautomat
{
    enum state {WAITING, ACCEPTING, PRINTING, PAYBACK};
    public class Fahrkartenautomat
    {
        double zuZahlen;
        double eingezahlterGesamtbetrag = 0.0;
        double eingeworfeneMünze;
        double rückgabebetrag;

        static void Main(string[] args)
        {
            state activeState = state.WAITING;
            Fahrkartenautomat automat = new Fahrkartenautomat();
            // Die Sound-Dateien werden in dem Verzeichnis erwartet, 
            // in dem sich diese Quelltextdatei befindet.
            //System.Media.SoundPlayer münzeFällt = 
            //    new System.Media.SoundPlayer(@"..\..\coin-drop-1.wav");
            //System.Media.SoundPlayer fahrscheinWirdGedruckt =
            //    new System.Media.SoundPlayer(@"..\..\printer-dotmatrix-01.wav");

            while (true)
            {
                switch (activeState)
                {
                    case state.WAITING:
                        automat.Reset();
                        activeState = automat.SelectTicket();
                        break;
                    case state.ACCEPTING:
                        activeState = automat.Pay();
                        break;
                    case state.PRINTING:
                        activeState = FahrscheinDrucken();
                        break;
                    case state.PAYBACK:
                        activeState = automat.Payback();
                        break;
                }
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

        private state SelectTicket()
        {
            while (zuZahlen == 0)
            {                
                zuZahlen = Eingabe();
            }
            return state.ACCEPTING;
        }

        static public double Münzeinwurf(double zuZahlen, double eingezahlterGesamtbetrag)
        {
            bool correctInput = false;
            double eingeworfeneMünze = 0;
            Console.WriteLine("Noch zu zahlen: {0:F2} EURO", zuZahlen - eingezahlterGesamtbetrag);
            while (!correctInput)
            {
                Console.WriteLine("Eingabe (mind. 5Ct, höchstens 20 Euro): ");
                double.TryParse(Console.ReadLine(), out eingeworfeneMünze);
                if (eingeworfeneMünze >= 0.05 && eingeworfeneMünze <= 20)
                {
                    correctInput = true;
                    break;
                }
                Console.WriteLine("Falsche Münze/Schein!");
            }
            return eingeworfeneMünze;
        }

        private state Pay()
        {
            while (eingezahlterGesamtbetrag < zuZahlen)
            {
                eingeworfeneMünze = Münzeinwurf(zuZahlen, eingezahlterGesamtbetrag);
                eingezahlterGesamtbetrag += eingeworfeneMünze;
            }
            return state.PRINTING;
        }

        static private state FahrscheinDrucken()
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
            return state.PAYBACK;
        }

        private state Payback()
        {
            rückgabebetrag = eingezahlterGesamtbetrag - zuZahlen;
            rückgabebetrag = Rückgabe(rückgabebetrag);

            Console.WriteLine("\nVergessen Sie nicht, den Fahrschein\n" +
                              "vor Fahrtantritt stempeln zu lassen!\n" +
                              "Wir wünschen Ihnen eine gute Fahrt.");
            Console.ReadKey();

            return state.WAITING;
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
        private void Reset()
        {
            Console.Clear();
            zuZahlen = 0;
            eingezahlterGesamtbetrag = 0.0;
            eingeworfeneMünze = 0;
            rückgabebetrag = 0;
        }

    }

}