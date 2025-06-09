using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData CurrentUserData { get; private set; } // 현재 로그인된 유저 데이터
    public List<UserData> userDataList = new List<UserData>(); // 유저들 데이터 리스트

    private string _path; // 유저 데이터 저장/로드할 파일 경로

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
        
        // Combine(저장경로, 생성할 파일 이름);
        // Application.persistentDataPath - 모바일에서 경로 지정할 때 사용하는 것 같음 - 읽기 쓰기 가능한 저장 경로
        // Directory.GetCurrentDirectory() - 프로젝트 최상단에 저장하는 경로
        _path = Path.Combine(Directory.GetCurrentDirectory(), "UserData.json");
        Debug.Log(_path); // 제이슨 저장 경로

        LoadUserData(); // 저장된 데이터 불러옴
    }

    #region 기능구현

    public void DepositCash(int amount) // 입금(정산) - 버튼에 연결
    {
        if (CurrentUserData.cash >= amount) // 현재 유저 현금 >= 해당 입금 금액
        {
            CurrentUserData.cash -= amount;
            CurrentUserData.balance += amount;
        }
    }

    public void WithdrawalCash(int amount) // 출금(정산) - 버튼에 연결
    {
        if (CurrentUserData.balance >= amount) // 현재 유저 통장 >= 해당 출금 금액
        {
            CurrentUserData.balance -= amount;
            CurrentUserData.cash += amount;
        }
    }

    public void RemittanceCash(UserData targetUser, int amount) // 송금(정산) - 버튼에 연결
    {
        if (targetUser == null) return; // 저장된 유저 데이터가 없다면 종료

        if (CurrentUserData.balance >= amount) // 현재 유저 통장 금액 >= 송금 금액
        {
            CurrentUserData.balance -= amount;
            targetUser.balance += amount; // 송금 대상 통장에 금액 송금
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
        File.WriteAllText(_path, jsonSave);
    }

    private void LoadUserData() // 유저 데이터 불러오기
    {
        if (File.Exists(_path)) // 경로에 파일이 존재하면
        {
            // 경로에 있는 파일의 모든 텍스트 내용을 읽어옴 -> 문자열 변수에 저장
            string json = File.ReadAllText(_path);
            // json 형식의 문자열을 C# 객체(UserData 타입)로 변환 -> 역직렬화
            userDataList = JsonConvert.DeserializeObject<List<UserData>>(json);
        }

        if (userDataList == null) // 저장된 데이터가 없다면
        {
            userDataList = new List<UserData>(); // 기본값으로 할당
        }
    }

    public void CurrentUserInfo(UserData userData) // 현재(로그인한) 유저 정보
    {
        CurrentUserData = userData; // 로그인한 유저 정보를 현재 유저정보에 저장
    }

    #endregion
}