using System;
using System.Threading;

namespace Fahrkartenautomat
{
    class Fahrkartenautomat
    {
        static void Main(string[] args)
        {
            int i;
            double zuZahlen;
            double eingezahlterGesamtbetrag = 0.0;
            double eingeworfeneMünze;
            double rückgabebetrag;
            // Die Sound-Dateien werden in dem Verzeichnis erwartet, 
            // in dem sich diese Quelltextdatei befindet.
            //System.Media.SoundPlayer münzeFällt = 
            //    new System.Media.SoundPlayer(@"..\..\coin-drop-1.wav");
            //System.Media.SoundPlayer fahrscheinWirdGedruckt =
            //    new System.Media.SoundPlayer(@"..\..\printer-dotmatrix-01.wav");

            // Eingabe
            // -------
            Console.Write("Zu zahlender Betrag (EURO): ");
            double.TryParse(Console.ReadLine(), out zuZahlen);
            while (eingezahlterGesamtbetrag < zuZahlen)
            {
                Console.WriteLine("Noch zu zahlen: {0:F2} EURO", zuZahlen - eingezahlterGesamtbetrag);
                Console.WriteLine("Eingabe (mind. 5Ct, höchstens 2 Euro): ");
                double.TryParse(Console.ReadLine(), out eingeworfeneMünze);
                eingezahlterGesamtbetrag += eingeworfeneMünze;
            }

            // Fahrscheinausgabe
            // -----------------
            Console.WriteLine("\nFahrschein wird ausgegeben");
            //fahrscheinWirdGedruckt.Play();
            for (i = 0; i < 5; i++)
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

            // Rückgeldberechnung und -Ausgabe
            // -------------------------------
            rückgabebetrag = eingezahlterGesamtbetrag - zuZahlen;
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
                while (rückgabebetrag >= 0.05)// 5 CENT-Münzen
                {
                    Console.WriteLine("5 CENT");
                    //münzeFällt.PlaySync();
                    rückgabebetrag -= 0.05;
                }
            }

            Console.WriteLine("\nVergessen Sie nicht, den Fahrschein\n" +
                              "vor Fahrtantritt stempeln zu lassen!\n" +
                              "Wir wünschen Ihnen eine gute Fahrt.");
            Console.ReadKey();
        }
    }


}