using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeCanvas : UICanvas
{
     public Sprite OnVolume;
    public Sprite OffVolume;

    [SerializeField] private Image buttonImage;

    void Start()
    {
  
        UpdateButtonImage();
  
    }
    void Update()
    {
        UpdateButtonImage();
    }
    public void playBtn()
    {
        UIManager.Instance.CloseUIDirectly<HomeCanvas>();
        UIManager.Instance.OpenUI<GamePlayCanvas>();
        SceneManager.LoadScene("GamePlay");
        SoundManager.Instance.PlayVFXSound(4);
            
    }

    public void SoundBtn()
    {
        SoundManager.Instance.TurnOn = !SoundManager.Instance.TurnOn;
        UpdateButtonImage();
         SoundManager.Instance.PlayVFXSound(4);
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
