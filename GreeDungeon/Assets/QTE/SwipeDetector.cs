using UnityEngine;
using UnityEngine.Serialization;

public delegate void SwipeAction(SwipeData swipeData);

public class SwipeDetector : MonoBehaviour
{
    public float minDistanceForSwipe = 20f;
    public event SwipeAction onSwipe = delegate { };

    private Vector2 _startPosition;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startPosition = Input.GetTouch(0).position;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 endPosition = Input.GetTouch(0).position;
            Vector2 swipeVector = endPosition - _startPosition;

            float swipeDistance = Vector2.Distance(_startPosition, endPosition);

            if (swipeDistance >= minDistanceForSwipe)
            {
                swipeVector.Normalize();

                if (swipeVector.y > 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Up
                    });
                }
                else if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Down
                    });
                }
                else if (swipeVector.x < 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Left
                    });
                }
                else if (swipeVector.x > 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Right
                    });
                }
            }
        }
    }
}
