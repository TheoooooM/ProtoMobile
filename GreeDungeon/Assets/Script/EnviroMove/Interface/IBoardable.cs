using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardable
{
    void SetMaster(LevelConstructor gridMaster, Vector2 pos); //TODO : Unifier les Grids
    void SetPosition(Vector2 newPos);
    Vector3 GetWorldPosition();

    bool isBlockOnTop();
    void SetTopState(bool state);
}
