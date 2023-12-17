using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public string playerName = "player0";
    public string scoreKey => playerName+".score";
    
    private int _score;
    public int score => _score;
    
    private void OnEnable()
    {
        LoadScore();
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        if(!PlayerPrefs.HasKey(scoreKey))
            return;
        
        _score = PlayerPrefs.GetInt(scoreKey);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(scoreKey, _score);
    }

    public void AddScore(int count)
    {
        _score += count;
        SaveScore();
    }

    public void ResetScore()
    {
        _score = 0;
        SaveScore();
    }
}
