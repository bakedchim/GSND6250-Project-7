using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private InputManager inputManager;

    private Transform cameraTransform;

    bool canMove = true;
    string interctableObjectTag = "";
    
    [SerializeField]
    private TMP_Text interactText;

    [SerializeField]
    private DialogController dialogController;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canMove)
            return;

        if (interctableObjectTag != "" && inputManager.GetInteractInput()) {
            dialogController.StartInteraction(interctableObjectTag);
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetMovementInput();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (inputManager.GetJumpInput() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        // Check the layer of the object that the player is colliding with
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            interctableObjectTag = other.tag;
            interactText.gameObject.SetActive(true);
        }   
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            interctableObjectTag = other.tag;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            interctableObjectTag = "";
            interactText.gameObject.SetActive(false);
        }
    }
}
