using System;

namespace PkwListe
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variante mit Array:
            // ==================
            Pkw[] pkwArray = new Pkw[10];
            pkwArray[0] = new Pkw("Fiat", "Panda", "rot");
            pkwArray[1] = new Pkw("Opel", "Zafira", "grau");
            int roteAutos = 0;
            for (int i = 0; i < pkwArray.Length; i++)
            {
                Console.WriteLine("Auto " + i + ": ");
                if (pkwArray[i] != null)
                {
                    Console.WriteLine(pkwArray[i].GetFarbe());
                    if (pkwArray[i].GetFarbe().Equals("rot"))
                    {
                        roteAutos++;
                    }
                }
                else
                {
                    Console.WriteLine("nicht vorhanden");
                }
            }
            Console.WriteLine(roteAutos + " rote Autos gefunden");

            // Variante mit verketteter Liste:
            // ==============================
            Console.WriteLine("===============================================");
            PkwListe pkwListe = new PkwListe();
            pkwListe.FügePkwEin(new Pkw("Fiat", "Panda", "rot"));
            pkwListe.FügePkwEin(new Pkw("Opel", "Zafira", "grau"));
            pkwListe.FügePkwEin(new Pkw("Toyota", "Yaris", "grün"));
            pkwListe.FügePkwEin(new Pkw("Mercedes", "A-Klasse", "gelb"));
            pkwListe.FügePkwEin(new Pkw("Kia", "Seed", "silber"));
            Console.Write(pkwListe.Select());
            Console.WriteLine("Es gibt " + pkwListe.AnzahlPkws + " Pkws");
            pkwListe.Delete(new Pkw("Fiat", "Panda", "rot"));

            Console.ReadKey();
        }
    }

    class PkwListe
    {
        private Pkw ersterPkw;
        private Pkw letzterPkw;
        private int anzahlPkws;

        public PkwListe()
        {
            ersterPkw = letzterPkw = null;
            anzahlPkws = 0;
        }
        public int AnzahlPkws
        {
            get { return anzahlPkws; }
        }

        public Pkw FügePkwEin(Pkw neuerPkw)
        {
            neuerPkw.SetNext(null);
            if (ersterPkw == null)
            {
                ersterPkw = letzterPkw = neuerPkw;
            }
            else
            {
                letzterPkw.SetNext(neuerPkw);
                letzterPkw = neuerPkw;
            }
            anzahlPkws++;
            return neuerPkw;
        }

        public string Select()
        {
            Pkw tmp = ersterPkw;
            string selectString = "";
            while(tmp != null)
            {
                selectString += tmp.GetMarke() + " " + tmp.GetModell() + " " + tmp.GetFarbe() + "\n";
                tmp = tmp.GetNext();
            }
            return selectString;
        }

        public void Delete(Pkw pkw)
        {
            Pkw tmp = ersterPkw;
            Pkw previousEntry = null;
            bool deleted = false;
            while (tmp != null)
            {

                if 
                    (
                    pkw.GetFarbe() == tmp.GetFarbe() &&
                    pkw.GetKmStand() == tmp.GetKmStand() &&
                    pkw.GetMarke() == tmp.GetMarke() &&
                    pkw.GetModell() == tmp.GetModell() &&
                    pkw.GetType() == tmp.GetType()
                    )
                {
                    if (tmp == ersterPkw)
                    {
                        ersterPkw = tmp.GetNext();
                        tmp = null;
                        anzahlPkws--;
                    }
                    else if (tmp == letzterPkw && previousEntry == null)
                    {
                        tmp = null;
                        letzterPkw = ersterPkw = null;
                        anzahlPkws = 0;
                    }
                    else
                    {
                        previousEntry.SetNext(tmp.GetNext());
                        tmp = null;
                        anzahlPkws--;
                    }
                    Console.WriteLine("Deletion Success");
                    Console.Write("\n");
                } else
                {
                    previousEntry = tmp;
                    tmp = tmp.GetNext();
                }
            }
            if (deleted == false)
            {
                Console.Write("List doesn't contain the Object you wish to Delete \n");
            }
            Console.Write(Select());
        }
    }
    class Pkw
    {
        private String marke;
        private String modell;
        private String farbe;
        private double leistung;
        private double km;
        private double geschwindigkeit;

        // Für die Verkettung der Pkw-Objekte als Liste:
        private Pkw nächsterPkw;

        public Pkw(String marke, String modell, String farbe)
        {
            this.marke = marke;
            this.modell = modell;
            this.farbe = farbe;
            this.geschwindigkeit = 0.0;
            this.nächsterPkw = null;
        }
        public void SetLeistung(double kW)
        {
            leistung = kW;
        }
        public double FahreStrecke(double strecke)
        {
            km += strecke;
            return km;
        }
        public String GetMarke()
        {
            return marke;
        }
        public String GetModell()
        {
            return modell;
        }
        public String GetFarbe()
        {
            return farbe;
        }
        public double GetKmStand()
        {
            return km;
        }

        internal Pkw GetNext()
        {
            return nächsterPkw;
        }

        internal void SetNext(Pkw neuerPkw)
        {
            nächsterPkw = neuerPkw;
        }
    }
}