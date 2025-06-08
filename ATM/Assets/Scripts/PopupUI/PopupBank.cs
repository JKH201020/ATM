using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [Header("PopupBank 씬 오브젝트 할당")] [SerializeField]
    public GameObject deposit; // 입금 오브젝트

    [SerializeField] public GameObject withdrawal; // 출금 오브젝트
    [SerializeField] private GameObject atm; // ATM 오브젝트
    [SerializeField] private TMP_InputField depositInputField; // 입금 직접 입력 오브젝트
    [SerializeField] private TMP_InputField withdrawalInputField; // 출금 직접 입력 오브젝트
    
    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText; // 유저 이름 출력 텍스트

    [SerializeField] private TextMeshProUGUI balanceText; // 통장 잔액 출력 텍스트
    [SerializeField] private TextMeshProUGUI cashText; // 현금 잔액 출력 텍스트
    
    public GameObject panel; // 잔액부족 판넬
    
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
    }

    public void InputDepositButtonClick() // 직접 입력 입금 버튼
    {
        string amountText = depositInputField.text;

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.DepositCash(amount);
            depositInputField.text = "";
        }
    }

    public void InputWithdrawalButtonClick() // 직접 입력 출금 버튼
    {
        string amountText = withdrawalInputField.text;

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.withdrawalCash(amount);
            withdrawalInputField.text = "";
        }
    }

    public void PanelOk() // 금액부족 판넬 Ok버튼
    {
        // GameManager.Instance.panel.SetActive(false);
    }
}