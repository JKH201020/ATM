[System.Serializable]
public class UserData
{
    public string Name;
    public int Balance;
    public int Cash;
    
    public UserData(string name, int balance, int cash) // 생성자
    {
        Name = name;
        Balance = balance;
        Cash = cash;
    }

    public UserData()
    {
    }
}
