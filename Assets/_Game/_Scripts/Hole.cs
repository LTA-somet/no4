using System;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2D;
    public bool isHasScrew = false;
    public List<PointObject> listPointObjectInHole;
    public List<PointObject> listPointObjectInHoleKill;
    private RaycastHit2D hit2D;
    public bool isLock = false;
    void Start()
    {
        listPointObjectInHoleKill = new List<PointObject>();
        if (!isHasScrew && !isLock)
        {
            circleCollider2D.isTrigger = true;          
        }
    }

    private void Update()
    {
        UpdateLock();
    }
 
    public void AddPointObj(PointObject newPointObject)
    {
        listPointObjectInHole.Add(newPointObject);
    }

    public void UpdateIsHasScrew(bool isHasScrew)
    {
        this.isHasScrew = isHasScrew;
        circleCollider2D.isTrigger = !isHasScrew;
    }

    public void UpdateLock()
    {
        if (isHasScrew) return;
        hit2D = Physics2D.CircleCast(transform.position, 0.1f, transform.forward, 100, LayerMask.GetMask("Bolt"));
        if (hit2D.collider != null)
        {
            if (!isLock)
               
            isLock = true;
        }
        else
        {
            if (isLock)
            
            isLock = false;
        }
    }

    public void CheckPosListPoint()
    {
        for (int i = 0; i < listPointObjectInHole.Count; i++)
        {
            if (DistancePoint(listPointObjectInHole[i].transform.position))
            {
                listPointObjectInHoleKill.Add(listPointObjectInHole[i]);
                listPointObjectInHole.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < listPointObjectInHoleKill.Count; i++)
        {
            if (!DistancePoint(listPointObjectInHoleKill[i].transform.position))
            {
                listPointObjectInHole.Add(listPointObjectInHoleKill[i]);
                listPointObjectInHoleKill.RemoveAt(i);
                i--;
            }
        }
    }


    private bool DistancePoint(Vector2 pos)
    {
        return Vector2.Distance(transform.position, pos) >0.1f;
    }

    public Vector2 GetPosition() => transform.position;

    public List<PointObject> GetListPointInHole() => listPointObjectInHole;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bolt")) { }
            
    }
}