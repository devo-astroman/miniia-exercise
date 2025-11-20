using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDirectionDefiner : MonoBehaviour
{
    private VehicleDirections currentDirection = VehicleDirections.Stop;

    public VehicleDirections GetNewDirection()
    {
        currentDirection = VehicleDirectionUtility.GetRandomDirection(currentDirection);
        return currentDirection;
    }
}
