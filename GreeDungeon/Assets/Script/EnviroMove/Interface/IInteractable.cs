using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Select();
    void Deselect();
    void Swipe(Enums.Side side);
}
