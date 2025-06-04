using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData userData;

    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI cashText;

    public GameObject panel;

    void Awake() // 초기화
    {
        Instance = this;
        if (userData == null)
        {
            userData = new UserData("", 0, 0);
        }

        Refresh();
    }

    public void DepositCash(int amount) // 입금
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

    public void withdrawalCash(int amount) // 출금
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