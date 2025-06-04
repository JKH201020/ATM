public class UserData
{
    public string Name { get; private set; }
    public int Cash { get; private set; }
    public int Balance { get; private set; }

    public UserData(string name, int cash, int balance)
    {
        Name = name;
        Cash = cash;
        Balance = balance;
    }
}