using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Active());
    }

  private IEnumerator Active()
    {
        yield return  new WaitForSeconds(5f);
        this.gameObject.SetActive(true);
    }
}
