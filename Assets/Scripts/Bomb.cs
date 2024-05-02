using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.PlayerTag))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
            Instantiate(explosionPrefab, GameManager.Instance.Player.transform.position, Quaternion.identity, GameManager.Instance.Player.transform.parent);
            AudioManager.Instance.PlayAudio(Constants.Explosion);
            GameManager.Instance.GameOver = true;
            Destroy(GameManager.Instance.Player.Visual.gameObject);
            Destroy(gameObject);
        }
    }
}
