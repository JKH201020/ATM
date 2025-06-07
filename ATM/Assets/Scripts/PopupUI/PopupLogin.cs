using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    [Header("PopupLogin 씬 오브젝트 할당")] [SerializeField]
    private GameObject panel; // 회원가입 판넬

    [SerializeField] private GameObject errorPanel; // 에러 판넬
    [SerializeField] private TextMeshProUGUI errorText; // 비번 잘못 입력 시 출력되는 문구

    [Header("InputText 오브젝트 할당")] [SerializeField]
    private TMP_InputField signUpID; // 아이디 입력

    [SerializeField] private TMP_InputField signUpName; // 이름 입력
    [SerializeField] private TMP_InputField signUpPS; // 비번 입력
    [SerializeField] private TMP_InputField signUpPSConfirm; // 비번 확인

    private bool _signUpConfirm = false; // 회원가입이 확인되었는지

    public void OnSingUpButtonClick()
    {
        if (panel.activeSelf == false)
        {
            // 메인화면 Sign Up버튼 누르면 회원가입 정보 기입 초기화 
            signUpID.text = "";
            signUpName.text = "";
            signUpPS.text = "";
            signUpPSConfirm.text = "";
            errorText.text = "";
            
            panel.SetActive(true);
        }
        else
        {
            SignUpSaveData();

            if (_signUpConfirm == true)
            {
                panel.SetActive(false);
            }
        }
    }

    public void OnOkButtonClick() // 에러 판넬 Ok버튼
    {
        errorPanel.SetActive(false);
    }

    public void OnCancelButtonClick() // SignUp Cancel버튼 클릭
    {
        panel.SetActive(false);
    }

    public void SignUpSaveData()
    {
        string id = signUpID.text;
        string name = signUpName.text;
        string passWord = signUpPS.text;
        string psConfirm = signUpPSConfirm.text;

        if (passWord == psConfirm)
        {
            _signUpConfirm = true; // 회원가입 완료
            errorText.text = ""; // 에러 문구 초기화

            PlayerPrefs.SetString("ID", id); // 아이디 저장
            PlayerPrefs.SetString("ID/Name", name); // 이름 저장
            PlayerPrefs.SetString("ID/PassWord", passWord); // 비번 저장
            PlayerPrefs.SetString("ID/PassWordConfirm", psConfirm); // 비번 확인

            PlayerPrefs.Save(); // 데이터 저장
        }
        else
        {
            errorPanel.SetActive(true);
            _signUpConfirm = false;
            errorText.text = "비밀번호를 확인해주세요."; // 에러문구
        }
    }
}