using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataUserManager : MonoBehaviour
{
    private string filePath;
    private void Awake()
    {
        
        filePath = Application.persistentDataPath + "/users.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        Debug.Log("Data will be stored at: " + filePath);
    }

    //Ham dang ki nguoi dung moi
    //Kiem tra xem nguoi dung da ton tai hay chua neu co thi false chua thi hoan tat dang ki
    public bool Register(string username, string password)
    {
        if (UserExists(username))
        {
            Debug.Log("User already exists.");
            return false;
        }

        User newUser = new User(username, password);
        List<User> users = LoadUsersFromFile();
        users.Add(newUser);
        SaveUsersToFile(users);
        PlayerPrefs.SetInt("LevelMaxCurrent", GetInfoLevelPlayer(username));

        Debug.Log("User registered successfully.");
        return true;
    }

    // ham dang nhap
    //kiem tra xem nguoi dung da ton tai chua neu co thi tra ve true
    public bool Login(string username, string password)
    {
        List<User> users = LoadUsersFromFile();

        foreach (var user in users)
        {
            if (user.username == username && user.password == password)
            {
                Debug.Log("Login successful.");
                PlayerPrefs.SetInt("LevelMaxCurrent", GetInfoLevelPlayer(username));
                Debug.Log("Level max" + PlayerPrefs.GetInt("LevelMaxCurrent"));

                return true;
            }
        }

        Debug.Log("Login failed. Incorrect username or password.");
        return false;
    }

    // Them du lieu man choi vao nguoi dung hien tai
    public void SaveLevelRecord(string username, int levelNumber, float timeSpent)
    {
        List<User> users = LoadUsersFromFile();

        foreach (var user in users)
        {
            if (user.username == username)
            {
                user.AddLevelRecord(levelNumber, timeSpent);
                SaveUsersToFile(users); // luu du lieu lai sau khi cap nhat
                Debug.Log("Level data saved successfully.");
                return;
            }
        }

        Debug.LogError("User not found.");
    }
    //Lay level max cua nguoi choi hien tai
    public int GetInfoLevelPlayer(string username)
    {
        List<User> users = LoadUsersFromFile();
        foreach(var user in users)
        {
            if(user.username == username)
            {
              return  user.playedLevels.Count;
            }
        }
        return 0;
    }
    // Kiem tra nguoi dung ton tai hay chua
    //kiem tra xem nguoi dung ton tai khong
    private bool UserExists(string username)
    {
        List<User> users = LoadUsersFromFile();

        foreach (var user in users)
        {
            if (user.username == username)
            {
                return true;
            }
        }
        return false;
    }

    // luu danh sach vao file json
    private void SaveUsersToFile(List<User> users)
    {
        string json = JsonUtility.ToJson(new UserList(users), true);
        File.WriteAllText(filePath, json);
    }

        
    public List<User> GetTop3Players()
    {
        List<User> users = LoadUsersFromFile();
        List<User> validUsers = new List<User>();

        // Them danh sach neu nguoi do hoan thanh it nhat mot man choi
        foreach (var user in users)
        {
            if (user.playedLevels.Count > 0)
            {
                validUsers.Add(user);
            }
        }

        // Sap xep
        validUsers.Sort((a, b) =>
        {
            // So sanh 
            int levelComparison = b.playedLevels.Count.CompareTo(a.playedLevels.Count); // Sap xep giam dan
            if (levelComparison == 0)
            {
                // Neu so man bang nhau thi so sanh thoi gian
                for (int i = 0; i < a.playedLevels.Count; i++)
                {
                    // Kiem tra xem nguoi choi co cung so man hay khong
                    if (i >= b.playedLevels.Count)
                        return -1; // Nguoi choi b co so man thap hon thi xep sau

                    // So sanh thoi gian tung man choi
                    int timeComparison = a.playedLevels[i].timeSpent.CompareTo(b.playedLevels[i].timeSpent);
                    if (timeComparison != 0)
                    {
                        return timeComparison; // Thoi gian choi it hon se dung truoc
                    }
                }
            }
            return levelComparison;
        });

        // Tra ve top 3
        List<User> topPlayers = new List<User>();
        for (int i = 0; i < Mathf.Min(3, validUsers.Count); i++)
        {
            topPlayers.Add(validUsers[i]);
        }

        return topPlayers;
    }

    

    // Tai danh sach nguoi dung tu file json
    private List<User> LoadUsersFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(json))
            {
                Debug.Log("File trang");

                return new List<User>(); // Tra ve danh sach neu file rong
            }

            UserList userList = JsonUtility.FromJson<UserList>(json);

            if (userList != null && userList.users != null)
            {
                return userList.users;
            }
            else
            {
                return new List<User>(); // Khong chuyen doi thanh cong
            }
        }
        else
        {
            Debug.Log("File khong ton tai");
            return new List<User>(); //Khong ton tai file
            
        }
    }
    // Class luu danh sach nguoi dung cho viec chuyen doi json
    [System.Serializable]
    private class UserList
    {
        //Class chua cac danh sach doi tuong
        public List<User> users;

        public UserList(List<User> users)
        {
            this.users = users;
        }
    }
    
}
