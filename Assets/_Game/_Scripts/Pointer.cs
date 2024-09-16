using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Animation animPoiter;
    void Start()
    {
        animPoiter = GetComponent<Animation>();   
       
    }

   public void  PlayAnim(string str)
    {      
        animPoiter.Play(str);
    }
    
}
