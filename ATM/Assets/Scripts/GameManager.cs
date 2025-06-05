using System.Collections;
using System.Collections.Generic;
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

    void Awake() // 초기화
    {
        Instance = this;

        // LoadUserData()에서 저장된 값이 없으면 초기값을 할당해주기 때문에
        // 이 조건문은 필요가 없어짐
        // if (userData == null)
        // {
        //     // 유저 정보 초기화
        //     userData = new UserData("", 0, 0);
        // }

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

    #region 데이터 세이브, 로드

    public void SaveUserData() // 유저 데이터 저장
    {
        // 첫 번째 인자는 "Key"라는 저장 공간이고 "Key"의 이름에 따라 저장된 값을 불러온다.
        // 두 번째 인자는 저장할 데이터를 입력한다.
        PlayerPrefs.SetString("Name", userData.Name);
        PlayerPrefs.SetInt("Balance", userData.Balance);
        PlayerPrefs.SetInt("Cash", userData.Cash);

        PlayerPrefs.Save(); // 위의 내용들을 저장
    }

    public void LoadUserData() // 유저 데이터 불러오기
    {
        // "Key"의 이름에 맞는 저장된 값을 불러온다.
        // 저장된 값이 없을 경우 두 번째 인자를 기본값으로 불러온다.
        userData.Name = PlayerPrefs.GetString("Name", "");
        userData.Balance = PlayerPrefs.GetInt("Balance", 0);
        userData.Cash = PlayerPrefs.GetInt("Cash", 0);

        Refresh();
    }

    #endregion
}