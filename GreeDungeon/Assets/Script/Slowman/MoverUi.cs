using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverUi : MonoBehaviour
{
    private RectTransform canva;
    [SerializeField] private RectTransform handler;
    [SerializeField] private RectTransform mover;

    private void Start()
    {
        Inputs.Instance.OnTouch += SetHandler;
        canva = GetComponent<RectTransform>();
    }

    void SetHandler(Vector2 position)
    {
        Debug.Log("Move to " + position);
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canva, position, Camera.main, out canvasPos);
        //Handler.localPosition = Camera.main.ScreenToViewportPoint(position);
        handler.anchoredPosition = position;
    }
    
}
