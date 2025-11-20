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

    [Header("Target to Detect")]
    public Transform target;              // Object we want to detect
    public float maxDistance = 10f;
    public LayerMask detectionMask = ~0;  // By default, hit everything

    [Header("C# Events (Actions)")]
    public Action<Transform> OnTargetDetected;
    public Action OnTargetLost;

    [Header("Unity Events (Inspector-friendly)")]
    public UnityEvent OnTargetDetectedUnity;
    public UnityEvent OnTargetLostUnity;

    private bool isDetected;
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
            // If we have a specific target, check if we hit it
            if (target != null && hit.transform == target)
            {
                DetectTargetIfNeeded();
                return;
            }

            // If you later want to detect "any hit", you could:
            // DetectTargetIfNeeded();
            // and maybe pass hit.transform to the Action.
        }

        // If we reach this point, we consider the target lost
        LoseTargetIfNeeded();
    }

    private void DetectTargetIfNeeded()
    {
        if (isDetected) return;

        isDetected = true;

        // C# Actions
        OnTargetDetected?.Invoke(target);

        // UnityEvents
        OnTargetDetectedUnity?.Invoke();
    }

    private void LoseTargetIfNeeded()
    {
        if (!isDetected) return;

        isDetected = false;

        // C# Actions
        OnTargetLost?.Invoke();

        // UnityEvents
        OnTargetLostUnity?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        if (originPoint == null || directionPoint == null)
            return;

        Vector3 origin = originPoint.position;
        Vector3 dir;

        // In edit mode lastDirection may not be set, so recalc just for gizmos
        dir = (directionPoint.position - origin).normalized;

        Gizmos.color = isDetected ? Color.green : Color.red;
        Gizmos.DrawRay(origin, dir * maxDistance);
        Gizmos.DrawSphere(origin + dir * maxDistance, 0.08f);
    }
}
