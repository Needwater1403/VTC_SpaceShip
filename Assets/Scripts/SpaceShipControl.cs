using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Screen;

public class SpaceShipControl : MonoBehaviour
{
    public ConfigSpaceshipMovementSO configMovement;
    private float dirX, dirZ, hoverInput, rollInput;
    private Vector3 position;
    private Vector2 lookInput, center, mouseDistance;
    void Start()
    {
        center.x = width / 2;
        center.y = height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // MOUSE
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        mouseDistance.x = (lookInput.x - center.x) / center.x;
        mouseDistance.y = (lookInput.y - center.y) / center.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        
        // KEYUBOARD
        dirX = Mathf.Lerp(dirX, Input.GetAxisRaw("Horizontal") * configMovement.strafeSpeed, configMovement.strafeAcceleration * Time.deltaTime);
        dirZ = Mathf.Lerp(dirZ, Input.GetAxisRaw("Vertical") * configMovement.forwardSpeed, configMovement.forwardAcceleration * Time.deltaTime);
        hoverInput = Mathf.Lerp(hoverInput, Input.GetAxisRaw("Hover") * configMovement.hoverSpeed, configMovement.hoverAcceleration * Time.deltaTime);
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), configMovement.rollAcceleration* Time.deltaTime);
        
        transform.Rotate(-mouseDistance.y * configMovement.lookRateSpeed * Time.deltaTime, mouseDistance.x * configMovement.lookRateSpeed * Time.deltaTime, rollInput * configMovement.rollSpeed * Time.deltaTime, Space.Self);
        var transform1 = transform;
        position += transform1.forward * (dirZ * Time.deltaTime);
        position += transform1.right * (dirX * Time.deltaTime) + transform1.up * (hoverInput * Time.deltaTime);
        transform1.position = position;
        
    }
}
