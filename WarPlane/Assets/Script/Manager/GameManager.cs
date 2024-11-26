using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHP =3;
    public int hp;
    public int Score;
    public PlayerHeatlh player; 
    public bool GameOver;
    
    void Awake()
    {
        if(player == null)
        {
            player= FindObjectOfType<PlayerHeatlh>();
        
        }
    }
    void Update()
    {
        if(player == null)
        {
            player= FindObjectOfType<PlayerHeatlh>();
        }   
        Score = player.Score;
        hp = player.HP;
        if(player.IsDead && !GameOver)
        {
            UIManager.Instance.OpenUI<LoseCanvas>();
            SaveHighScore(Score);
            StartCoroutine(Delay());
            GameOver= true;
        }

    }
    IEnumerator Delay(){
        yield return new WaitForSeconds(0.3f);
        Time.timeScale=0;
    }
    void SaveHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Lấy điểm cao hiện tại, mặc định là 0 nếu chưa có
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score); // Lưu điểm cao mới
            PlayerPrefs.Save(); // Lưu thay đổi vào bộ nhớ
        }
    }
    
}
