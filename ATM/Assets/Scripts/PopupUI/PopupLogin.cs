using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    [Header("PopupLogin 씬 오브젝트 할당")] [SerializeField]
    private GameObject SignUpPanel;

    [SerializeField] private GameObject ErrorPanel;
    
    #region PopupLogin Scene UI

    public void OnSingUpButtonClick()
    {
        if (SignUpPanel.activeSelf == false)
        {
            SignUpPanel.SetActive(true);
        }
        else
        {
            SignUpPanel.SetActive(false);
        }
    }

    public void OnCancelButtonClick()
    {
        if (SignUpPanel.activeSelf == true)
        {
            SignUpPanel.SetActive(false);
        }
        else if (ErrorPanel.activeSelf == true)
        {
            ErrorPanel.SetActive(false);
        }
    }

    #endregion
}
