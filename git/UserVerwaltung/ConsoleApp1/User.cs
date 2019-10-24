public class User
{
    private char[] username;
    private char[] password;
    public User next;

    public User()
    {
    }

    public User(char[] username, char[] password)
    {
        this.username = username;
        this.password = password;
    }

    public User(char[] username, char[] password, int nLength)
    {
        this.username = username;
        this.password = password;
    }

    public char[] Username
    {
        get { return username; }
        set { username = value; }
    }
    //TODO
//    public void SetUsername(char[] name, int length)
//    {
//        char[] temp = new char[length];
//        for ( int i = 0; i < length; i++)
//        {
//            temp[]
//        }
//    }

    public string GetUsernameAsString()
    {
        return new string(username);
    }

    public string GetPasswordAsString()
    {
        return new string(password);
    }

    public char[] Password
    {
        get { return password; }
        set { password = value; }
    }
}
