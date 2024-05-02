using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    private Animator transition;
    public float transitionDuration = 1;
    private static readonly int Start1 = Animator.StringToHash("Start");

    private void Start()
    {
        Time.timeScale = 1;
        transition = GetComponentInChildren<Animator>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(Load(index));
    }

    private IEnumerator Load(int _index)
    {
        transition.SetTrigger(Start1);
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneBuildIndex: _index);
        yield return null;
    }

    private void StartCrossFade()
    {
        if(MainMenuManager.Instance == null) return;
        MainMenuManager.Instance.crossFade.enabled = true;
    }
}
