using UnityEngine;

public delegate void SwipeAction(SwipeData swipeData);

public class SwipeDetector : MonoBehaviour
{
    public float minDistanceForSwipe = 20f;
    public event SwipeAction onSwipe = delegate { };

    private Vector2 _startPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPosition = Input.mousePosition;
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
                    Debug.Log("Swip Up");
                }
                else if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Down
                    });
                    Debug.Log("Swip Down");
                }
                else if (swipeVector.x < 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Left
                    });
                    Debug.Log("Swip Left");
                }
                else if (swipeVector.x > 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f)
                {
                    onSwipe(new SwipeData()
                    {
                        startPosition = _startPosition,
                        endPosition = endPosition,
                        direction = SwipeDirection.Right
                    });
                    Debug.Log("Swip Right");
                }
            }
        }
    }
}
