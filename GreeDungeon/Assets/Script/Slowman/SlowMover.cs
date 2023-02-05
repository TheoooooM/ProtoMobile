using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMover : MonoBehaviour
{
    [SerializeField] private float deathZone = 10f;
    [SerializeField] private float maxZone = 100f;
    private Vector2 startPos;
     private Vector2 lastPos;
     private float magnitude;
    void Start()
    {
        Inputs.Instance.OnTouch += pos => startPos = lastPos = pos;
        Inputs.Instance.OnMove += MoveUpdate;
        Inputs.Instance.OnRelease += _ => {startPos = lastPos = Vector2.zero; magnitude = 0;};
        
    }

    private void Update()
    {
        if (magnitude > deathZone) Move();
    }

    void MoveUpdate(Vector2 pos)
    {
        lastPos = pos;
        magnitude = Mathf.Clamp((lastPos-startPos).magnitude,0f,100f);
        
    }

    void Move()
    {
        Debug.Log("Move");
    }
}
