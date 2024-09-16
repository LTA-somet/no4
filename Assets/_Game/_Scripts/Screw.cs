using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Screw : MonoBehaviour
{

    [SerializeField] private Animation animScrew;
    [SerializeField] private SpriteRenderer spriteScrew;

    public Vector2 curPos { get; private set; }
    public List<PointObject> listPointInScrew;
  

    public CircleCollider2D colliderScrewRaycast;
    public Hole curHole;

    private RaycastHit2D hit;

    private string curLayer;

    private bool isCheckHoldWood = false;


    void Start()
    {     
        animScrew = GetComponent<Animation>();
        curLayer = LayerMask.LayerToName(gameObject.layer);
        curPos = transform.position;
        listPointInScrew = curHole.GetListPointInHole();  
    }
   private void DelayDisHole()
    {
        curHole.transform.DOScale(Vector2.zero, 0.4f);
    }
    public void SetAnim(bool isUp = false)
    {
        if (isUp)
            animScrew.Play("ScrewUp2");
        else
            animScrew.Play("ScrewDown2");
    }
    public void IsChoose()
    {
        if (spriteScrew != null)
            spriteScrew.sortingOrder = 10;
    }
    public void EndChoose()
    {
        if (spriteScrew != null)
            spriteScrew.sortingOrder = 0;
    }
    public void ChangePosition(Vector2 pos, bool isChangePos = false)
    {
        if (isChangePos)
        {
            float timeMove = Vector2.Distance(transform.position, pos) / 15;
            transform.DOMove(new Vector3(pos.x, pos.y, transform.position.z), timeMove).SetEase(Ease.Linear).OnComplete(() =>
            {
                SetAnim(false);
            });
        }
        else
            SetAnim(false);
    }

    public void SetCurHole(Hole hole)
    {
        curHole = hole;
    }

    public void SetCurPos(Vector2 pos)
    {
        curPos = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void EnableCollider()
    {
       
        colliderScrewRaycast.enabled = true;
    }
    public void DisableCollider()
    {
        
        colliderScrewRaycast.enabled = false;
    }
    public void ChangeConnectRigidHingeJoint(List<PointObject> newPoints) // goi khi Input.GetmousebuttonUp
    {
        foreach (var point in newPoints)
        {
            point.ChangeActiveHingeJoint(true);
        }

        foreach (var point in listPointInScrew)
        {
            point.ChangeActiveHingeJoint(false);
        }
      
        if (listPointInScrew.Count > 0)
            isCheckHoldWood = true;
        listPointInScrew = newPoints;
        if (listPointInScrew.Count <= 0 && isCheckHoldWood)
        {
          
            isCheckHoldWood = false;
        }
        else if (listPointInScrew.Count > 0 && !isCheckHoldWood) { }
           
    }
    private void DelayMoveIntro()
    {
        Vector3 newPos = new Vector3(curHole.transform.position.x, curHole.transform.position.y, transform.position.z);
        transform.DOMove(newPos, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetAnim(false);
        });
    }

}