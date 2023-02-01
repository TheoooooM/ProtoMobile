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
    
    [SerializeField] private TextMeshProUGUI _text;

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
        
        if (_currentQte >= totalQte)
        {
            EndQte();
        }

        if (_currentQte < totalQte)
        {
            switch (_qteSequence[_currentQte])
            {
                case 0:
                    _text.text = "↑";
                    break;
                case 1:
                    _text.text = "↓";
                    break;
                case 2:
                    _text.text = "←";
                    break;
                case 3:
                    _text.text = "→";
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
        _text.text = "QTE Ended";
    }

    void OnSwipeUp()
    {
        if (_qteSequence.Length <= 0) return;
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
        if (_qteSequence.Length <= 0) return;
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
        if (_qteSequence.Length <= 0) return;
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
        if (_qteSequence.Length <= 0) return;
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