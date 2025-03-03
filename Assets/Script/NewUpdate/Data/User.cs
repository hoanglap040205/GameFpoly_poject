using System.Collections.Generic;

[System.Serializable]
public class LevelRecord
{
    public int levelNumber;
    public float timeSpent; //Thoi gian hoan thanh man choi

    public LevelRecord(int levelNumber, float timeSpent)
    {
        this.levelNumber = levelNumber;
        this.timeSpent = timeSpent;
    }
}

[System.Serializable]
public class User
{
    public string username;
    public string password;
    public List<LevelRecord> playedLevels; //Danh sach man choi da hoan thanh

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
        this.playedLevels = new List<LevelRecord>();
    }

    //Them thong tin man choi vao danh sach
    public void AddLevelRecord(int levelNumber, float timeSpent)
    {
        LevelRecord record = new LevelRecord(levelNumber, timeSpent);
        playedLevels.Add(record);
    }
}
