using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [SerializeField] private GameObject deposit;
    [SerializeField] private GameObject withdrawal;
   
    public void OnDepositButtonClick()
    {
        deposit.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnWithdrawalButtonClick()
    {
        withdrawal.SetActive(true);
        gameObject.SetActive(false);
    }
}