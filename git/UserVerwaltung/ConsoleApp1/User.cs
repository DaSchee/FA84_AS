using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class User
{
    private string username;
    private string password;
    public User next;

    public User()
    {
    }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public byte[] SerialisiereBinaer()
    {
        using (MemoryStream m = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(m))
            {
                writer.Write(Username);
                writer.Write(Password);
            }
            return m.ToArray();
        }
    }

    public static User DeserialisiereBinär(byte[] byteArray)
    {
        User result = new User();
        using (MemoryStream m = new MemoryStream(byteArray))
        {
            using (BinaryReader reader = new BinaryReader(m))
            {
                result = DeserialisiereBinärMitReader(reader);
                reader.Close();
            }
        }
        return result;
    }

    public static User DeserialisiereBinärMitReader(BinaryReader reader)
    {
        User result = new User();
        result.Username = reader.ReadString();
        result.Password = reader.ReadString();
        return result;
    }
}
