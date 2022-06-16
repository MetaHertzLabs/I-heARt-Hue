using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    InputAction menuToggle;
    bool menuActive;
    [SerializeField] GameObject menuPanel;

    // Start is called before the first frame update
    private void Start()
    {
        var inputActionMap = inputActions.FindActionMap("XRI LeftHand Interaction");
        menuToggle = inputActionMap.FindAction("Menu Toggle");
        menuToggle.performed += ToggleMenu;
        menuToggle.Enable();
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (menuActive)
        {
            menuActive = false;
            menuPanel.SetActive(false);
        }
        else
        {
                menuActive = true;
                menuPanel.SetActive(true);
        }
    }
}
