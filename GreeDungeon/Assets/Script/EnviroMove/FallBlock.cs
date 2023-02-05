using System;
using System.Collections;
using System.Collections.Generic;
using Script.EnviroMove;
using UnityEngine;

public class FallBlock : Block, IInteractable
{
    private Vector3 startScale;

    private void Start() => startScale = transform.localScale;

    public void Select() => transform.localScale = startScale * 1.2f;
    public void Deselect() => transform.localScale = startScale;

    public void Swipe(Enums.Side side)
    {
        throw new System.NotImplementedException();
    }
}
