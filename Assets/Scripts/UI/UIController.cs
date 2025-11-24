using System;
using UnityEngine.UIElements;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    public Action OnStartButtonClick;

    private Button startButton;


    void Start()
    {
        GetUIElements();
        RegisterButtonCallbacks();
    }

    public void HideStartButton()
    {
        startButton.style.display = DisplayStyle.None;
    }

    private void GetUIElements()
    {
        // Initial queries to get every ui element
        var root = _document.rootVisualElement;

        startButton = root.Q<Button>("StartButton");        
    }

    private void RegisterButtonCallbacks()
    {
        startButton.RegisterCallback<ClickEvent>(evt =>
        { 
            OnStartButtonClick?.Invoke();            
        });

        
    }
    
}
