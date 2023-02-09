using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    Rigidbody rb;
    public Animator playerAnimator;
    public Camera playerCamera;

    float distanceToGround;
    bool isGrounded;
    public float jumpForce;
    public float walkSpeed;
    Vector3 cameraRotation;

    public GameObject projectile;
    public Transform projectilePos;

    bool isWalking = false;

    void Awake()
    {
        inputAction = new PlayerAction();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => rotate = Vector2.zero;

        inputAction.Player.Shoot.performed += cntxt => Shoot();

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        inputAction.Player.Enable();
    }

    void Update()
    {
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z);

        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);
        
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }

    void LateUpdate()
    {
        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    void OnDisable()
    {
        inputAction.Player.Disable();
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void Shoot()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, -Vector3.up * distanceToGround);
    }
}
