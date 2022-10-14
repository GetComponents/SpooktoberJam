using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed, sprintinSpeed;
    Vector2 movementDirection;
    bool isSprinting;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isSprinting)
        {
            rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.y * sprintinSpeed);
        }
        else
        {
            rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.y * movementSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<bool>();
    }
}
