using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Title("Player UI")] 
    [SerializeField] private Slider healthSlider;
    
    [Title("Panel")] 
    [SerializeField] private GameObject YouDiedPanel;
    [SerializeField] private GameObject YouWinPanel;
    [Title("Text")] 
    [SerializeField] private TextMeshProUGUI txt_point;
    private void Start()
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

    public void UpdateTimerUI(float timer, float maxLength)
    {
        var percentage = timer / maxLength ;
        healthSlider.value = (percentage);
    }

    public void ShowLosePanel(bool show)
    {
        YouDiedPanel.SetActive(show);
    }
    
    public void ShowWinPanel(bool show)
    {
        YouWinPanel.SetActive(show);
    }

    public void OnClickRestart()
    {
        LevelLoader.Instance.LoadLevel(1);
    }
    
    public void OnClickReturnToMainMenu()
    {
        LevelLoader.Instance.LoadLevel(0);
    }

    public void SetPointText()
    {
        txt_point.SetText($"{GameManager.Instance.Point}");
    }
}
