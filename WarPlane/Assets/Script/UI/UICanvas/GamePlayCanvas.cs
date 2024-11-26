using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCanvas : UICanvas
{
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private GameManager _gameManager;
    private List<Image> HeartImages = new List<Image>();
    public Text score;
    [SerializeField] private Transform HeartContainer;
    [SerializeField] private GameObject HeartPrefab;
    public GameObject player;
    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }
    void Start()
    {
        spawnHeartManual();
    }
    private void Update()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        UpdateScoreText();
        UpdateHPCount();
    }
    public void Launch()
    {
        player.GetComponent<PlayerMoveMent>().Launch();
    }
    public void Shoot()
    {
        player.GetComponent<Shooter>().shoot();
    }




    public void pauseBtn()
    {
        UIManager.Instance.OpenUI<PauseCanvas>();
        SoundManager.Instance.PlayVFXSound(4);
        Time.timeScale = 0; 
    }

    private void UpdateScoreText()
    {
        if (score != null)
        {  
            score.text = "Score: " + _gameManager.Score.ToString();
        }
    }

    public void UpdateHPCount()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        int remainingSteps = _gameManager.hp;

        for (int i = 0; i < HeartImages.Count; i++)
        {
            if (i < remainingSteps)
            {
                HeartImages[i].sprite = fullSprite;
            }
            else
            {
                HeartImages[i].sprite = emptySprite;
            }
        }
    }
    public void spawnHeartManual()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        // Xóa các UI Count hiện có
        foreach (Transform child in HeartContainer)
        {
            Destroy(child.gameObject);
        }
        HeartImages.Clear();
        for (int i = 0; i < 3; i++)
        {
            GameObject heart = Instantiate(HeartPrefab, HeartContainer);
            RectTransform rectTransform = heart.GetComponent<RectTransform>();
            // Thêm vào danh sách
            Image stepCountImage = heart.GetComponent<Image>();
            stepCountImage.sprite = fullSprite;
            HeartImages.Add(stepCountImage);
        }

            
    }


}
