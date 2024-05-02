using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserGun : MonoBehaviour
{
    private enum LaserID
    {
        LG1,
        LG2
    };

    [SerializeField] private LaserID id;
    [SerializeField] private LineRenderer beam;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float maxLength;
    [SerializeField] private ParticleSystem muzzleParticleSystem;
    [SerializeField] private ParticleSystem hitParticleSystem;
    private float laserDamage = 1;
    private bool isPlay;
    private bool playHitAudio;
    private void Awake()
    {
        beam.enabled = false;
    }

    private void SetBeam(bool _enabled = true)
    {
        beam.enabled = _enabled;
        
        if (!enabled)
        {
            var position = startPoint.position;
            beam.SetPosition(0, position);
            beam.SetPosition(1, position);
        }
    }

    private void BeamOn()
    {
        isPlay = true;
        SetBeam();
        muzzleParticleSystem.Play();
        hitParticleSystem.Play();
        AudioManager.Instance.PlayAudio(Constants.LaserGun);
    }

    private void BeamOff()
    {
        isPlay = false;
        SetBeam(false);
        muzzleParticleSystem.Stop();
        hitParticleSystem.Stop();
        AudioManager.Instance.StopAudio(Constants.LaserGun);
        playHitAudio = false;
        switch (id)
        {
            case LaserID.LG1:
                AudioManager.Instance.StopAudio(Constants.LaserHit1);
                break;
            case LaserID.LG2:
                AudioManager.Instance.StopAudio(Constants.LaserHit2);
                break;
        }
    }
    private void Update()
    {
        if (!GameManager.Instance.Player.CanControl) return;
        if(Input.GetMouseButtonDown(0) && !isPlay)
        {
            BeamOn();
        }
        else if ((Input.GetMouseButtonUp(0) || GameManager.Instance.IsFinished) && isPlay)
        {
            BeamOff();
        }
    }

    private void FixedUpdate()
    {
        if(!beam.enabled) return;
        var ray = new Ray(startPoint.position, startPoint.forward);
        var cast = Physics.Raycast(ray, out var hit, maxLength);
        var hitPos = cast ? hit.point : startPoint.position + startPoint.forward * maxLength;
        if (cast)
        {
            if (!playHitAudio)
            {
                playHitAudio = true;
                switch (id)
                {
                    case LaserID.LG1:
                        AudioManager.Instance.PlayAudio(Constants.LaserHit1);
                        break;
                    case LaserID.LG2:
                        AudioManager.Instance.PlayAudio(Constants.LaserHit2);
                        break;
                }
            }
            if(hit.transform.TryGetComponent(out Asteroid enemy))
            {
                enemy.TakeDamage(laserDamage);
            }
        }
        else
        {
            playHitAudio = false;
            switch (id)
            {
                case LaserID.LG1:
                    AudioManager.Instance.StopAudio(Constants.LaserHit1);
                    break;
                case LaserID.LG2:
                    AudioManager.Instance.StopAudio(Constants.LaserHit2);
                    break;
            }
        }
        beam.SetPosition(0, startPoint.position);
        beam.SetPosition(1, hitPos);
        hitParticleSystem.transform.position = hitPos;
    }
}
