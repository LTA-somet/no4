using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<HingeJoint2D> hingeJoint2Ds = new List<HingeJoint2D>();
    private Rigidbody2D rigid2D;
    private int countHingeJointDis = 0;
    private PolygonCollider2D polygonCollider2D;
    private BoxCollider2D boxCollider2D;
    private bool isRoi = false;
    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.isKinematic = true;
    }

    private void Update()
    {
        if (isRoi && transform.position.y - Camera.main.transform.position.y <= -14)
        {
            isRoi = false;
            gameObject.SetActive(false);
        }
    }
    public HingeJoint2D GetListHingeJoint2D(int index) => hingeJoint2Ds[index];

    public void UpdateCountHingeJointDis(int index)
    {
        countHingeJointDis += index;
        if (countHingeJointDis >= hingeJoint2Ds.Count - 1)
        {
            rigid2D.isKinematic = false;
            polygonCollider2D.enabled = true;
        }
        else
            rigid2D.isKinematic = true;
        if (countHingeJointDis >= hingeJoint2Ds.Count)
        {
            if (boxCollider2D != null)
            {
                boxCollider2D.enabled = true;
                polygonCollider2D.enabled = false;
            }
            isRoi = true;
        }
    }
}
