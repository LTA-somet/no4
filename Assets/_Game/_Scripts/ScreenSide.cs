using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSide : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject destroyer;
 
    void Start()
    {
        mainCamera.DOOrthoSize(2.5f, 1.7f);
        StartCoroutine(ActiveDestroyer());
    }

private IEnumerator ActiveDestroyer()
    {
        
        yield return new WaitForSeconds(1.5f);
        destroyer.SetActive(true);
    }
}
