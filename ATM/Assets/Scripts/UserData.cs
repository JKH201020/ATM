[System.Serializable]
public class UserData
{
    public string Name;
    public int Balance;
    public int Cash;
    
    public UserData(string name, int balance, int cash)
    {
        Name = name;
        Cash = cash;
        Balance = balance;
    }
}