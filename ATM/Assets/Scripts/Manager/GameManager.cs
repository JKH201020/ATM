using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData CurrentUserData { get; private set; } // 현재 로그인된 유저 데이터
    public List<UserData> userDataList = new List<UserData>(); // 유저들 데이터 리스트

    private string path; // 유저 데이터 저장/로드할 파일 경로
    
    void Awake() // 초기화
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // persistentDataPath: 읽기 쓰기 가능한 저장 경로
        // Combine(저장경로, 생성할 파일 이름);
        path = Path.Combine(Application.persistentDataPath, "UserData.json");

        LoadUserData(); // 저장된 데이터 불러옴
    }

    #region 기능구현

    public void DepositCash(int amount) // 입금 - 버튼에 연결
    {
        if (CurrentUserData.Cash >= amount)
        {
            CurrentUserData.Cash -= amount;
            CurrentUserData.Balance += amount;
        }
    }

    public void WithdrawalCash(int amount) // 출금 - 버튼에 연결
    {
        if (CurrentUserData.Balance >= amount)
        {
            CurrentUserData.Balance -= amount;
            CurrentUserData.Cash += amount;
        }
    }

    #endregion

    #region json으로 저장

    public void SaveUserData() // 유저 데이터 저장
    {
        // userData 인자를 Json형식의 문자열로 변환 -> 직렬화
        // Formatting.Indented 가독성을 높이기 위해 들여쓰기를 해줌 
        string jsonSave = JsonConvert.SerializeObject(userDataList, Formatting.Indented);

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
            userDataList = JsonConvert.DeserializeObject<List<UserData>>(json);
        }

        if (userDataList == null) // 저장된 데이터가 없다면
        {
            // 기본값으로 할당
            userDataList = new List<UserData>();
        }
    }

    public void CurrentUserInfo(UserData userData) // 현재(로그인한) 유저 정보
    {
        CurrentUserData = userData;
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