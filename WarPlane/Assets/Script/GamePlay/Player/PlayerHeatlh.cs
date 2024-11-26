using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeatlh : MonoBehaviour
{
    //Health
    
    public int MaxHP;
    public int HP;
    public  int Score;
    public int amount; 
    public bool IsDead;
    public bool isInvicable;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinking = false;
    public GameObject shield;
    public GameObject Explosion;
    public GameObject Splash;
    void Start()
    {
        HP = MaxHP;
        isInvicable = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        shield.SetActive(false);
    }
    void Update()
    {
        if (HP == 1 && !isInvicable && !isBlinking)
        {
            StartCoroutine(BlinkRed());
        }

        if (HP <= 0 && !IsDead)
        {
            IsDead = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(!isInvicable)
            {
                HP -= amount;
                Instantiate(Explosion,transform.position, Quaternion.identity); 
                StartCoroutine(onShield());
                
            }

           
        }
        if (collision.CompareTag("Sea"))
        {
            Instantiate(Splash,transform.position, Quaternion.identity); 
        }
    }
    IEnumerator onShield()
    {
        isInvicable = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(2);
        isInvicable = false;
        shield.SetActive(false);
    }
    IEnumerator BlinkRed()
    {
        isBlinking = true;
        while (HP == 1 && isBlinking)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.25f);
        }
        isBlinking = false;
    }   


}
