﻿using System;
using System.Text.RegularExpressions;
using System.Linq;


class Login
{
    public static void Main(String[] args)
    {

        string inputPassword;
        char[] encryptedFakeRealPassword =
        { (char)36, (char)61, (char)72, (char)72, (char)75, (char)13, (char)14, (char)15 };
        string encryptedRealPassword = new string(encryptedFakeRealPassword);
        string encryptedInputPassword;
        string usernameInput;
        string message = "";
        string filepath;
        int key = 65500;
        UserList users = new UserList();
        ConsoleKeyInfo auswahl = new ConsoleKeyInfo();
        bool passed = false;
        User currentUser = null;
        while (auswahl.Key != ConsoleKey.X)
        {
            Console.Clear();
            if (message != "")
            {
                Console.Write(message + "\n");
                message = "";
            }
            if (passed && currentUser != null) Console.WriteLine("Eingeloggt als: " + currentUser.Username);
            Console.Write("Was wollen sie tun? \n");
            if (!passed)
            {
                Console.Write("Login (L) \n");
                Console.Write("Registrieren (R) \n");
            }
            else
            {
                Console.Write("Ausloggen (L) \n");
                Console.Write("Nutzer kopieren (C) \n");
            }
            Console.Write("Nutzer auflisten (i) \n");
            Console.Write("Nutzerliste Exportieren (E) \n");
            Console.Write("Nutzerliste aus Datei Importieren (F) \n");
            Console.Write("User löschen (D) \n");
            Console.Write("Verlassen (X) \n");

            auswahl = Console.ReadKey();
            switch (auswahl.Key)
            {
                case ConsoleKey.F:
                    filepath = GetFilePath();
                    UserList tempUsers = UserList.importFromFile(filepath);
                    if (tempUsers != null)
                    {
                        HandleImportedUsers(tempUsers, users);
                      
                    } else
                    {
                        message = "Fehler beim importieren aus Datei bitte Dateipfad überprüfen!";
                    }
                    break;
                case ConsoleKey.E:
                    filepath = GetFilePath();
                    users.saveToFile(filepath);
                    message = "Saved To File!";
                    break;
                case ConsoleKey.C:
                    if (passed)
                    {
                        Console.Write("\n Nutzer zum kopieren auswählen \n");
                        Console.Write(users.GetUserList());
                        int index = GetNumber();
                        Console.Write("byte Array Length: " + users.FindByIndex(index).SerialisiereBinaer().Length + "\n");
                        User habib = User.DeserialisiereBinär(users.FindByIndex(index).SerialisiereBinaer());
                        users.AddUser(habib);
                        message = "Nutzer: " + habib.Username + " erfolgreich kopiert!";
                    }
                    break;
                case ConsoleKey.L:
                    if (!passed)
                    {
                        while (!passed)
                        {
                            Console.Write("\nEinloggen!");
                            Console.Write("\nUsername: ");
                            usernameInput = Console.ReadLine();
                            Console.Write("Passwort: ");
                            inputPassword = Console.ReadLine();
                            // Eingelesenes Passwort durch Aufruf der Funktion Encrypt verschlüsseln:
                            encryptedInputPassword = Encrypt(inputPassword, key);

                            // Überprüfen ob Login Daten übereinstimmen:
                            Console.Write(users.GetUserList());
                            User existingUser = users.FindUser(new User(usernameInput, encryptedInputPassword));

                            if (existingUser != null)
                            {
                                currentUser = existingUser;
                                passed = true;
                            }

                            if (passed)
                            {
                                inputPassword = null;
                            }
                            else
                            {
                                Console.Write("\nUsername oder Passwort Falsch! Wiederholen? Y/n \n");
                                auswahl = Console.ReadKey();
                                if (auswahl.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        currentUser = null;
                        passed = false;
                    }
                    break;
                case ConsoleKey.R:
                    if (!passed)
                    {
                        Console.Write("\nRegistrieren!");
                        Console.Write("\nUsername: ");
                        usernameInput = Console.ReadLine();
                        Console.Write("Passwort: ");
                        inputPassword = Console.ReadLine();
                        // Eingelesenes Passwort durch Aufruf der Funktion Encrypt verschlüsseln:
                        encryptedInputPassword = Encrypt(inputPassword, key);

                        User newUser = new User(usernameInput, encryptedInputPassword);
                        users.AddUser(newUser);
                        currentUser = newUser;
                        passed = true;
                    }
                    break;
                case ConsoleKey.D:
                    if (passed)
                    {
                        Console.Write("Passwort eingeben um Löschen des Users zu bestätigen: ");
                        inputPassword = Console.ReadLine();
                        // Eingelesenes Passwort durch Aufruf der Funktion Encrypt verschlüsseln:
                        encryptedInputPassword = Encrypt(inputPassword, key);
                        if (currentUser.Password == encryptedInputPassword)
                        {
                            users.DeleteUser(currentUser);
                            currentUser = null;
                            passed = false;
                        }
                    }
                    break;
                case ConsoleKey.I:
                    Console.Write("\nCurrent Users: \n");
                    Console.Write(users.GetUserList());
                    Console.ReadKey();
                    break;
            }
        }
    }
    // Die Methode Encrypt(char[], int length, int key) verschlüsselt die ersten length
    // Zeichen, die sich im char-Array s befinden, mit dem Schlüssel key nach der
    // einfachen Cäsar-Verschlüsselung (Modulo 65536).
    static string Encrypt(string s, int key)
    {
        char[] cs = s.ToCharArray();
        char[] encrypted = new char[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            encrypted[i] = (char)((cs[i ] + (char)key) % 65536);
        }
        string test = new string(encrypted);
        return test;
    }

    static int GetNumber()
    {
        Regex isANumber = new Regex(@"^\d+$");
        string userIndex = Console.ReadLine();
        if (!isANumber.IsMatch(userIndex)) {
            return GetNumber();
        } else
        {
            return Convert.ToInt32(userIndex);
        }
    }

    static string GetFilePath()
    {
        string filepath;
        Console.Write("\n Dateipfad angeben \n");
        filepath = Console.ReadLine();
        if (filepath != "")
        {
            return filepath;
        } else
        {
            return "test.txt";
        }
    }

    static UserList HandleImportedUsers(UserList newUsers, UserList oldUsers)
    {
        UserList users = new UserList();
        ConsoleKeyInfo select;
        User tmp = newUsers.FindByIndex(0);
        Console.WriteLine("Aktuelle Nutzer durch Import ersetzen? (Y/n)");
        select = Console.ReadKey();
        if (select.Key == ConsoleKey.Y)
        {
            users = newUsers;
            return users;
        }
        else
        {
            while (tmp != null)
            {
                oldUsers.AddUser(User.DeserialisiereBinär(tmp.SerialisiereBinaer()));
                tmp = tmp.next;
            }
            users = oldUsers;
            return users;
        }
    }
}
