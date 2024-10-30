using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }
            return instance;
        }
    }

    private PlayerControls playerControls;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetLookInput()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetJumpInput()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool GetInteractInput()
    {
        return playerControls.Player.Interact.triggered;
    }
}
