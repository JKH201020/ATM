[System.Serializable]
public class UserData
{
    public string ID;
    public string Name;
    public string PS;
    public int Balance;
    public int Cash;

    public UserData()
    {
    } // 역직렬화를 위한 기본 생성자

    public UserData(string id, string name, string ps) // 회원가입 시 생성자
    {
        ID = id;
        Name = name;
        PS = ps;
        Balance = 150000; // 통장 초기 금액
        Cash = 100000; // 현금 초기 금액
    }

    public UserData(string id, string name, string ps, int balance, int cash) // 기존 유저 생성자
    {
        ID = id;
        Name = name;
        PS = ps;
        Balance = balance;
        Cash = cash;
    }
}