using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    [SerializeField] private GameObject explosionPrefab;
    private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}
