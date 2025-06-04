using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UserData userData;

    [Header("유저 정보")] [SerializeField] private string name;
    [SerializeField] private int money;
    [SerializeField] private int balance;

    void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    void UpdateUI()
    {
        userData.nameText.text = name;
        userData.moneyText.text = string.Format("{0:N0}", "Balance " + money);
        userData.balanceText.text = string.Format("{0:N0}", balance);
    }
}