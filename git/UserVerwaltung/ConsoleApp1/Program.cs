using System;
using System.Linq;


class Login
{
    public static void Main(String[] args)
    {
        
        char[] inputPassword = new char[50];
        char[] encryptedRealPassword =
        { (char)36, (char)61, (char)72, (char)72, (char)75, (char)13, (char)14, (char)15 }; 
        char[] encryptedInputPassword;
        char[] username =
        { '1', '3', '3', '7'};
        char[] usernameInput = new char[50];
        int key = 65500;
        User dade = new User(username, encryptedRealPassword);
        UserList users = new UserList();
        users.AddUser(dade);
        // Passwort einlesen:
        bool passed = false;
       
        while (!passed)
        {
            int inputLength = 0;
            int usernameInputLength = 0;
            ConsoleKeyInfo cki;
            Console.Write("Username: ");
            while ((cki = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                Console.Write(cki.KeyChar);
                usernameInput[usernameInputLength++] = cki.KeyChar;
            }
            Console.Write("\nPasswort: ");
            while ((cki = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (cki.Key == ConsoleKey.Backspace && inputLength > 0)
                {
                    Console.Write("\b \b");
                    inputPassword = inputPassword.Reverse().Skip(1).Reverse().ToArray();
                    inputLength--;
                }
                else if (cki.Key != ConsoleKey.Backspace)
                {
                    Console.Write('*');
                    inputPassword[inputLength++] = cki.KeyChar;
                }

            }
            // Eingelesenes Passwort durch Aufruf der Funktion Encrypt verschlüsseln:
            encryptedInputPassword = Encrypt(inputPassword, inputLength, key);

            // Ausgabe des Verschlüsselten Passworts
            // Console.WriteLine(Encrypt(encryptedRealPassword, encryptedRealPassword.Length, -65500));

            // Verschlüsselte Passwörter vergleichen:
            users.GetUserList();
            User existingUser = users.FindUser(new User(usernameInput, encryptedInputPassword));

            if (existingUser != null)
            {
                Console.WriteLine("Moin");
                passed = true;
            }
            //if (dade.Password.Length == inputLength && dade.Username.Length == usernameInputLength)
            //{
            //    passed = true;
            //    for (int i = 0; i < usernameInputLength && passed; i++)
            //    {
            //        if (usernameInput[i] != dade.Username[i]) {
            //            passed = false;
            //        }
            //    }
            //    for (int i = 0; i < inputLength && passed; i++)
            //    {
            //        if (encryptedInputPassword[i] != dade.Password[i])
            //        {
            //            passed = false;
            //        }
            //    }
            //}
            if (passed)
            {
                Console.WriteLine("\nSie sind eingeloggt.");
                // Die folgende Ausgabe dient uns nur zum Testen der Eingabe:
                Console.WriteLine("Willkomen: " + new string(existingUser.Username));
                // Beim realen Anmeldevorgang wird die Eingabe sofort gelöscht:
                inputPassword = null;
            }
            else
            {
                Console.Write("\nUsername oder Passwort Falsch, bitte wiederholen!\n");
            }
        }
        Console.ReadKey(true);
    }
    // Die Methode Encrypt(char[], int length, int key) verschlüsselt die ersten length
    // Zeichen, die sich im char-Array s befinden, mit dem Schlüssel key nach der
    // einfachen Cäsar-Verschlüsselung (Modulo 65536).
    static char[] Encrypt(char[] s, int length, int key)
    {
        char[] encrypted = new char[length];
        for (int i = 0; i < length; i++)
        {
            encrypted[i] = (char)((s[i ] + (char)key) % 65536);
        }
        return encrypted;
    }
}
