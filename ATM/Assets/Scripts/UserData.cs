[System.Serializable]
public class UserData
{
    public string id;
    public string name;
    public string ps;
    public int balance;
    public int cash;

    public UserData()
    {
    } // 역직렬화를 위한 기본 생성자

    public UserData(string id, string name, string ps) // 회원가입 시 생성자
    {
        this.id = id;
        this.name = name;
        this.ps = ps;
        balance = 150000; // 통장 초기 금액
        cash = 100000; // 현금 초기 금액
    }

    public UserData(string id, string name, string ps, int balance, int cash) // 기존 유저 생성자
    {
        this.id = id;
        this.name = name;
        this.ps = ps;
        this.balance = balance;
        this.cash = cash;
    }
}