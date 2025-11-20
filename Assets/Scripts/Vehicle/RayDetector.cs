using System;
using UnityEngine;
using UnityEngine.Events;

public class RayDetector : MonoBehaviour
{
    [Header("Ray Setup")]
    [Tooltip("Point where the ray starts.")]
    public Transform originPoint;

    [Tooltip("Point that defines the direction of the ray (direction = dirPoint - originPoint).")]
    public Transform directionPoint;

    [Header("Detection Settings")]
    public float maxDistance = 10f;
    public LayerMask detectionMask = ~0;  // By default, hit everything

    [Header("C# Events (Actions)")]
    public Action<Transform> OnTargetDetected; // passes the hit Transform
    public Action OnTargetLost;

    
    [Serializable]
    public class TransformUnityEvent : UnityEvent<Transform> { }
    [Header("Unity Events (Inspector-friendly)")]
    public TransformUnityEvent OnTargetDetectedUnity;
    public UnityEvent OnTargetLostUnity;

    private bool isDetected;
    private Transform currentDetected;
    private Vector3 lastDirection;  // For gizmos

    private void Reset()
    {
        // Useful default: if not set, use this transform
        originPoint = transform;
    }

    private void Update()
    {
        if (originPoint == null || directionPoint == null)
            return;

        // Calculate direction from origin to direction point
        Vector3 dir = directionPoint.position - originPoint.position;

        if (dir.sqrMagnitude < 0.0001f)
        {
            // Points are almost in the same place → no valid direction
            LoseTargetIfNeeded();
            return;
        }

        dir.Normalize();
        lastDirection = dir;

        // Raycast from origin in that direction
        if (Physics.Raycast(originPoint.position, dir, out RaycastHit hit, maxDistance, detectionMask))
        {
            Transform hitTransform = hit.transform;
            DetectTargetIfNeeded(hitTransform);
        }
        else
        {
            // No hit this frame → lost
            LoseTargetIfNeeded();
        }
    }

    private void DetectTargetIfNeeded(Transform detected)
    {
        // If already detecting this same object, do nothing
        if (isDetected && currentDetected == detected)
            return;

        // If we were detecting something else, notify lost first
        if (isDetected && currentDetected != detected)
        {
            OnTargetLost?.Invoke();
            OnTargetLostUnity?.Invoke();
        }

        isDetected = true;
        currentDetected = detected;

        // C# Actions
        OnTargetDetected?.Invoke(detected);

        // UnityEvents
        OnTargetDetectedUnity?.Invoke(detected);
    }

    private void LoseTargetIfNeeded()
    {
        if (!isDetected)
            return;

        isDetected = false;

        // C# Actions
        OnTargetLost?.Invoke();

        // UnityEvents
        OnTargetLostUnity?.Invoke();

        currentDetected = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (originPoint == null || directionPoint == null)
            return;

        Vector3 origin = originPoint.position;
        Vector3 dir = (directionPoint.position - origin).normalized;

        Gizmos.color = isDetected ? Color.green : Color.red;
        Gizmos.DrawRay(origin, dir * maxDistance);
        Gizmos.DrawSphere(origin + dir * maxDistance, 0.08f);
    }
}
