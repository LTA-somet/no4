using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnScaleScript : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
        if (parentSpriteRenderer != null)
        {
            Vector2 parentsize = parentSpriteRenderer.bounds.size;
            foreach (Transform child in transform)
            {
                SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                if ((childSpriteRenderer != null))
                {
                    Vector2 childSize = childSpriteRenderer.bounds.size;

                    Vector3 newSacle = new Vector3(

                        parentsize.x / childSize.x,
                        parentsize.y / childSize.y,
                        1);
                    child.localScale = newSacle;
                }
            }
        }


    }
    


}


