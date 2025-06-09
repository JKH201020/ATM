using System.Linq;
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

    private bool _signUpConfirm; // 회원가입이 확인되었는지

    // 비밀번호 TMP_InputField가 한글로 입력되면 *이 한번에 8개 출력되는 것을 방지
    // 게임 플레이중에는 키보드 입력이 텍스트를 의도하는 것이 아니기 때문에 unity에서 자동으로 IME 기능을 비활성화
    void Update()
    {
        // 한글 기준으로 한 문자(김 <- 이런식으로)를 완성해야 '*'하나로 인식하는 것 같음
        // 비밀번호 입력 필드에 포커스가 있을 때만 IME를 끕
        // 다른 입력 필드(ID, 이름 등)에는 IME 설정이 영향을 주지 않도록 합니다.
        if (loginPS.isFocused || signUpPS.isFocused || signUpPSConfirm.isFocused)
        {
            Input.imeCompositionMode = IMECompositionMode.Off;
        }
        else
        {
            // 다른 입력 필드에 포커스가 있거나 아무것도 포커스되지 않은 경우
            // Unity가 자동으로 IME를 관리하도록 둡니다.
            Input.imeCompositionMode = IMECompositionMode.On;
        }
    }

    public void OnSingUpButtonClick() // 회원가입 버튼
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

            if (_signUpConfirm) // 회원가입 완료상태면
            {
                signUpPanel.SetActive(false); // 회원가입 판넬 비활성화
                _signUpConfirm = false; // 회원가입 상태 미완으로 변경
            }
        }
    }

    public void OnOkButtonClick() // 에러 판넬 Ok버튼
    {
        if (loginErrorPanel.activeSelf) // 로그인 에러 판넬 활성화 상태라면
        {
            loginErrorPanel.SetActive(false); // 비활성화로 전환
        }
        else // 로그인 에러 판넬 비활성화 상태라면
        {
            signUpErrorPanel.SetActive(false); // 회원가입 에러 판넬 비활성화
        }
    }

    public void OnCancelButtonClick() // 회원가입 Cancel버튼 클릭
    {
        signUpPanel.SetActive(false); // 회원가입 판넬 비활성화
    }

    private void SignUpSaveData() // 회원가입 데이터 저장
    {
        // 공백이 포함되어 있으면 공백 제거한 상태로 저장
        string newID = signUpID.text.Trim(); 
        string newName = signUpName.text.Trim();
        string newPassWord = signUpPS.text.Trim();
        string newPSConfirm = signUpPSConfirm.text.Trim();

        if (newID.Contains(" ")) // 아이디에 공백이 있을 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "아이디를 확인해주세요."; // 에러문구
            return;
        }

        if (newName.Contains(" ")) // 이름에 공백이 있을 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "이름을 확인해주세요."; // 에러문구
            return;
        }

        if (newPassWord != newPSConfirm) // 비밀번호와 확인 비밀번호가 다를 경우
        {
            signUpErrorPanel.SetActive(true); // 에러 판넬 활성화
            signUpErrorText.text = "비밀번호를 확인해주세요."; // 에러문구
            return;
        }

        // 위의 아무 문제가 없으면 유저 데이터 저장

        _signUpConfirm = true; // 회원가입 완료
        signUpErrorText.text = ""; // 에러 문구 초기화

        UserData newUser = new UserData(newID, newName, newPassWord); // 신규 유저 등록
        GameManager.Instance.userDataList.Add(newUser); // 유저 목록에 추가
        GameManager.Instance.SaveUserData(); // 신규 유저 데이터 저장
    }

    public void OnLoginButtonClick() // 로그인 버튼 (json)
    {
        // loginErrorPanel의 자식인 TextMeshProUGUI 오브젝트를 저장;
        TextMeshProUGUI loginErrorText = loginErrorPanel.GetComponentInChildren<TextMeshProUGUI>();

        string userID = loginID.text.Trim();
        string userPS = loginPS.text.Trim();

        // 첫 번째 요소(w.ID)를 반환하거나 없으면 기본값(null)을 반환함
        UserData savedUserData = GameManager.Instance.userDataList.FirstOrDefault(w => w.id == userID);

        if (userID == "")
        {
            loginErrorText.text = "아이디를 입력하세요.";
            loginErrorPanel.SetActive(true);

            return;
        }
        
        if (userPS == "")
        {
            loginErrorText.text = "비밀번호를 입력하세요.";
            loginErrorPanel.SetActive(true);

            return;
        }

        if (savedUserData != null && savedUserData.id == userID) // 저장된 아이디와 입력 아이디가 같으면
        {
            if (savedUserData.ps == userPS) // 저장된 비번과 입력 비번이 같으면
            {
                GameManager.Instance.CurrentUserInfo(savedUserData); // 게임매니저에 로그인한 유저를 현재 사용 유저로 저장
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