using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public Animator crossFade;
    public Animator swipe;
    
    private bool isStart;
    private static readonly int Start1 = Animator.StringToHash("Start");
    public bool IsStart => isStart;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void OnClickStart()
    {
        isStart = true;
        crossFade.SetTrigger(Start1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    
    private void StartGame()
    {
        StartCoroutine(WaitToStart());
    }
    
    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(.4f);
        LevelLoader.Instance.LoadLevel(1);
    }
}
