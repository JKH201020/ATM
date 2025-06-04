[System.Serializable]
public class UserData
{
    public string Name;
    public long Balance;
    public long Cash;
    
    public UserData(string name, long cash, long balance)
    {
        Name = name;
        Cash = cash;
        Balance = balance;
    }
}