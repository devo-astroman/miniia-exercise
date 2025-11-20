using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VehicleRadar : MonoBehaviour
{
    [SerializeField] private RayDetector _rayDetectorForward;
    [SerializeField] private RayDetector _rayDetectorBackward;
    [SerializeField] private RayDetector _rayDetectorLeft;
    [SerializeField] private RayDetector _rayDetectorRight;

    [SerializeField] private bool _forwardBlocked = false;
    [SerializeField] private bool _backwardBlocked = false;
    [SerializeField] private bool _leftBlocked = false;
    [SerializeField] private bool _rightBlocked = false;

    private VehicleDirections directionToNotify = VehicleDirections.Stop;

    [Header("C# Events (Actions)")]
    public Action<Transform, VehicleDirections> OnObstacleDetected;



    void Start()
    {
        _rayDetectorForward.OnTargetDetected += HandleTargetDetectedF;
        _rayDetectorForward.OnTargetLost += HandleOnTargetLostF;

        _rayDetectorBackward.OnTargetDetected += HandleTargetDetectedB;
        _rayDetectorBackward.OnTargetLost += HandleOnTargetLostB;

        _rayDetectorLeft.OnTargetDetected += HandleTargetDetectedL;
        _rayDetectorLeft.OnTargetLost += HandleOnTargetLostL;

        _rayDetectorRight.OnTargetDetected += HandleTargetDetectedR;
        _rayDetectorRight.OnTargetLost += HandleOnTargetLostR;
    }

    void OnDestroy()
    {
        _rayDetectorForward.OnTargetDetected -= HandleTargetDetectedF;
        _rayDetectorForward.OnTargetLost -= HandleOnTargetLostF;

        _rayDetectorBackward.OnTargetDetected -= HandleTargetDetectedB;
        _rayDetectorBackward.OnTargetLost -= HandleOnTargetLostB;

        _rayDetectorLeft.OnTargetDetected -= HandleTargetDetectedL;
        _rayDetectorLeft.OnTargetLost -= HandleOnTargetLostL;

        _rayDetectorRight.OnTargetDetected -= HandleTargetDetectedR;
        _rayDetectorRight.OnTargetLost -= HandleOnTargetLostR;
    }


    public void NotifyMeOn(VehicleDirections direction){
        directionToNotify = direction;
    }

    //get directions with no obstacles
    public List<VehicleDirections> GetFreeDirections(){        

        List<VehicleDirections> freeDirections = new List<VehicleDirections>();

        if(!_forwardBlocked){
            freeDirections.Add(VehicleDirections.Forward);
        }
        if(!_backwardBlocked){
            freeDirections.Add(VehicleDirections.Backward);
        }
        if(!_leftBlocked){
            freeDirections.Add(VehicleDirections.Left);
        }
        if(!_rightBlocked){
            freeDirections.Add(VehicleDirections.Right);
        }

        return freeDirections;
    }

    private void HandleTargetDetectedF(Transform target){
        _forwardBlocked = true;
        NotifyDetection(VehicleDirections.Forward);
    }
    private void HandleOnTargetLostF(){
        _forwardBlocked = false;        
    }

    private void HandleTargetDetectedB(Transform target){
        _backwardBlocked = true;
        NotifyDetection(VehicleDirections.Backward);
    }
    private void HandleOnTargetLostB(){
        _backwardBlocked = false;        
    }

    private void HandleTargetDetectedL(Transform target){
        _leftBlocked = true;
        NotifyDetection(VehicleDirections.Left);
    }
    private void HandleOnTargetLostL(){
        _leftBlocked = false;        
    }

    private void HandleTargetDetectedR(Transform target){
        _rightBlocked = true;
        NotifyDetection(VehicleDirections.Right);
    }
    private void HandleOnTargetLostR(){
        _rightBlocked = false;
    }

    private void NotifyDetection(VehicleDirections directionDetected){
        if(directionToNotify == directionDetected){
            OnObstacleDetected?.Invoke(this.transform, directionDetected);
        }
    }
 
}
