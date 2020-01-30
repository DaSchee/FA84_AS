 using System;
using System.IO;

public class UserList
{
    private User firstUser;
    private User lastUser;
    private int userCount;

    public UserList()
    {
        firstUser = lastUser = null;
        userCount = 0;
    }
    public int GetUserCount
    {
        get { return userCount; }
    }

    public User AddUser(User newUser)
    {
        newUser.next = null;
        if (firstUser == null)
        {
            firstUser = lastUser = newUser;
        }
        else
        {
            lastUser.next = newUser;
            lastUser = newUser;
        }
        userCount++;
        return newUser;
    }

    public string GetUserList()
    {
        User tmp = firstUser;
        string selectString = "";
        int index = 0;
        while (tmp != null)
        {
            selectString += index + ": " + tmp.Username + "\n";
            index += 1;
            tmp = tmp.next;
        }
        return selectString;
    }

    public User FindByIndex(int index)
    {
        User tmp = firstUser;
        int count = 0;
        while (tmp != null)
        {
            if (count == index)
            {
                return tmp;
            }
            count++;
            tmp = tmp.next;
        }
        return null;
    }

    public User FindUser(User user)
    {
        User tmp = firstUser;
        User returnValue = null;
        User previousEntry = null;
        while (tmp != null)
        {
            if ( user.Password == tmp.Password && user.Username == tmp.Username )
            { 
                returnValue = tmp;
                break;
            }
            else
            {
                previousEntry = tmp;
                tmp = tmp.next;
            }
        }
        return returnValue;
    }

    public void DeleteUser(User user)
    {
        User tmp = firstUser;
        User previousEntry = null;
        bool deleted = false;
        while (tmp != null)
        {

            if
                (
                user.Password == tmp.Password &&
                user.Username == tmp.Username
                )
            {
                deleted = true;
                if (tmp == firstUser)
                {
                    firstUser = tmp.next;
                    tmp = null;
                    userCount--;
                }
                else if (tmp == lastUser && previousEntry == null)
                {
                    tmp = null;
                    lastUser = firstUser = null;
                    userCount = 0;
                }
                else
                {
                    previousEntry.next = tmp.next;
                    tmp = null;
                    userCount--;
                }
                Console.WriteLine("Deletion Success");
                Console.Write("\n");
            }
            else
            {
                previousEntry = tmp;
                tmp = tmp.next;
            }
        }
        if (deleted == false)
        {
            Console.Write("List doesn't contain the Object you wish to Delete \n");
        }
        Console.Write(GetUserList());
    }

    public bool saveToFile(string path)
    {
        User tmp = firstUser;
        byte[] tmpToByte;
        FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        BinaryWriter bw = new BinaryWriter(fs);
        try
        {
            while (tmp != null)
            {
                tmpToByte = tmp.SerialisiereBinaer();
                bw.Write(tmpToByte);
                tmp = tmp.next;
            }
        }
        finally
        {
            bw.Close();
            fs.Close();
        }
        return true;
    }

    public UserList importFromFile(string path)
    {
        UserList newList = new UserList();
        User re;
        FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        do
        {
            re = User.DeserialisiereBinärMitReader(br);
            if (re != null)
            {
                newList.AddUser(re);
            }
        } while (re != null);
        //TODO lololol
        return newList;
    }
}