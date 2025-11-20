using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    [Header("Layers to detect")]
    public LayerMask detectionMask = ~0; // Default: everything

    [Header("Player Stats")]
    public Action<Collision> OnElementCollisionEnter;
    public Action<Collision> OnElementCollisionExit;

    public Action<Collider> OnElementTriggerEnter;
    public Action<Collider> OnElementTriggerExit;

    [Header("Notifiers")]
    public UnityEvent FireCollisionEnter;
    public UnityEvent FireTriggerEnter;

    private bool isActive = true;

    public void InactiveCollisions() => isActive = false;
    public void ActiveCollisions()   => isActive = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        if (IsInDetectionMask(collision.collider.gameObject.layer))
        {
            FireCollisionEnter?.Invoke();
            OnElementCollisionEnter?.Invoke(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isActive) return;

        if (IsInDetectionMask(collision.collider.gameObject.layer))
        {
            OnElementCollisionExit?.Invoke(collision);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isActive) return;

        if (IsInDetectionMask(collider.gameObject.layer))
        {
            FireTriggerEnter?.Invoke();
            OnElementTriggerEnter?.Invoke(collider);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!isActive) return;

        if (IsInDetectionMask(collider.gameObject.layer))
        {
            OnElementTriggerExit?.Invoke(collider);
        }
    }

    // ✔ LayerMask check instead of tag check
    private bool IsInDetectionMask(int layer)
    {
        return (detectionMask.value & (1 << layer)) != 0;
    }
}
