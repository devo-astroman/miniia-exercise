using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private VehicleDirectionDefiner _directionDefiner;
    [SerializeField] private VehicleMovement _vehicleMovement;
    [SerializeField] private VehicleRadar _vehicleRadar;

    [SerializeField] private bool _changeDirectionOnTimeInterval = false;

    private SetTimeoutUtility timeoutDelayToDecideNewDirection;
    private SetIntervalUtility timeIntervalToDecideNewDirection;

    void Awake()
    {
        timeoutDelayToDecideNewDirection = new SetTimeoutUtility(this);
        timeIntervalToDecideNewDirection = new SetIntervalUtility(this);
    }

    void Start()
    {
        _vehicleRadar.NotifyMeOn(VehicleDirections.Left);
        _vehicleRadar.OnObstacleDetected += HandleTargetDetected;

        _vehicleMovement.MoveLeft();


        if (_changeDirectionOnTimeInterval)
        {            
            timeoutDelayToDecideNewDirection.SetTimeout(() => {
                timeIntervalToDecideNewDirection.SetInterval(() => {
                    DecideNewDirection();
                }, 2);            
            }, 8);
        }
    }

    void OnDestroy()
    {
        _vehicleRadar.OnObstacleDetected -= HandleTargetDetected;
    }

    private void HandleTargetDetected(Transform target, VehicleDirections directionDetected){

        DecideNewDirection();
    }

    private void DecideNewDirection(){
        List<VehicleDirections> freeDirections = _vehicleRadar.GetFreeDirections();

        VehicleDirections newDirection = _directionDefiner.GetNewDirectionFrom(freeDirections);

        _vehicleRadar.NotifyMeOn(newDirection);

        switch (newDirection)
        {
            case VehicleDirections.Forward:
                _vehicleMovement.MoveForward();
                break;
            case VehicleDirections.Backward:
                _vehicleMovement.MoveBackward();
                break;
            case VehicleDirections.Left:
                _vehicleMovement.MoveLeft();
                break;
            case VehicleDirections.Right:
                _vehicleMovement.MoveRight();
                break;
            case VehicleDirections.Stop:
                // Do nothing
                break;
        }
    }
}
