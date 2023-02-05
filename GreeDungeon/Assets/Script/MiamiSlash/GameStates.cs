using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    public static GameStates Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Inputs.Instance.OnTouch += side => gameStarted = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool gameStarted;
}
