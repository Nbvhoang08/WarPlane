using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletObj;
    public Transform gunPos;
    public Transform gunPos1;

    private float fire1;



  

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R) )
        {
            if (flag)
            {
                flag = false;
                StartCoroutine(Fire());
            }
            
        }


    }


    bool flag = true;
    IEnumerator Fire()
    {
        Instantiate(bulletObj, gunPos.position, transform.rotation);
        Instantiate(bulletObj, gunPos1.position, transform.rotation);
        yield return new WaitForSeconds(0.3f);

        flag = true;
    }
}