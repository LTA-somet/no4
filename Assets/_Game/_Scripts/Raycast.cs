using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Destroyer destroyer;
    public Destroyer Destroyer {  get { return destroyer; } }
    public string maskRaycastScrew = "", maskRaycastHole = "";
    private RaycastHit2D[] hits;
    private Vector3 mousePosition, oldMousePosition;
    private Screw curScrew = null;
    private Hole curHole = null;
    private bool isMoveScrew = false;
    private bool isChooseScrew = false;
    public bool isEnd = false;
    private int check=0;
    [SerializeField] private GameObject anim1;
    [SerializeField] private GameObject anim2;
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;  
    [SerializeField] private GameObject guide;  
    
   
   
    private int pointCount = 0;
    private void Start()
    {
        StartCoroutine(StartPlay());
    }
    void Update()
    {
        if (Destroyer.blockCount >= 10)
        {
            isEnd = true;
            return;
             }
        
        if (Input.GetMouseButtonDown(0)) // check hole
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hits = Physics2D.RaycastAll(mousePosition, Vector3.zero, 50);
            if (pointCount == 0)
            {
                point1.gameObject.SetActive(true);
                pointCount++;
                guide.gameObject.SetActive(false);
            }
          
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer(maskRaycastHole)))                  
                {
                    Debug.Log("hole was choosen");
                    curHole = hit.transform.GetComponent<Hole>();
                    anim2.SetActive(false);
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

                if (pointCount == 1)
                {
                    point2.transform.position = curHole.transform.position;
                    point2.SetActive(true);
                    pointCount++;
                }
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
                if (check == 0)
                {
                    anim1.SetActive(false);
                    anim2.SetActive(true);
                    check++;
                }
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


                // Hiển thị point1 tại vị trí của ốc đang được chọn
                point1.transform.position = curScrew.transform.position;
                point1.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void EndChooseScrew()
    {
        curScrew.EndChoose();
       
        anim2.SetActive(false);
        isChooseScrew = false;
        curScrew.EnableCollider();
        curScrew = null;
        curHole = null;
        isMoveScrew = false;
        oldMousePosition = Vector3.zero;
    }
    private IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(3f);
        anim1.SetActive(true);
        guide.SetActive(true);
    }
}
