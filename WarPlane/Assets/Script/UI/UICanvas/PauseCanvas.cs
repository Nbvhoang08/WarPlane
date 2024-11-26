using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseCanvas : UICanvas
{
    // Start is called before the first frame update
    public Sprite OnVolume;
    public Sprite OffVolume;

    [SerializeField] private Image buttonImage;
    [SerializeField] private GameManager _gameManager;
    public Text Score;
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
      
    }
    void Update()
    {
        UpdateButtonImage();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayVFXSound(4);
        UIManager.Instance.CloseUI<PauseCanvas>(0.2f);
            
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

