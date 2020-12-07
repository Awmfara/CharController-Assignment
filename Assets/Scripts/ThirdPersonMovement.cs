using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Character Settings")]
    [Tooltip("Assign to Character")]
    public Player player;
    public CharacterController controller;

    [SerializeField, Tooltip("Gravity of Character")]
    private float gravity = -9.81f;

    [SerializeField, Tooltip("Velocity of Character Falling")]
    public Vector3 playerVelocity;


    [Header("Camera Settings")]
    [Tooltip("Assign to Camera")]
    public Transform cam;
    [SerializeField, Tooltip("Smooths the Camera movement angle time")]
    private float turnSmoothTime = 0.1f;
    [Tooltip("Smooths the Camera movement velocity time")]
    private float turnSmoothVelocity;
    private bool isGrounded;

    public bool Respawn;
    [SerializeField]
    private Vector3 respawnPoint;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (Respawn)
        {
            player.transform.position = respawnPoint;
            Respawn = false;
        }
        else
        {
            Movement();
            Jump();
        }
   
    }
    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }
    /// <summary>
    /// Method dictating Jumping, Taking into account:
    /// Keyboards or controller Jump Input,
    /// Gravity, Jump Height and Velocity
    /// 
    /// </summary>
    void Jump()
    {
        if (isGrounded && playerVelocity.y < 0)//resets player velocity to O when grounded

        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // what happens when char jumps
        {
            playerVelocity.y += Mathf.Sqrt(player.playerStats.jumpHeight * -3.0f * gravity);
        }
        playerVelocity.y += gravity * Time.deltaTime; //calculates velocity of char falling as to increase as time progresses
        controller.Move(playerVelocity * Time.deltaTime); //moves cahracter based on velocity multiplied bu time


    }
    /// <summary>
    /// Movement of Character, 
    /// Taking into acount Xaxis, Zaxis on Keyboard or Controller
    /// And mouse moving camera and its angles
    /// Further Descriptions in Method
    /// </summary>
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //gets horizontal controls gamepad or AD or Left, Right.
        float vertical = Input.GetAxisRaw("Vertical"); //gets veritcal controls i.e. gamepad of WS or up down.
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;//creates parameter direction with horizontal movement, xaxis, no movement z axis, and vertical y azxis.



        if (direction.magnitude >= 0.1f) //as using raw axis, requires minimal input to move
        {
            //moves the way the character is facing taking into account the x-axis, TOA(Tan Opposite Adjacent)+ the y axis mouse  angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smooths angle using velocity and time as assignable floats
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // defines the rotaion based on previous two statements
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//character moves relative to cameraa

            // chooses speed based on whether sprinting or crouching
            float movementSpeed = player.playerStats.speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (player.playerStats.currentStamina>0)
                {
                    movementSpeed = player.playerStats.sprintSpeed;
                    player.playerStats.currentStamina -= Time.deltaTime * player.playerStats.staminaDecreaseRate;
                }
             
            }
           
            else if (Input.GetKey(KeyCode.C))
            {
                movementSpeed = player.playerStats.crouchSpeed;
            }
            else
            {
                if (player.playerStats.currentStamina<player.playerStats.maxStamina)
                {
                    player.playerStats.currentStamina += Time.deltaTime * player.playerStats.staminaIncreaseRate;
                }
           
            }

            controller.Move(moveDir * movementSpeed * Time.deltaTime);//moves character in direction, times the speed, times real time
        }
        else if (player.playerStats.currentStamina < player.playerStats.maxStamina)
        {
            player.playerStats.currentStamina += Time.deltaTime * player.playerStats.staminaIncreaseRate;
        }
    }
    bool IsGrounded()
    {
        //Bit shift the index of the layer(8) to get a bit mask
        int layerMask = 1 << 8;
        //This would cast rays only against colliders in layer 8
        //But instead we want to collide against everything except layer 8.  The ~ operator does this, it inverts a bit mask
        layerMask = ~layerMask;

        RaycastHit hit;
        //Raycast origin, direction,
        if (Physics.SphereCast(transform.position, controller.radius, -Vector3.up, out hit,controller.bounds.extents.y+0.1f-controller.bounds.extents.x, layerMask))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (-Vector3.up *(controller.bounds.extents.y + 0.1f - controller.bounds.extents.x)), controller.radius);
    }
}
