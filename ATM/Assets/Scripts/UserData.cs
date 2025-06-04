using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [Header("텍스트 인스펙터 연결")] [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI balanceText;

    [Header("유저 정보")] [SerializeField] private string name;
    [SerializeField] private int money;
    [SerializeField] private int balance;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        nameText.text = name;
        moneyText.text = string.Format("{0:N0}", money);
        balanceText.text = string.Format("{0:N0}",balance);
    }
}