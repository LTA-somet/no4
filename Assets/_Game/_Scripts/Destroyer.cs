using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
 public int blockCount = 0;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            collision.gameObject.SetActive(false);  
            blockCount++;
        }
    }
 
}
