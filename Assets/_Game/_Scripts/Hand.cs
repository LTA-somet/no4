using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animation animHand;
    void Start()
    {
        animHand=GetComponent<Animation>();
        
    }

    public void PlayAnim()
    {      
        animHand.Play("break");
    }
}
