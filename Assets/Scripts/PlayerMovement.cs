using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed, sprintinSpeed;
    Vector2 movementDirection;
    public bool isSprinting;
    Rigidbody rb;
    [SerializeField]
    LayerMask groundmask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (isSprinting)
        {
            rb.velocity = new Vector3(movementDirection.x * sprintinSpeed, rb.velocity.y, movementDirection.y * sprintinSpeed);
        }
        else
        {
            rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.y * movementSpeed);
        }
    }

    private void Rotate()
    {
        //Debug.Log(movementDirection.x + " / " + movementDirection.y);
        if (movementDirection.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 180 - (movementDirection.x * 45), 0);
        }
        else
        {

            transform.eulerAngles = new Vector3(0, movementDirection.x * 90, 0);
        }


        if (movementDirection.x == 0 && movementDirection.y == 0)
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(cameraRay, out hit, 200, groundmask))
            {
                Vector3 pointToLook = hit.point;
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z), Vector3.up);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }
}
