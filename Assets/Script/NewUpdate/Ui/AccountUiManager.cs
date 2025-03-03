using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountUiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panelObject;//Panel trong scene
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private TextMeshProUGUI currentPlayer;//Ten nguoi dung dang nhap
    
    [SerializeField] private TextMeshProUGUI feedBack;
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField password;

    public DataUserManager data;


    private GameObject currentOpening;//panel dang duoc mo

    private string playerLogin;
    private bool isOpened;

    //Tu dong dang nhap khi startGame
    private string autoLoginUserName;
    private string autoLoginPassword;
    private void Start()
    {
        isOpened = true;
        for(int i = 0; i < panelObject.Length; i++)
        {
            if (panelObject[i].activeInHierarchy)
            {
                panelObject[i].SetActive(!isOpened);
            }
        }

        menuPanel.SetActive(isOpened);
        autoLoginUserName = PlayerPrefs.GetString("UserName");
        autoLoginPassword = PlayerPrefs.GetString("Password");
        CheckLastPlayerLogin(autoLoginUserName, autoLoginPassword);
        currentPlayer.text = playerLogin;
        
    }

    
    public void OpenedMenu()
    {
        SceneManager.LoadScene("Levels");

    }
    public void Opened(GameObject onOpened)
    {
        CheckPanelOpening();
        onOpened.SetActive(isOpened);
    }

    private void CheckPanelOpening()
    {
        for(int i = 0; i < panelObject.Length; i++)
        {
            if (panelObject[i].activeInHierarchy)
            {
                currentOpening = panelObject[i];
                currentOpening.SetActive(!isOpened);
                return;
            }
        }
    }
    public void BackToMenu( GameObject menuPanel)
    {
        CheckPanelOpening();
        menuPanel.SetActive(isOpened);
    }

    public void Exitgame()
    {
        Application.Quit();
        Debug.Log("Thoatgame");
    }

    
    public void OnClickRegister()
    {
        string username = userName.text;
        string pass = password.text;

        if (data.Register(username, pass))
        {
            feedBack.text = "Dang ki thanh cong";
            playerLogin = username;
            AutoSavePlayerLogin(username, pass);
            BackToMenu(menuPanel);
        }
        else
        {
            feedBack.text = "Dang ki khong thanh cong";
        }
    }
    public void OnClickLogin()
    {
        string username = userName.text;
        string pass = password.text;

        if (data.Login(username, pass))
        {
            feedBack.text = "Thanh cong";
            playerLogin = username;
            AutoSavePlayerLogin(username,pass);
            BackToMenu(menuPanel);
        }
        else
        {
            feedBack.text = "Thong tin tai khoan mat khau khong chinh xac";
            username = "";
            pass = "";
        }
    }
    //tu dong dang nhap tai khoan gan nhat

    public void AutoLogin( string user, string pass)
    {
        Debug.LogWarning("user " + PlayerPrefs.GetString("UserName"));
        Debug.LogWarning("Pass " + PlayerPrefs.GetString("Password"));
        

        if (data.Login(user,pass))
        {
            playerLogin = autoLoginUserName;
            Debug.Log("Player dang nhap" + playerLogin);
        }
        else
        {
            Debug.Log("Loiiii");
            
        }
        
    }
    //Tu dong luu lai thong tin nguoi choi vua dnag nhap
    private void AutoSavePlayerLogin(string user, string pass)
    {
        PlayerPrefs.SetString("UserName", user);
        PlayerPrefs.SetString("Password", pass);
        PlayerPrefs.Save();
        Debug.Log("Save player login");
        Debug.Log("Ten nguoi choi vua dang nhap" +PlayerPrefs.GetString("UserName"));
    }
    //kiem tra nguoi cuoi cung dang nhap
    private void CheckLastPlayerLogin(string user,string pass)
    {
        if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
        {
            Debug.Log("nguoi dung" + user);
            AutoLogin(user,pass);
        }
        else
        {
            Debug.Log("Khong co nguo choi nao dang nhap truoc day");
            return;

        }
    }

    

}
