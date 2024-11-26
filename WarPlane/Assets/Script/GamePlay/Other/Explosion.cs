using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isExplosion;
    void Awake()
    {
        if(isExplosion)
        {
            SoundManager.Instance.PlayVFXSound(2);
        }else
        {
            SoundManager.Instance.PlayVFXSound(3);
        }
    }
    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
