using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [Header("오브젝트 할당")] [SerializeField] public GameObject deposit;
    [SerializeField] public GameObject withdrawal;
    [SerializeField] private GameObject atm;
    [SerializeField] private TMP_InputField depositInputField;
    [SerializeField] private TMP_InputField withdrawalInputField;
    
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
        if (deposit.activeSelf == true)
        {
            deposit.SetActive(false);
        }
        else if (withdrawal.activeSelf == true)
        {
            withdrawal.SetActive(false);
        }

        atm.SetActive(true);
    }

    public void InputDepositButtonClick()// 직접 입력 입금 버튼
    {
        string amountText = depositInputField.text;

        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.DepositCash(amount);
            depositInputField.text = "";
        }
    }

    public void InputWithdrawalButtonClick() // 직접 입력 출금 버튼
    {
        string amountText = withdrawalInputField.text;

        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.withdrawalCash(amount);
            withdrawalInputField.text = "";
        }
    }

    public void PanelOk() // 금액부족 판넬 Ok버튼
    {
        GameManager.Instance.panel.SetActive(false);
    }
}