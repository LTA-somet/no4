using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public string maskRaycastScrew = "", maskRaycastHole = "";
    private RaycastHit2D[] hits;
    private Vector3 mousePosition, oldMousePosition;
    private Screw curScrew = null;
    private Hole curHole = null;
    private bool isMoveScrew = false;
    private bool isChooseScrew = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // check hole
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hits = Physics2D.RaycastAll(mousePosition, Vector3.zero, 50);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer(maskRaycastHole)))                  
                {
                    Debug.Log("hole was choosen");
                    curHole = hit.transform.GetComponent<Hole>();
                    break;
                }
                else
                {
                    curHole = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !isChooseScrew)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hits = Physics2D.RaycastAll(mousePosition, Vector3.zero, 50);
            ChooseScrew();
        }

        if (Input.GetMouseButtonUp(0) && !isChooseScrew && curScrew != null)
            isChooseScrew = true;
        else if (isChooseScrew && Input.GetMouseButtonUp(0))
            isChooseScrew = false;

        if (Input.GetMouseButtonDown(0) && !isMoveScrew && isChooseScrew && curScrew != null)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hits = Physics2D.RaycastAll(mousePosition, Vector3.zero, 50);
            StopChooseScrew();
            ChooseScrew();
        }
    }

    private void StopChooseScrew()
    {
        if (curHole != null)
        {
            if (!curHole.isHasScrew) //&& !curHole.isLock)
            {
                curScrew.ChangePosition(curHole.GetPosition(), true);
                curScrew.SetCurPos(curHole.transform.position);
                curHole.CheckPosListPoint();
                curScrew.ChangeConnectRigidHingeJoint(curHole.listPointObjectInHole);
                curScrew.curHole.UpdateIsHasScrew(false);
                curScrew.SetCurHole(curHole);
                curHole.UpdateIsHasScrew(true);
                EndChooseScrew();
            }
            else
            {
                curScrew.EnableCollider();
                curScrew.ChangePosition(curScrew.curHole.GetPosition());
                EndChooseScrew();
            }
        }
        else
        {
            curScrew.EnableCollider();
            curScrew.ChangePosition(curScrew.curHole.GetPosition());
            EndChooseScrew();
        }
    }

    private void ChooseScrew()
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer(maskRaycastScrew)))
            {
                isMoveScrew = false;
                if (curScrew != null && hit.collider.gameObject != curScrew)
                {
                    EndChooseScrew();
                }
                oldMousePosition = mousePosition;
                curScrew = hit.transform.GetComponent<Screw>();
                curScrew.DisableCollider();
                curScrew.SetAnim(true);
                curScrew.IsChoose();
               
                break;
            }
        }
    }
    public void EndChooseScrew()
    {
        curScrew.EndChoose();
        isChooseScrew = false;
        curScrew.EnableCollider();
        curScrew = null;
        curHole = null;
        isMoveScrew = false;
        oldMousePosition = Vector3.zero;
    }
}
