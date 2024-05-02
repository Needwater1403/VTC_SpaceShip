using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Screen;
using Random = System.Random;

public class SpaceShipControl : MonoBehaviour
{
    public ConfigSpaceshipMovementSO configMovement;
    [SerializeField] private Transform visual;
    
    #region Parameters
    // ================ Manual ================
    private float dirX, dirZ, hoverInput, rollInput;
    private Vector3 position;
    private Vector2 lookInput, center, mouseDistance;

    private bool canControl;

    public bool CanControl => canControl;
    // ================ Auto ================
    private float timer = 1;
    private float roll;
    #endregion
    
    public Transform Visual => visual;
    void Start()
    {
        position = transform.position;
        center.x = width / 2;
        center.y = height / 2;
    }
    
    void Update()
    {
        if(canControl) ManualControl(); 
        else AutoMovingFoward();
    }

    private void ManualControl()
    {
        if (GameManager.Instance.IsFinished) return;
        
        // MOUSE
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        mouseDistance.x = (lookInput.x - center.x) / center.x;
        mouseDistance.y = (lookInput.y - center.y) / center.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        // KEYBOARD
        dirX = Mathf.Lerp(dirX, Input.GetAxisRaw("Horizontal") * configMovement.strafeSpeed,
            configMovement.strafeAcceleration * Time.deltaTime);
        dirZ = Mathf.Lerp(dirZ, Input.GetAxisRaw("Vertical") * configMovement.forwardSpeed,
            configMovement.forwardAcceleration * Time.deltaTime);
        hoverInput = Mathf.Lerp(hoverInput, Input.GetAxisRaw("Hover") * configMovement.hoverSpeed,
            configMovement.hoverAcceleration * Time.deltaTime);
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"),
            configMovement.rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * configMovement.lookRateSpeed * Time.deltaTime,
            mouseDistance.x * configMovement.lookRateSpeed * Time.deltaTime,
            rollInput * configMovement.rollSpeed * Time.deltaTime, Space.Self);
        var transform1 = transform;
        position += transform1.forward * (dirZ * Time.deltaTime);
        position += transform1.right * (dirX * Time.deltaTime) + transform1.up * (hoverInput * Time.deltaTime);
        transform1.position = position;
    }


    private void AutoMovingFoward()
    {
        if(MainMenuManager.Instance != null)
        {   
            // MOVE FORWARD
            dirZ = Mathf.Lerp(dirZ,
                !MainMenuManager.Instance.IsStart
                    ? configMovement.forwardSpeed / 1.5f
                    : configMovement.forwardSpeed * 5,
                configMovement.forwardAcceleration * Time.deltaTime);
            var transform1 = transform;
            transform1.position += transform1.forward * (dirZ * Time.deltaTime);
            
            // HOVER
            if (MainMenuManager.Instance.IsStart) return;
            switch (timer)
            {
                case >= 6:
                    timer -= 6;
                    roll = UnityEngine.Random.Range(-.2f, .2f);
                    break;
                case <= 1:
                    timer += Time.deltaTime;
                    rollInput = Mathf.Lerp(rollInput, roll,
                        configMovement.rollAcceleration * Time.deltaTime);

                    transform.Rotate(-mouseDistance.y * configMovement.lookRateSpeed * Time.deltaTime,
                        mouseDistance.x * configMovement.lookRateSpeed * Time.deltaTime,
                        rollInput * configMovement.rollSpeed * Time.deltaTime, Space.Self);
                    break;
                default:
                    timer += Time.deltaTime;
                    break;
            }
        }
        else
        {
            dirZ = Mathf.Lerp(dirZ, configMovement.forwardSpeed * 5,
                    configMovement.forwardAcceleration * Time.deltaTime);
            var transform1 = transform;
            if (transform1.position.z >= -350)
            {
                position = transform1.position;
                canControl = true;
            }
            else transform1.position += transform1.forward * (dirZ * Time.deltaTime);
        }
    }

}
