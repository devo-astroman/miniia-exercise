using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    [SerializeField] private VehicleDirectionDefiner _directionDefiner;
    [SerializeField] private VehicleMovement _vehicleMovement;

    

    // Start is called before the first frame update
    void Start()
    {
        _vehicleMovement.MoveLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
