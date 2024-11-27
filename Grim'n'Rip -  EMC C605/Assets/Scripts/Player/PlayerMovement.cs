using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Upgradable")]
    public float playerMovementSpeed;

    [Header("PlayerComponent")]
    public Animator playerAnim;
    public Rigidbody playerRb;
    [SerializeField] FixedJoystick movementJoystick;
    [SerializeField] FixedJoystick aimingJoystick;
    private Vector3 lastAimDirection;

    [Header("Aiming Rotation Properties")]
    public float rotationSpeed = 5f; // Smoothing speed for rotation

    void Start()
    {
        lastAimDirection = Vector3.forward; // Default facing direction
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        playerMovementSpeed = PlayerPrefs.GetFloat("movementSpeedAmount") + PlayerPrefs.GetFloat("temporaryMovementSpeedAmount");

        if (GameManager.instance.enemiesValue <= 0 && GameManager.instance.isRoundStart == true)
        {
            GameManager.instance.ResetPlayerPosition();
        }
    }
    void FixedUpdate()
    {
        if (GameManager.instance.canMove == true)
        {
             // Handle movement
            Vector3 movement = new Vector3(movementJoystick.Horizontal * playerMovementSpeed, playerRb.velocity.y, movementJoystick.Vertical * playerMovementSpeed);
            playerRb.velocity = movement;

            bool isMoving = movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0;
            bool isAiming = aimingJoystick.Horizontal != 0 || aimingJoystick.Vertical != 0;

            // Handle aiming input
            if (isAiming)
            {
                lastAimDirection = new Vector3(aimingJoystick.Horizontal, 0, aimingJoystick.Vertical).normalized;

                // Smoothly rotate the player towards the aim direction
                Quaternion targetRotation = Quaternion.LookRotation(lastAimDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                if (isMoving)
                {
                    // Moving while shooting
                    playerAnim.SetInteger("animState", 2);  // Introduce a new state for moving and shooting
                }
                else
                {
                    // Aiming but not moving
                    playerAnim.SetInteger("animState", 1);  // Shooting but standing still
                }
            }
            else if (isMoving)
            {
                // If moving but not aiming, use last aim direction
                Quaternion targetRotation = Quaternion.LookRotation(lastAimDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                // Moving without shooting
                playerAnim.SetInteger("animState", 2);  // Running animation
            }
            else
            {
                // Player is idle
                playerAnim.SetInteger("animState", 1);
            }
        }    
    }
}


    

//     //CAMERA MOVEMENT WITH ROTATING CAMERA USING THE AIMING JOYSTICK ====================================================================================================
//     [Header("Upgradable")]
//     public float playerMoveSpeed;

//     [Header("Player Components")]
//     [SerializeField] Rigidbody playerRb;
//     [SerializeField] FixedJoystick movementJoystick;
//     [SerializeField] FixedJoystick aimingJoystick;
//     private Vector3 lastAimDirection;


//     [Header("Camera Properties")]
//     [SerializeField] CinemachineVirtualCamera virtualCamera; // Cinemachine reference
//     private Vector3 initialCameraPosition;
//     private Quaternion initialCameraRotation;

//     void Start()
//     {
//         lastAimDirection = Vector3.forward; // Default facing direction
//         playerRb = GetComponent<Rigidbody>();

//         // Store the initial position and rotation of the camera
//         initialCameraPosition = virtualCamera.transform.position;
//         initialCameraRotation = virtualCamera.transform.rotation;
//     }

//     // Update is called once per frame
//     void FixedUpdate()
//     {
//         // Handle movement
//         Vector3 movement = new Vector3(movementJoystick.Horizontal * playerMoveSpeed, playerRb.velocity.y, movementJoystick.Vertical * playerMoveSpeed);
//         playerRb.velocity = movement;

//         // Check if there is aim input, if so update the aim direction
//         if (aimingJoystick.Horizontal != 0 || aimingJoystick.Vertical != 0)
//         {
//             lastAimDirection = new Vector3(aimingJoystick.Horizontal, 0, aimingJoystick.Vertical).normalized;
//             transform.rotation = Quaternion.LookRotation(lastAimDirection); // Rotate the player to face the aim direction

//             // Rotate the virtual camera without changing its position
//             virtualCamera.transform.position = initialCameraPosition; // Keep the original position
//             virtualCamera.transform.rotation = Quaternion.Euler(initialCameraRotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
//         }
//         else if (movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0)
//         {
//             // If the player is moving and there is no aim input, keep the last aim direction for facing
//             transform.rotation = Quaternion.LookRotation(lastAimDirection);

//             // Keep the camera's original position and apply the last aim rotation
//             virtualCamera.transform.position = initialCameraPosition;
//             virtualCamera.transform.rotation = Quaternion.Euler(initialCameraRotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
//         }

//         // Handle animations based on movement
//         if (movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0)
//         {
//             Vector3 moveDirection = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical).normalized;

//             // Check if the player is moving backward (opposite of the last aim direction)
//             if (Vector3.Dot(moveDirection, lastAimDirection) < -0.5f)
//             {
//                 // Running backward while firing
//             }
//             else
//             {
//                 // Regular forward movement while firing
//             }
//         }
//         else
//         {
//             // Player is idle
//         }
// }