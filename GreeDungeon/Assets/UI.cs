using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;
    
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        Instance = this;
    }


    public void SetLifeBar(float maxLife)
    {
        healthBar.maxValue = maxLife;
        healthBar.value = maxLife;
        Debug.Log($"Set life to {healthBar.value}");
    }

    public void RemoveLife(float currentlife)
    {
        healthBar.value = currentlife;
    }
}
