using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData userData;

    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText; // 유저 이름 출력 텍스트

    [SerializeField] private TextMeshProUGUI balanceText; // 통장 잔액 출력 텍스트
    [SerializeField] private TextMeshProUGUI cashText; // 현금 잔액 출력 텍스트

    public GameObject panel; // 잔액부족 판넬

    private string path; // 유저 데이터 저장/로드할 파일 경로

    void Awake() // 초기화
    {
        Instance = this;
        
        // persistentDataPath: 읽기 쓰기 가능한 저장 경로
        // Combine(저장경로, 생성할 파일 이름);
        path = Path.Combine(Application.persistentDataPath, "UserData.json");

        LoadUserData(); // 저장된 데이터 불러옴
        Refresh(); // UI 업데이트
    }

    #region 기능구현

    public void DepositCash(int amount) // 입금 - 버튼에 연결
    {
        if (userData.Cash - amount >= 0)
        {
            userData.Cash -= amount;
            userData.Balance += amount;
        }
        else
        {
            panel.SetActive(true);
        }

        Refresh();
    }

    public void withdrawalCash(int amount) // 출금 - 버튼에 연결
    {
        if (userData.Balance - amount >= 0)
        {
            userData.Balance -= amount;
            userData.Cash += amount;
        }
        else
        {
            panel.SetActive(true);
        }

        Refresh();
    }

    public void Refresh() // UI 업데이트
    {
        nameText.text = userData.Name;
        cashText.text = string.Format("{0:N0}", userData.Cash);
        balanceText.text = string.Format("Balance      {0:N0}", userData.Balance);

        SaveUserData();
    }

    #endregion

    #region json으로 저장

    public void SaveUserData() // 유저 데이터 저장
    {
        // userData 인자를 Json형식의 문자열로 변환 -> 직렬화
        // Formatting.Indented 가독성을 높이기 위해 들여쓰기를 해줌 
        string jsonSave = JsonConvert.SerializeObject(userData, Formatting.Indented);

        // path 경로에 파일이 없으면 jsonSave내용으로 생성하고, 파일이 있으면 내용을 덮어쓴다.
        File.WriteAllText(path, jsonSave);
    }

    public void LoadUserData() // 유저 데이터 불러오기
    {
        if (File.Exists(path)) // 경로에 파일이 존재하면
        {
            // 경로에 있는 파일의 모든 텍스트 내용을 읽어옴 -> 문자열 변수에 저장
            string json = File.ReadAllText(path);
            // json 형식의 문자열을 C# 객체(UserData 타입)로 변환 -> 역직렬화
            UserData jsonLoad = JsonConvert.DeserializeObject<UserData>(json);

            if (jsonLoad != null) // 제이슨에 저장된 데이터 값이 있다면
            {
                // 불러온 데이터를 userData에 할당
                userData.Name = jsonLoad.Name;
                userData.Balance = jsonLoad.Balance;
                userData.Cash = jsonLoad.Cash;
            }
        }
        
        if (userData == null) // 저장된 데이터가 없다면
        {
            // 기본값으로 할당
            userData = new UserData("", 0, 0);  
        }
    }

    #endregion

    #region 데이터 세이브, 로드

// public void SaveUserData() // 유저 데이터 저장
// {
//     // 첫 번째 인자는 "Key"라는 저장 공간이고 "Key"의 이름에 따라 저장된 값을 불러온다.
//     // 두 번째 인자는 저장할 데이터를 입력한다.
//     PlayerPrefs.SetString("Name", userData.Name);
//     PlayerPrefs.SetInt("Balance", userData.Balance);
//     PlayerPrefs.SetInt("Cash", userData.Cash);
//
//     PlayerPrefs.Save(); // 위의 내용들을 저장
// }
//
// public void LoadUserData() // 유저 데이터 불러오기
// {
//     // "Key"의 이름에 맞는 저장된 값을 불러온다.
//     // 저장된 값이 없을 경우 두 번째 인자를 기본값으로 불러온다.
//     userData.Name = PlayerPrefs.GetString("Name", "");
//     userData.Balance = PlayerPrefs.GetInt("Balance", 0);
//     userData.Cash = PlayerPrefs.GetInt("Cash", 0);
//
//     Refresh();
// }

    #endregion
}