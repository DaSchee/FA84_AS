using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FahrkartenFramework
{
    class PaybackState : State
    {
        public PaybackState(Fahrkartenautomat automat) : base(automat) { }

        public override State Handle()
        {
            Payback();
            return new WaitingState(automat);
        }

        private void Payback()
        {
            if (automat.abbruch)
            {
                automat.rückgabebetrag = automat.eingezahlterGesamtbetrag;
            }
            else
            {
                automat.rückgabebetrag = automat.eingezahlterGesamtbetrag - automat.zuZahlen;
            }
            automat.rückgabebetrag = Fahrkartenautomat.Rückgabe(automat.rückgabebetrag);
            if (automat.abbruch)
            {
                Console.WriteLine("\nZahlungsvorgang abgebrochen!");
            }
            else
            {
                Console.WriteLine("\nVergessen Sie nicht, den Fahrschein\n" +
                                  "vor Fahrtantritt stempeln zu lassen!\n" +
                                  "Wir wünschen Ihnen eine gute Fahrt.");
            }
            Console.ReadKey();
        }
    }
}
