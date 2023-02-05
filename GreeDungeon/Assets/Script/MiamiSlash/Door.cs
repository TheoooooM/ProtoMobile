using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool goLeft = true;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform leftPos;


    void ChangeRoom()
    {
        Debug.Log("Change Room");
        if (goLeft)
        {
            Camera.main.transform.DOMove(Camera.main.transform.position + Vector3.left * 20.3f, 2f).OnComplete(() =>
            {
                PlayerSlash.Instance.transform.position = leftPos.position;
            });
            goLeft = false;
        }
        else
        {
            Camera.main.transform.DOMove(Camera.main.transform.position + Vector3.right * 20.3f, 1f).OnComplete(() =>
            {
                PlayerSlash.Instance.transform.position = rightPos.position;
            });
            goLeft = true;
        }
    }
    
    public void Select()
    {
        ChangeRoom();
    }

    public void Deselect()
    {
        //throw new System.NotImplementedException();
    }

    public void Swipe(Enums.Side side)
    {
        //throw new System.NotImplementedException();
    }
}
