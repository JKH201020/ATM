using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [Header("PopupBank 씬 오브젝트 할당")] [SerializeField]
    public GameObject deposit; // 입금 오브젝트

    [SerializeField] public GameObject withdrawal; // 출금 오브젝트
    [SerializeField] private GameObject atm; // ATM 오브젝트
    [SerializeField] private GameObject bgPanel; // 잔액부족 판넬
    [SerializeField] private TMP_InputField depositInputField; // 입금 직접 입력 오브젝트
    [SerializeField] private TMP_InputField withdrawalInputField; // 출금 직접 입력 오브젝트

    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText; // 유저 이름 출력 텍스트

    [SerializeField] private TextMeshProUGUI balanceText; // 통장 잔액 출력 텍스트
    [SerializeField] private TextMeshProUGUI cashText; // 현금 잔액 출력 텍스트

    void Start()
    {
        Refresh();
    }

    public void OnDepositButtonClick() // 입금으로 넘어가는 버튼
    {
        deposit.SetActive(true);
        atm.SetActive(false);
    }

    public void OnWithdrawalButtonClick() // 출금으로 넘어가는 버튼
    {
        withdrawal.SetActive(true);
        atm.SetActive(false);
    }

    public void OnBackButtonClick() // 뒤로 가기
    {
        // 입금 오브젝트 활성화 되어있을 경우
        if (deposit.activeSelf == true)
        {
            deposit.SetActive(false);
        }
        // 출금 오브젝트 활성화 되어있을 경우
        else if (withdrawal.activeSelf == true)
        {
            withdrawal.SetActive(false);
        }

        atm.SetActive(true);
        Refresh();
    }

    public void OnMoneyChangeButtonClick(int amount) // 입출금(10000, 30000, 50000) 버튼
    {
        if (deposit.activeSelf == true) // 입금 창일 때
        {
            if (GameManager.Instance.CurrentUserData.Cash >= amount)
            {
                GameManager.Instance.DepositCash(amount);
            }
            else
            {
                bgPanel.SetActive(true);
            }
        }
        else if (withdrawal.activeSelf == true) // 출금 창일 때
        {
            if (GameManager.Instance.CurrentUserData.Balance >= amount)
            {
                GameManager.Instance.WithdrawalCash(amount);
            }
            else
            {
                bgPanel.SetActive(true);
            }
        }

        Refresh();
    }

    public void InputDepositButtonClick() // 직접 입력 입금 버튼
    {
        string amountText = depositInputField.text;

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.DepositCash(amount);
            depositInputField.text = "";
            Refresh();
        }
    }

    public void InputWithdrawalButtonClick() // 직접 입력 출금 버튼
    {
        string amountText = withdrawalInputField.text;

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.WithdrawalCash(amount);
            withdrawalInputField.text = "";
            Refresh();
        }
    }

    public void PanelOk() // 금액부족 판넬 Ok버튼
    {
        bgPanel.SetActive(false);
    }

    public void CurrentUserInfo() // 현재 유저 정보
    {
        nameText.text = GameManager.Instance.CurrentUserData.Name;
        balanceText.text = string.Format("Balance   {0:N0}", GameManager.Instance.CurrentUserData.Balance);
        cashText.text = string.Format("{0:N0}", GameManager.Instance.CurrentUserData.Cash);
    }

    public void Refresh() // UI 업데이트
    {
        CurrentUserInfo();
        GameManager.Instance.SaveUserData();
    }
}