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
        
        if (userData == null)
        {
            // 유저 정보 초기화
            userData = new UserData("", 0, 0);
        }

        Refresh();
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
    }
}