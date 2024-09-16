using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSide : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject destroyer;
   [SerializeField] private GameObject darker;
    [SerializeField] private GameObject retryBtn;

    [SerializeField] private Raycast raycast;
  [SerializeField]  private SpriteRenderer darkerRenderer;
   [SerializeField] private Color dark = new Color(70, 70, 70);
   [SerializeField] private Color old = new Color(255, 255, 255);

    void Start()
    {
        StartCoroutine(CameraMove());
        StartCoroutine(DestroyerMove());
    }
    private void Update()
    {
        if (raycast.isEnd)
        {
            StartCoroutine(ReturnPos());
        }
    }


    private void FadeToDarker(Color color)
    {
        
        if (darkerRenderer != null)
        {           
            darkerRenderer.DOColor(color, 2f); 
        }
    }

    private IEnumerator CameraMove()
    {
        yield return new WaitForSeconds(1.5f);
        mainCamera.DOOrthoSize(1.8f, 2f);
        mainCamera.transform.DOMove(new Vector3(0, -2.2f, -10), 1.5f);
        yield return new WaitForSeconds(2f);
        FadeToDarker(dark);
        yield return new WaitForSeconds(5f);
        retryBtn.gameObject.SetActive(true);
    }
    private IEnumerator ReturnPos()
    {
        yield return new WaitForSeconds(1.5f);
        FadeToDarker(old); 
        yield return new WaitForSeconds(1.5f);
        mainCamera.DOOrthoSize(5f, 2f);
        mainCamera.transform.DOMove(new Vector3(0, 0f, -10), 1.5f);
        destroyer.gameObject.transform.DOMove(new Vector3(0, -4.5f, 0), 1.5f);
    }
    private IEnumerator DestroyerMove()
    {
        yield return new WaitForSeconds(2.2f);
        destroyer.gameObject.transform.DOMove(new Vector3(0, -3.7f, 0), 1.5f).SetEase(Ease.OutBack);
    }
}
