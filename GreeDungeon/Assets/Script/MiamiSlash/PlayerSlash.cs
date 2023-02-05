using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public delegate void BasicDelegate();

public class PlayerSlash : MonoBehaviour
{
    public static PlayerSlash Instance;
    private CapsuleCollider collider;
    private MeshRenderer mesh;
    private Color startColor;
    
    [SerializeField] private float speed = 10f;
    [Space]
    [SerializeField] private float obstructedTime = 1f;
    [Space]
    [SerializeField] private float attackRadius = 1f;
    
    private Vector3 movePos;
    BasicDelegate OnFinishMove = delegate {  };

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<CapsuleCollider>();
        
        Inputs.Instance.OnTouch += CalculateMove;
        OnFinishMove += Slash;
        startColor = mesh.material.color;
    }

    void Update() => Move();

    private void Move()
    {
        if (movePos != Vector3.zero)
        { 
            var dir = movePos - transform.position;
            var moveForce = speed * Time.deltaTime; 
            
            if (dir.magnitude > moveForce) transform.position += dir * moveForce;
            else
            {
                transform.position = movePos;
                movePos = Vector3.zero;
                OnFinishMove?.Invoke();
            }
        }
    }

    void CalculateMove(Vector2 clickPosition)
    {
        var ray = Camera.main.ScreenPointToRay(clickPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var movePoint = hit.point;
            movePoint.y = transform.position.y;
            Debug.DrawRay(movePoint, Vector3.up, Color.red, 5f);
            movePos = movePoint;
            var dir = movePoint - transform.position;
            RaycastHit[] collideObject = Physics.SphereCastAll(transform.position, collider.radius, dir, dir.magnitude);
            foreach (var collide in collideObject)
            {
                switch (collide.transform.tag)
                {
                    case "Obstacle":
                        collide.transform.gameObject.SetActive(false);
                        movePos = collide.point;
                        OnFinishMove += LaunchObstruction;
                        break;
                    case "Wall" :
                        movePos = collide.point + collide.normal*collider.radius;
                        break;
                }
                
            }
            //Debug.Break();
        }
    }

    void Slash()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                col.gameObject.SetActive(false);
            }
        }
    }
    
    void LaunchObstruction()=> StartCoroutine(GetObstructedBozo());

    IEnumerator GetObstructedBozo()
    {
        OnFinishMove -= LaunchObstruction;
        movePos = Vector3.zero;
        mesh.material.color = Color.red;
        Inputs.Instance.OnTouch -= CalculateMove;
        
        yield return new WaitForSeconds(obstructedTime);
        
        mesh.material.color = startColor;
        Inputs.Instance.OnTouch += CalculateMove;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePos, .2f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
