using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorQuad : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Color _someColor;
    [SerializeField] private Color _emptyColor;

    private int _nElementsInCage = 0;
    
    
    void Start()
    {
        _collisionHandler.OnElementTriggerEnter += HandleElementTriggerEnter;
        _collisionHandler.OnElementTriggerExit += HandleElementTriggerExit;
    }

    void OnDestroy()
    {
        _collisionHandler.OnElementTriggerEnter -= HandleElementTriggerEnter;
        _collisionHandler.OnElementTriggerExit -= HandleElementTriggerExit;
    }

    private void HandleElementTriggerEnter(Collider coll){
        _nElementsInCage++;
        
        _meshRenderer.material.color = _someColor;

    }

    private void HandleElementTriggerExit(Collider coll){
        _nElementsInCage--;
        _meshRenderer.material.color = _emptyColor;
    }

    private void UpdateCageColor(){

        if(_nElementsInCage > 0){
            _meshRenderer.material.color = _someColor;
            
        }else{
            _meshRenderer.material.color = _emptyColor;
        }
    }
}
