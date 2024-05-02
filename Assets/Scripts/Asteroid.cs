using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float maxHP = 150;
    [SerializeField] private GameObject explosionPrefab;
    private float currentHP;
    private bool isDestroyed;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
            GameManager.Instance.AddPoint(1);
            UIManager.Instance.SetPointText();
            AudioManager.Instance.PlayAudio(Constants.Explosion);
            Destroy(gameObject);
        }
    }
}
