using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour
{
    public int indexHoleInObj = 0;
    public HingeJoint2D hingeJointObject;

    public Block obj;

    private void Start()
    {
        hingeJointObject = obj.GetListHingeJoint2D(indexHoleInObj);
    }

    public void ChangeActiveHingeJoint(bool isEnable)
    {
        hingeJointObject.enabled = isEnable;
        if (isEnable)
        {
            obj.UpdateCountHingeJointDis(-1);
        }
        else
        {
            obj.UpdateCountHingeJointDis(1);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
