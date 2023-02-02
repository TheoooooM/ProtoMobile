using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SideAction(Enums.Side side);
public delegate void PositionAction(Vector2 side);

public class Inputs : MonoBehaviour
{
    public static Inputs Instance;
    [SerializeField] private float minimumSwipeDistance = .2f;
    private Vector2 touchStartPos;
    public event SideAction OnSwip = delegate{ };
    public event PositionAction OnTouch = delegate{ };
    public event PositionAction OnRelease = delegate{ };

    void Awake() => Instance = this;
    void Update()=>InputCheck();
    

    void InputCheck()
    {
        if (Input.touches.Length == 0) return;
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            touchStartPos = Input.touches[0].position;
            OnTouch?.Invoke(touchStartPos);
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            var touchEndPos = Input.touches[0].position;
            OnRelease?.Invoke(touchEndPos);
            TrySwipe(touchStartPos,touchEndPos);
        }
    }

    void TrySwipe(Vector2 startPos, Vector2 endPos)
    {
        var dir = endPos - startPos;
        if(Vector2.Distance(startPos, endPos)>= minimumSwipeDistance) 
            OnSwip?.Invoke(Mathf.Abs(dir.x) > Mathf.Abs(dir.y)? dir.x >= 0 ? Enums.Side.right : Enums.Side.left : dir.y >= 0 ? Enums.Side.up : Enums.Side.down);
    }
}
