using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI displayBox;
    public TextMeshProUGUI passBox;
    public int qTEGen;
    public int waitingForKey;
    public int correctkey;
    public int countingDown;
    
    public float timeToPress;
    public float timeToRead;
    
    private bool isCoroutineExecuting = false;
    private bool isCoroutineExecuting2 = false;

    private void Update()
    {
       
    }

    public void LaunchQTE()
    {
         if (waitingForKey == 0)
        {
            qTEGen = Random.Range(1, 7);
            countingDown = 1;
            StartCoroutine(CountingDown());
            if (qTEGen == 1)
            {
                displayBox.text = "Tap the screen";
                waitingForKey = 1;
            }
            else if (qTEGen == 2)
            {
                displayBox.text = "Swipe left";
                waitingForKey = 1;
            }
            else if (qTEGen == 3)
            {
                displayBox.text = "Swipe right";
                waitingForKey = 1;
            }
            else if (qTEGen == 4)
            {
                displayBox.text = "Swipe up";
                waitingForKey = 1;
            }
            else if (qTEGen == 5)
            {
                displayBox.text = "Swipe down";
                waitingForKey = 1;
            }
            else if (qTEGen == 6)
            {
                displayBox.text = "Hold the screen";
                waitingForKey = 1;
            }
            
        }
         if (Input.touchCount > 0)
        {
            if (qTEGen == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    correctkey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctkey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
            else if (qTEGen == 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.x < 0)
                    {
                        correctkey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctkey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            else if (qTEGen == 3)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.x > 0)
                    {
                        correctkey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctkey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            else if (qTEGen == 4)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.y > 0)
                    {
                        correctkey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctkey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            else if (qTEGen == 5)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.y < 0)
                    {
                        correctkey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctkey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            else if (qTEGen == 6)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(0).deltaTime > 1)
                    {
                        correctkey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctkey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
        }
    }

    private IEnumerator CountingDown()
    {
        if (isCoroutineExecuting2)
        {
            yield break;
        }
        Debug.Log("Counting down");
        isCoroutineExecuting2 = true;
        yield return new WaitForSeconds(timeToPress);
        if (countingDown == 1)
        {
            countingDown = 2;
            passBox.text = "Fail";
            yield return new WaitForSeconds(timeToRead);
            correctkey = 0;
            passBox.text = "";
            displayBox.text = "";
            yield return new WaitForSeconds(timeToRead);
            waitingForKey = 0;
            countingDown = 1;
        }
        Debug.Log("Counting down finished");
        isCoroutineExecuting2 = false;
    }

    private IEnumerator KeyPressing()
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }
        Debug.Log("Key Pressing");
        isCoroutineExecuting = true;
        qTEGen = 7;
        if (correctkey == 1)
        {
            countingDown = 2;
            passBox.text = "Pass";
            yield return new WaitForSeconds(timeToRead);
            correctkey = 0;
            passBox.text = "";
            displayBox.text = "";
            yield return new WaitForSeconds(timeToRead);
            waitingForKey = 0;
            countingDown = 1;
        }

        if (correctkey == 2)
        {
            countingDown = 2;
            passBox.text = "Fail";
            yield return new WaitForSeconds(timeToRead);
            correctkey = 0;
            passBox.text = "";
            displayBox.text = "";
            yield return new WaitForSeconds(timeToRead);
            waitingForKey = 0;
            countingDown = 1;
        }
        Debug.Log("Key Pressing finished");
        isCoroutineExecuting = false;
    }
}