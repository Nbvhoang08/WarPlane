using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCanvas : UICanvas
{
    public void pauseBtn()
    {
        UIManager.Instance.OpenUI<PauseCanvas>();
        Time.timeScale = 0;
    }
}
