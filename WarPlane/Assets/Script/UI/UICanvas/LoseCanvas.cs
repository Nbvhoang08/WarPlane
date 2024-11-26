using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoseCanvas : UICanvas
{
     // Start is called before the first frame update
    public Sprite OnVolume;
    public Sprite OffVolume;
    public Text Score;
    public Text HighScore;
    [SerializeField] private Image buttonImage;
    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
       
    }
    void Start()
    {
  
        UpdateButtonImage();
       
  
    }
    void OnEnable()
    {
        Score.text = "Score: " +_gameManager.Score.ToString();
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Lấy điểm cao đã lưu, mặc định là 0 nếu chưa có
        HighScore.text = "HighScore: " + highScore.ToString(); // Hiển thị điểm cao
    }
    void Update()
    {
        UpdateButtonImage();

    }
    public void RetryBtn()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayVFXSound(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIManager.Instance.CloseUI<LoseCanvas>(0.1f);        
    }

    public void HomeBtn()
    {
        UIManager.Instance.CloseAll();
        Time.timeScale = 1; 
        SoundManager.Instance.PlayVFXSound(4);
        SceneManager.LoadScene("Home");
        UIManager.Instance.OpenUI<HomeCanvas>();
    } 
    public void SoundBtn()
    {
        SoundManager.Instance.TurnOn = !SoundManager.Instance.TurnOn;
        UpdateButtonImage();
    }
    private void UpdateButtonImage()
    {
        if (SoundManager.Instance.TurnOn)
        {
            buttonImage.sprite = OnVolume;
        }
        else
        {
            buttonImage.sprite = OffVolume;
        }
    }   
}
