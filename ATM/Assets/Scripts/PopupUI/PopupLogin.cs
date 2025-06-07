using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupLogin : MonoBehaviour
{
    [Header("로그인 오브젝트 할당")] [SerializeField]
    private TMP_InputField loginID; // 로그인 아이디

    [SerializeField] private TMP_InputField loginPS; // 로그인 비번
    [SerializeField] private GameObject loginErrorPanel; // 로그인 에러 판넬

    [Header("회원가입 InputText 할당")] [SerializeField]
    private TMP_InputField signUpID; // 아이디 입력

    [SerializeField] private TMP_InputField signUpName; // 이름 입력
    [SerializeField] private TMP_InputField signUpPS; // 비번 입력
    [SerializeField] private TMP_InputField signUpPSConfirm; // 비번 확인

    [Header("회원가입 오브젝트 할당")] [SerializeField]
    private GameObject signUpPanel; // 회원가입 판넬

    [SerializeField] private GameObject signUpErrorPanel; // 에러 판넬
    [SerializeField] private TextMeshProUGUI signUpErrorText; // 비번 잘못 입력 시 출력되는 문구

    private bool _signUpConfirm = false; // 회원가입이 확인되었는지

    public void OnSingUpButtonClick()
    {
        if (signUpPanel.activeSelf == false) // 회원가입 판넬이 비활성화라면
        {
            // 메인화면 Sign Up버튼 누르면 회원가입 정보 기입 초기화 
            signUpID.text = "";
            signUpName.text = "";
            signUpPS.text = "";
            signUpPSConfirm.text = "";
            signUpErrorText.text = "";

            signUpPanel.SetActive(true); // 회원가입 판넬 활성화
        }
        else // 회원가입 판넬 활성화 상태라면
        {
            SignUpSaveData(); // 회원가입 데이터 저장

            if (_signUpConfirm == true) // 회원가입 완료상태면
            {
                signUpPanel.SetActive(false); // 회원가입 판넬 비활성화
                _signUpConfirm = false; // 회원가입 상태 미완으로 변경
            }
        }
    }

    public void OnOkButtonClick() // 에러 판넬 Ok버튼
    {
        if (loginErrorPanel.activeSelf == true)
        {
            loginErrorPanel.SetActive(false);
        }
        else
        {
            signUpErrorPanel.SetActive(false); // 에러 판넬 비활성화
        }
    }

    public void OnCancelButtonClick() // 회원가입 Cancel버튼 클릭
    {
        signUpPanel.SetActive(false); // 회원가입 판넬 비활성화
    }

    public void SignUpSaveData() // 회원가입 데이터 저장
    {
        string id = signUpID.text;
        string name = signUpName.text;
        string passWord = signUpPS.text;
        string psConfirm = signUpPSConfirm.text;

        if (id.Contains(" ")) // 아이디에 공백이 있을 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "아이디를 확인해주세요."; // 에러문구
            return;
        }

        if (name.Contains(" ")) // 이름에 공백이 있을 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "이름을 확인해주세요."; // 에러문구
            return;
        }

        if (passWord != psConfirm) // 비밀번호와 확인 비밀번호가 다를 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "비밀번호를 확인해주세요."; // 에러문구
            return;
        }

        // 위의 아무 문제가 없으면 유저 데이터 저장

        _signUpConfirm = true; // 회원가입 완료
        signUpErrorText.text = ""; // 에러 문구 초기화

        PlayerPrefs.SetString("ID", id); // 아이디 저장
        PlayerPrefs.SetString("ID/Name", name); // 이름 저장
        PlayerPrefs.SetString("ID/PassWord", passWord); // 비번 저장

        PlayerPrefs.Save(); // 데이터 저장
    }

    public void OnLoginButtonClick() // 로그인 버튼
    {
        string saveID = PlayerPrefs.GetString("ID");
        string savePS = PlayerPrefs.GetString("ID/PassWord");
        TextMeshProUGUI loginErrorText = loginErrorPanel.GetComponentInChildren<TextMeshProUGUI>();

        if (saveID == loginID.text) // 저장된 아이디와 입력 아이디가 같으면
        {
            if (savePS == loginPS.text) // 저장된 비번과 입력 비번이 같으면
            {
                SceneManager.LoadScene("PopupBank"); // 다음 씬으로 이동
            }
            else
            {
                loginErrorText.text = "비밀번호를 잘못 입력하셨습니다.";
                loginErrorPanel.SetActive(true);
            }
        }
        else
        {
            loginErrorText.text = "해당 아이디가 없습니다.";
            loginErrorPanel.SetActive(true);
        }
    }
}