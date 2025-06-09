using System.Linq;
using TMPro;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [Header("PopupBank 씬 오브젝트 할당")] [SerializeField]
    public GameObject deposit; // 입금 오브젝트

    [SerializeField] public GameObject withdrawal; // 출금 오브젝트
    [SerializeField] public GameObject remittance; // 송금 오브젝트
    [SerializeField] private GameObject atm; // ATM 오브젝트
    [SerializeField] private GameObject bgPanel; // 잔액부족 판넬
    [SerializeField] private TMP_InputField depositInputField; // 입금 직접 입력 오브젝트
    [SerializeField] private TMP_InputField withdrawalInputField; // 출금 직접 입력 오브젝트
    [SerializeField] private TMP_InputField remittanceTargetInputField; // 송금 대상 오브젝트
    [SerializeField] private TMP_InputField remittanceCashInputField; // 송금 금액 오브젝트
    [SerializeField] private TextMeshProUGUI errorText; // 에러 문구 오브젝트

    [Header("텍스트UI 인스펙터에 연결")] [SerializeField]
    private TextMeshProUGUI nameText; // 유저 이름 출력 텍스트

    [SerializeField] private TextMeshProUGUI balanceText; // 통장 잔액 출력 텍스트
    [SerializeField] private TextMeshProUGUI cashText; // 현금 잔액 출력 텍스트

    void Start()
    {
        Refresh();
    }

    private void Refresh() // UI 업데이트
    {
        CurrentUserInfo(); // 현재 유저 정보 업데이트
        GameManager.Instance.SaveUserData();
    }

    private void CurrentUserInfo() // 현재 유저 정보
    {
        nameText.text = GameManager.Instance.CurrentUserData.name;
        balanceText.text = $"Balance   {GameManager.Instance.CurrentUserData.balance:N0}";
        cashText.text = $"{GameManager.Instance.CurrentUserData.cash:N0}";
    }

    public void OnDepositButtonClick() // 입금 창으로 넘어가는 버튼
    {
        deposit.SetActive(true);
        atm.SetActive(false);
    }

    public void OnWithdrawalButtonClick() // 출금 창으로 넘어가는 버튼
    {
        withdrawal.SetActive(true);
        atm.SetActive(false);
    }

    public void OnRemittanceButtonClick() // 송금 창으로 넘어가는 버튼
    {
        remittance.SetActive(true);
        atm.SetActive(false);
    }

    public void OnMoneyChangeButtonClick(int amount) // 입출금(10000, 30000, 50000) 버튼
    {
        if (deposit.activeSelf) // 입금 창일 때
        {
            if (GameManager.Instance.CurrentUserData.cash >= amount) // 현금 >= 입금 금액
            {
                GameManager.Instance.DepositCash(amount); // 입금 진행
            }
            else
            {
                bgPanel.SetActive(true);
            }
        }
        else if (withdrawal.activeSelf) // 출금 창일 때
        {
            if (GameManager.Instance.CurrentUserData.balance >= amount) // 통장 금액 >= 출금 금액
            {
                GameManager.Instance.WithdrawalCash(amount); // 출금 진행
            }
            else
            {
                bgPanel.SetActive(true);
            }
        }

        Refresh();
    }

    public void OnRemittanceCashButtonClick() // 송금하는 버튼
    {
        string remittanceTargetText = remittanceTargetInputField.text.Trim(); // 인풋필드에서 입력한 대상 텍스트를 저장
        string remittanceCashText = remittanceCashInputField.text.Trim(); // 인풋필드에서 입력한 현금 텍스트를 저장

        // 첫 번째 요소(w.id 또는 w.name)를 반환하거나 없으면 기본값(null)을 반환함
        UserData targetUser =
            GameManager.Instance.userDataList.FirstOrDefault(w =>
                w.name == remittanceTargetText || w.id == remittanceTargetText);

        if (targetUser == null) // 송금 대상이 없는 ID면 에러
        {
            bgPanel.SetActive(true);
            errorText.text = "대상이 없습니다.";

            return; // 강제 종료
        }

        // 송금 대상 / 금액을 입력 안하면 에러
        if (string.IsNullOrEmpty(remittanceTargetText) || string.IsNullOrEmpty(remittanceCashText))
        {
            bgPanel.SetActive(true);
            errorText.text = "입력 정보를 확인해주세요.";

            return; // 강제 종료
        }

        if (int.TryParse(remittanceCashText, out int remittanceCash)) // 송금 금액 숫자형으로 변환
        {
            // 현재 유저 금액 >= 송금할 금액
            if (GameManager.Instance.CurrentUserData.cash >= remittanceCash)
            {
                GameManager.Instance.RemittanceCash(targetUser, remittanceCash);
            }
            else // 잔액이 부족하면 에러
            {
                bgPanel.SetActive(true);
                errorText.text = "잔액이 부족합니다.";

                return; // 강제 종료
            }
        }
        else
        {
            bgPanel.SetActive(true);
            errorText.text = "금액을 다시 입력해주세요.";

            return; // 강제 종료
        }

        if (GameManager.Instance.CurrentUserData == targetUser)
        {
            bgPanel.SetActive(true);
            errorText.text = "자기 자신에게는 불가능합니다.";

            return; // 강제 종료
        }
        
        Refresh();
    }

    public void InputDepositButtonClick() // 직접 입력 입금 버튼
    {
        string amountText = depositInputField.text; // 직접 입력 금액 텍스트를 저장

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        // 변환이 되었으면 true
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.DepositCash(amount);
            depositInputField.text = ""; // 문구 초기화
            Refresh();
        }
    }

    public void InputWithdrawalButtonClick() // 직접 입력 출금 버튼
    {
        string amountText = withdrawalInputField.text; // 직접 입력 금액 텍스트를 저장

        // 입력 받은 값을 문자열에서 정수형으로 변환시켜 amount변수에 저장
        if (int.TryParse(amountText, out int amount))
        {
            GameManager.Instance.WithdrawalCash(amount);
            withdrawalInputField.text = ""; // 문구 초기화
            Refresh();
        }
    }

    public void PanelOk() // 금액부족 판넬 Ok버튼
    {
        bgPanel.SetActive(false);
    }

    public void OnBackButtonClick() // 뒤로 가기
    {
        if (deposit.activeSelf) // 입금 오브젝트 활성화 되어있을 경우
        {
            deposit.SetActive(false);
        }
        else if (withdrawal.activeSelf) // 출금 오브젝트 활성화 되어있을 경우
        {
            withdrawal.SetActive(false);
        }
        else if (remittance.activeSelf) // 송금 오브젝트 활성화 되어있을 경우
        {
            remittance.SetActive(false);
        }

        atm.SetActive(true);
        Refresh();
    }
}