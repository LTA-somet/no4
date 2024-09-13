using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
  
    
    private void Update()
    {
        transform.Rotate(Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            collision.gameObject.SetActive(false);  
        }
    }
}
