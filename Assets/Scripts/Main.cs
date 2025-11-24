using System;
using UnityEngine;
using UnityEngine.Events;

public class Main : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Point where the ray starts.")]
    
    [SerializeField] private GameObject _vehicles;

    [SerializeField] private UIController _uiController;

    [SerializeField] private FreeFlyCamera _freeFlyCamera;

    

    void Start()
    {
        _uiController.OnStartButtonClick += HandleStartButtonClick;
    }

    void OnDestroy()
    {
        _uiController.OnStartButtonClick -= HandleStartButtonClick;
    }

    private void HandleStartButtonClick()
    {        
        _vehicles.SetActive(true);
        _freeFlyCamera.enabled = true;

        _uiController.HideStartButton();
    }


}
