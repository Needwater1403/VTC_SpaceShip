using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSpaceshipMovementSO", menuName = "Config/Config/Spaceship/Movement")]
public class ConfigSpaceshipMovementSO : ScriptableObject
{
    public float forwardSpeed;
    public float strafeSpeed;
    public float hoverSpeed;
    public float forwardAcceleration;
    public float strafeAcceleration;
    public float hoverAcceleration;
    public float rollSpeed;
    public float rollAcceleration;
    public float lookRateSpeed;
}
