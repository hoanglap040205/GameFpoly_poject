using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject leaderboardPanel;  
    public TextMeshProUGUI playerEntryTemplate;     
    public DataUserManager dataUserManager;  
    private void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        foreach (Transform child in leaderboardPanel.transform)
        {
            if (child != playerEntryTemplate.transform)  
            {
                Destroy(child.gameObject);
            }
        }

        List<User> topPlayers = dataUserManager.GetTop3Players();

        foreach (User player in topPlayers)
        {
            TextMeshProUGUI newEntry = Instantiate(playerEntryTemplate, leaderboardPanel.transform);
            newEntry.gameObject.SetActive(true);  

            string displayText = player.username + " - Levels: " + player.playedLevels.Count;

            if (player.playedLevels.Count > 0)
            {
                LevelRecord lastLevel = player.playedLevels[player.playedLevels.Count - 1];
                displayText += " - Last Time: " + lastLevel.timeSpent + "s";
            }

            newEntry.text = displayText;  
        }
    }
}
