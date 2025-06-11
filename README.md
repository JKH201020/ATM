-----

# 💰 ATM 시뮬레이터 💰

사용자 로그인, 회원가입, 입금, 출금, 그리고 다른 사용자에게 송금하는 기능을 제공하여 실제 ATM 사용 경험을 간접적으로 체험할 수 있습니다.

-----

## ✨ 주요 기능

### 1\. 로그인 및 회원가입

![image](https://github.com/user-attachments/assets/5814d637-48cb-4878-a31f-1c582958b744)

  * **로그인:** 사용자 ID와 비밀번호를 입력하여 로그인합니다. 비밀번호 입력 시 자동으로 영어로 변환됩니다.
    
![image](https://github.com/user-attachments/assets/2ae77a9a-5bb3-43bc-b41d-b6ac9efccd83)
![image](https://github.com/user-attachments/assets/1fb755bb-24a0-4a0e-bc19-59217f98591e)
![image](https://github.com/user-attachments/assets/1351e5ec-2ab1-4492-8d29-3d3f15cc8cfa)
![image](https://github.com/user-attachments/assets/e0a4b949-d234-4f17-bff5-15859040f2f4)

  * **회원가입:**
      * 새로운 계정을 생성할 수 있습니다.
      * 사용자 정보(ID, 이름, 비밀번호)를 입력해야 합니다.
      * **초기 잔액:** 신규 가입 시 통장 잔고 15만원, 현금 10만원이 기본으로 설정됩니다.
      * **유효성 검사:** 기입란에 공백을 포함하거나 입력 필드를 비워두면 에러 메시지가 표시됩니다.

### 2\. ATM 메인 화면

![image](https://github.com/user-attachments/assets/bbbd96ab-1294-4c3f-89a8-0070c9680c01)

  * 로그인 성공 시, 현재 로그인한 사용자의 이름, 통장 잔고, 소지 현금 정보가 표시됩니다.
  * **입금**, **출금**, **송금** 기능을 사용할 수 있습니다.

### 3\. 입금 및 출금

![image](https://github.com/user-attachments/assets/6ef85b8d-6298-4050-a9fc-4ad05ca4bed3)
![image](https://github.com/user-attachments/assets/5641c7df-5678-4d99-afb8-e5b3c50288c4)

  * **간편 기능:** 1만원, 3만원, 5만원 버튼을 통해 빠르게 입금 또는 출금할 수 있습니다.
  * **직접 입력:** 원하는 금액을 직접 입력하여 입금 또는 출금할 수 있습니다.

![image](https://github.com/user-attachments/assets/17a92a83-edda-4498-b907-801a609e809d)
![image](https://github.com/user-attachments/assets/3f1f7814-089a-49b6-97e2-f1f4a16dcb3c)
  
  * **잔액 부족 경고:** 소지하고 있는 현금이나 통장 잔고보다 더 많은 금액을 입금/출금하려고 할 경우 경고 메시지가 표시됩니다.
  * **돌아가기:** ATM 메인 화면으로 돌아갈 수 있습니다.

### 4\. 송금

![image](https://github.com/user-attachments/assets/2c4ba7e6-f2c2-444c-bf10-160cdfe5115b)

  * 다른 사용자에게 통장 잔고를 송금하는 기능입니다.

![image](https://github.com/user-attachments/assets/f260e6a8-7c36-4f53-8365-4733990e6a51)
![image](https://github.com/user-attachments/assets/0af98a6b-38e5-4a0c-b3bf-97d5cd0dcecd)

  * **사용자 데이터 관리:** 모든 사용자 데이터는 JSON 파일에 저장되어 관리됩니다. (JSON 파일은 프로젝트의 최상위 디렉토리에 위치해야 합니다.)

![image](https://github.com/user-attachments/assets/aa8ef4c8-9c0d-4b25-94bb-2467d6a32b26)
    
  * **송금 과정:**
      * 송금할 사람의 **이름** 또는 **ID**를 입력합니다.
      * 송금할 **금액**을 입력합니다.
      * 송금이 완료되면 보낸 사람과 받은 사람의 통장 잔고(`balance`)가 업데이트됩니다.
    
  * **잔액 부족 경고:** 통장 잔고보다 더 많은 금액을 송금하려고 할 경우 경고 메시지가 출력됩니다.
-----

## 🛠️ 개발 환경

  * C#
  * Unity - 2022.3.17f1

-----

## 💻 코드 구조

프로젝트의 주요 스크립트와 그 역할은 다음과 같습니다. 각 스크립트의 경로를 클릭하면 해당 코드를 바로 확인할 수 있습니다.

  * **ATM 사용자 데이터 관리**: `UserData` 클래스는 ATM 사용자 정보를 저장하고 관리합니다.

      * [ATM/Assets/Scripts/UserData.cs](https://github.com/JKH201020/ATM/blob/main/ATM/Assets/Scripts/UserData.cs)

  * **게임 관리자**: `GameManager`는 싱글톤 패턴을 사용하여 ATM의 핵심 기능을 제어하고, 사용자 데이터를 JSON 파일로 저장 및 로드하는 역할을 합니다.

      * [ATM/Assets/Scripts/Manager/GameManager.cs](https://github.com/JKH201020/ATM/blob/main/ATM/Assets/Scripts/Manager/GameManager.cs)

  * **로그인 UI 관리**: `PopupLogin` 스크립트는 로그인 씬에서 사용되는 UI 요소를 관리합니다.

      * [ATM/Assets/Scripts/PopupUI/PopupLogin.cs](https://github.com/JKH201020/ATM/blob/main/ATM/Assets/Scripts/PopupUI/PopupLogin.cs)

  * **뱅크 UI 관리**: `PopupBank` 스크립트는 ATM 뱅크 씬에서 사용되는 UI 요소를 관리합니다.

      * [ATM/Assets/Scripts/PopupUI/PopupBank.cs](https://github.com/JKH201020/ATM/blob/main/ATM/Assets/Scripts/PopupUI/PopupBank.cs)

-----
