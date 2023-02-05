using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Mathf.Cos(Time.time)*10, 0, Mathf.Sin(Time.time)*10);
    }
}
