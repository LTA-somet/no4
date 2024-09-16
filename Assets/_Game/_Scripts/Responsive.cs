using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Responsive : MonoBehaviour
{
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject title;
  
    bool isHorizontal = false;
    bool isVertical = false;
    
    Vector3 curPos = Vector3.zero;
    private void Start()
    {
       
        curPos = title.transform.position;
        if (Screen.width > Screen.height && !isHorizontal) //=>Chieu ngang
        {
            isHorizontal = true;
            isVertical = false;
           // ReturnHorizontal();
        }
        else if (Screen.width < Screen.height && !isVertical)
        {
         
            isHorizontal = false;
            isVertical = true;
        }
    }
   
    void Update()
    {
        if(Screen.width > Screen.height && !isHorizontal) //=>Chieu ngang
        {
            //Chuyen man ngang
          ReturnHorizontal();   
            isHorizontal = true;
            isVertical = false;
        }
        else if(Screen.width < Screen.height && !isVertical)
        {
            //Chuyen man doc
            ReturnVertical();   
            isHorizontal = false;
            isVertical = true;
        }
    }
    private void ReturnHorizontal()
    {
        image.SetActive(true);
        gameBoard.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        gameBoard.transform.position += new Vector3(-4, 0, 0);
        title.transform.position += new Vector3(8, -9, 0);
        title.transform.localScale = new Vector3(2, 2, 2);
    } 
    private void ReturnVertical()
    {
        image.SetActive(false);
        gameBoard.transform.localScale = Vector3.one;
        gameBoard.transform.position -= new Vector3(-4, 0, 0);
        title.transform.position = curPos;
        title.transform.localScale = Vector3.one;
    }
}
