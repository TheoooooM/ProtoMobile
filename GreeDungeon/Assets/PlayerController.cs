using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class PlayerController : MonoBehaviour
{
    
    
    private bool swiping;
    private Vector2 swipStartPos;

    [SerializeField] private float moveSpeed = .1f;
    [SerializeField] private AnimationCurve moveYCurve;
    [SerializeField] private Transform animationTransform;
    [Space] 
    [SerializeField] private Grid playGrid;
    private Vector2 gridPos;
    [Space]
    [SerializeField]float maxLife;
    float life;

    private void Start()
    {
        life = maxLife;
        UI.Instance.SetLifeBar(life);
    }

    void Update()
    {
        if (Input.touches.Length == 0) return;
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            swipStartPos = Input.touches[0].position;
        }
        if (Input.touches[0].phase == TouchPhase.Ended) Swip(swipStartPos,Input.touches[0].position);
    }

    void Swip(Vector2 startPos, Vector2 endPos)
    {
        var dir = endPos - startPos;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x>=0)
            {
                var neighbor = playGrid.GetNeighboor(gridPos, Enums.Side.right);
                if (neighbor != null)
                {
                    Move(neighbor.worldPos);
                    
                    gridPos += Vector2.right;
                }
            }
            else
            {
                var neighbor = playGrid.GetNeighboor(gridPos, Enums.Side.left);
                if (neighbor != null)
                {
                    Move(neighbor.worldPos);
                    gridPos += Vector2.left;
                }
            }
        }
        else
        {
            if (dir.y >= 0)
            {
                var neighbor = playGrid.GetNeighboor(gridPos, Enums.Side.up);
                if (neighbor != null)
                {
                    Move(neighbor.worldPos);
                    gridPos += Vector2.up;
                }
            }
            else 
            {
                var neighbor = playGrid.GetNeighboor(gridPos, Enums.Side.down);
                if (neighbor != null)
                {
                    Move(neighbor.worldPos);
                    gridPos += Vector2.down;
                }
            }
        }
    }

    async void Move(Vector3 pos)
    {
        
        var startPos = transform.position;
        float progress = 0;
        while (progress<1)
        {
            progress += moveSpeed;
            transform.position = Vector3.Lerp(startPos, pos, progress);
            animationTransform.position = transform.position + new Vector3(0, moveYCurve.Evaluate(progress), 0);
            await Task.Delay(10);
        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        UI.Instance.RemoveLife(life);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
