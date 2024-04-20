using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private SpaceShipControl player;
    private int point;
    public int Point => point;
    public TextMeshProUGUI txtPoint;
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

    public void AddPoint(int _amount)
    {
        point += _amount;
    }
}
