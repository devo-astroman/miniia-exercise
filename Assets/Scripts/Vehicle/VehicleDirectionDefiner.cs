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

    public VehicleDirections GetNewDirectionFrom(List<VehicleDirections> directions)
    {
        int randomIndex = Random.Range(0, directions.Count);
        VehicleDirections newDirection = directions[randomIndex];
        return newDirection;
    }
}
