using System.Collections;
using System.Collections.Generic;
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

    // 유저 데이터 저장/로드할 실제 파일 경로
    private string path;

    void Awake() // 초기화
    {
        Instance = this;
        path = Path.Combine(Application.persistentDataPath, "UserData.json");

        LoadUserData(); // 저장된 데이터 불러옴
        Refresh(); // UI 업데이트
    }

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
        balanceText.text = string.Format("Balance   {0:N0}", userData.Balance);

        SaveUserData(); // UI가 업데이트 되면 업데이트된 데이터 값을 저장
    }

    #region json으로 저장

    public void SaveUserData() // 유저 데이터 저장
    {
        // userData 인자를 Json 문자열로 변환
        // Formatting.Indented 가독성을 높이기 위해 들여쓰기를 해줌
        string jsonSave = JsonConvert.SerializeObject(userData, Formatting.Indented);

        File.WriteAllText(path, jsonSave);

        Debug.Log($"저장");
        Debug.Log(jsonSave);
    }

    public void LoadUserData() // 유저 데이터 불러오기
    {
        string json = File.ReadAllText(path); // json 문자열을 읽어옴
        UserData jsonLoad = JsonConvert.DeserializeObject<UserData>(json);

        // 불러온 데이터를 GameManager의 userData에 할당
        userData.Name = jsonLoad.Name;
        userData.Balance = jsonLoad.Balance;
        userData.Cash = jsonLoad.Cash;

        if (userData == null) // 만약 여기까지 userData가 null이면 (예외적인 경우)
        {
            userData = new UserData("", 0, 0);
        }

        Debug.Log($"로드");
        Debug.Log($"이름={userData.Name}, 잔액={userData.Balance}, 현금={userData.Cash}");
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