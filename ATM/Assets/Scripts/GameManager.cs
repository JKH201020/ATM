using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UserData userData;

    [Header("유저 정보")] [SerializeField] private string name;
    [SerializeField] private int balance;
    [SerializeField] private int cash;
    
    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI cashText;
    
    void Awake()
    {
        Instance = this;
        userData = new UserData(name, cash, balance);
        Refresh();
    }

    public void Refresh()
    {
        nameText.text = name;
        cashText.text = string.Format("{0:N0}", cash);
        balanceText.text = string.Format("Balance   {0:N0}", balance);
    }
}