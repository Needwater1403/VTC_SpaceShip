using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Title("Player")]
    [SerializeField] private SpaceShipControl player;
    [Title("Game Settings")]
    [SerializeField] private float gameLength;
    private float timer = 1;
    public SpaceShipControl Player => player;
    private int point;
    public int Point => point;

    private bool gameOver;
    public bool GameOver
    {
        get => gameOver;
        set => gameOver = value;
    }

    private bool isFinished;
    public bool IsFinished => isFinished;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(isFinished) return;
        if (timer > 0)
        {
            timer = gameLength - Time.timeSinceLevelLoad;
            UIManager.Instance.UpdateTimerUI(timer, gameLength);
        }
        else
        {
            timer = 0;
            gameOver = true;
        }
        
        if (player.Visual == null)
        {
            isFinished = true;
            gameOver = true;
        }

        if (point == 10)
        { 
            isFinished = true;
            UIManager.Instance.ShowWinPanel(true);
        }
        if (!gameOver) return;
        isFinished = true;
        UIManager.Instance.ShowLosePanel(true);
    }

    public void AddPoint(int _amount)
    {
        point += _amount;
    }
}
