using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class QTE : MonoBehaviour
{
    public int totalQte = 3;
    private int _currentQte = 0;

    public float timeLimit = 5f;
    private float _currentTime = 0f;

    private int[] _qteSequence;

    private SwipeDetector _swipeDetector;

    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        _swipeDetector = GetComponent<SwipeDetector>();
        _swipeDetector.onSwipe += HandleSwipe;

        StartQte();
    }
    
    

    void Update()
    {
    
    
    
        _currentTime += Time.deltaTime;
        if (_currentTime >= timeLimit)
        {
            EndQte();
        }

        if (_currentQte < totalQte)
        {
            switch (_qteSequence[_currentQte])
            {
                case 0:
                    text.text = "Swipe Up";
                    break;
                case 1:
                    text.text = "Swipe Down";
                    break;
                case 2:
                    text.text = "Swipe Left";
                    break;
                case 3:
                    text.text = "Swipe Right"; 
                    break;
            }
        }
    }

    void HandleSwipe(SwipeData swipeData)
    {
        switch (swipeData.direction)
        {
            case SwipeDirection.Up:
                OnSwipeUp();
                break;
            case SwipeDirection.Down:
                OnSwipeDown();
                break;
            case SwipeDirection.Left:
                OnSwipeLeft();
                break;
            case SwipeDirection.Right:
                OnSwipeRight();
                break;
        }
    }

    void StartQte()
    {
        _currentQte = 0;
        _currentTime = 0f;
        GenerateQteSequence();
    }

    void GenerateQteSequence()
    {
        _qteSequence = new int[totalQte];
        for (int i = 0; i < totalQte; i++)
        {
            _qteSequence[i] = Random.Range(0, 4);
        }
    }

    void EndQte()
    {
        Debug.Log("QTE Over");
    }

    void OnSwipeUp()
    {
        if (_qteSequence[_currentQte] == 0)
        {
            _currentQte++;
            _currentTime = 0f;
            Debug.Log("Correct Swipe");
        }
        else
        {
            EndQte();
        }
    }

    void OnSwipeDown()
    {
        if (_qteSequence[_currentQte] == 1)
        {
            _currentQte++;
            _currentTime = 0f;
            Debug.Log("Correct Swipe");
        }
        else
        {
            EndQte();
        }
    }

    void OnSwipeLeft()
    {
        if (_qteSequence[_currentQte] == 2)
        {
            _currentQte++;
            _currentTime = 0f;
            Debug.Log("Correct Swipe");
        }
       
        else
        {
            EndQte();
        }
    }
    
    void OnSwipeRight()
    {
        if (_qteSequence[_currentQte] == 3)
        {
            _currentQte++;
            _currentTime = 0f;
            Debug.Log("Correct Swipe");
        }
        else
        {
            EndQte();
        }
    }
}