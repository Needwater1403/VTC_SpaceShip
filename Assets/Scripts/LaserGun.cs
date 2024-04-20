using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private LineRenderer beam;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float maxLength;
    [SerializeField] private ParticleSystem muzzlepParticleSystem;
    [SerializeField] private ParticleSystem hitParticleSystem;
    private float laserDamage = 1;
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
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetBeam();
            muzzlepParticleSystem.Play();
            hitParticleSystem.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetBeam(false);
            muzzlepParticleSystem.Stop();
            hitParticleSystem.Stop();
        }
    }

    private void FixedUpdate()
    {
        if(!beam.enabled) return;
        Ray ray = new Ray(startPoint.position, startPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLength);
        Vector3 hitPos = cast ? hit.point : startPoint.position + startPoint.forward * maxLength;
        if (cast && hit.transform.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.TakeDamage(laserDamage);
        }
        beam.SetPosition(0, startPoint.position);
        beam.SetPosition(1, hitPos);
        hitParticleSystem.transform.position = hitPos;
        
    }
}
