using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] Vector2[] checkpoints;
    [SerializeField]private LevelConstructor levelReference;
    [Space]
    [SerializeField] private float speed;
    private Queue<Vector3> checkpointPositions = new();
    private Vector3 currentDestinaiton;
    [SerializeField] private bool DebugRayState;


    private void Start()
    {
        if (checkpoints.Length == 0) throw new NullReferenceException("CheckPoints isn't Set");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpointPositions.Enqueue(levelReference.GetPosition(checkpoints[i]).GetWorldPosition()+Vector3.up*.75f);
        }
        transform.position = checkpointPositions.Dequeue();
        Debug.Log($"Set Pos to {transform.position}");
        currentDestinaiton = GetNextPosition();
    }


    private void Update()
    {
        var dir = currentDestinaiton - transform.position;
        if (Vector3.Distance(transform.position, currentDestinaiton)>speed*Time.deltaTime)
        {
            transform.position += dir.normalized*speed*Time.deltaTime;
        }
        else currentDestinaiton = GetNextPosition();

        if (!isGrounded())
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }

    Vector3 GetNextPosition()
    {
        if (checkpointPositions.Count == 0)
        {
            Debug.Log("Finish Puzzle");
            return default;
        }
        var pos = checkpointPositions.Dequeue() + Vector3.up*.25f;
        Debug.Log($"New Destination {pos}, Remaining {checkpointPositions.Count}");
        return pos;
    }

    bool isGrounded()
    {
        Ray[] checkRays = new Ray[5];
        checkRays[0] = new Ray(transform.position, Vector3.down);
        checkRays[1] = new Ray(transform.position + Vector3.left*    transform.localScale.x/2, Vector3.down);
        checkRays[2] = new Ray(transform.position + Vector3.right*   transform.localScale.x/2, Vector3.down);
        checkRays[3] = new Ray(transform.position + Vector3.forward* transform.localScale.z/2, Vector3.down);
        checkRays[4] = new Ray(transform.position + Vector3.back*    transform.localScale.z/2, Vector3.down);
        foreach (var ray in checkRays)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 10))
            {
                if(DebugRayState)Debug.Log($"Ray hit {hit.transform.name} with tag {hit.transform.tag}");
                if (!hit.transform.CompareTag("Void")) return true;
            }
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Ray[] checkRays = new Ray[5];
        checkRays[0] = new Ray(transform.position, Vector3.down);
        checkRays[1] = new Ray(transform.position + Vector3.left*    transform.localScale.x/2, Vector3.down);
        checkRays[2] = new Ray(transform.position + Vector3.right*   transform.localScale.x/2, Vector3.down);
        checkRays[3] = new Ray(transform.position + Vector3.forward* transform.localScale.z/2, Vector3.down);
        checkRays[4] = new Ray(transform.position + Vector3.back*    transform.localScale.z/2, Vector3.down);
        foreach (var ray in checkRays)
        {
            Gizmos.DrawRay(ray);
        }
    }
}
