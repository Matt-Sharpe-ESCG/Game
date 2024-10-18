using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Game Scene Refrences
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheckMarker;
    public GameObject enemiesEnd;
    public GameObject player;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject pauseButton;
    public GameObject listControls;
    public MeshRenderer zoneMesh;
    public Animator anim;

    // Game Asset Values
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        // Gravity
        isGrounded = Physics.CheckSphere(groundCheckMarker.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movement Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 2 Axis Movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Animation
        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

        // Sprint 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
            anim.SetBool("Sprinting", true);
        }
        else
        {
            anim.SetBool("Sprinting", false);
            speed = 5;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            killPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WinZone")
        {
            WinGame();
        }
    }

    void WinGame()
    {
        zoneMesh.enabled = false;
        enemiesEnd.SetActive(false);
        winScreen.SetActive(true);
        listControls.SetActive(false);
        pauseButton.SetActive(false);
        speed = 0f;
        anim.Play("Winner");
    }
    void killPlayer()
    {
        enemiesEnd.SetActive(false);
        deathScreen.SetActive(true);
        listControls.SetActive(false);
        pauseButton.SetActive(false);
        speed = 0f;
        anim.Play("Idle");
    }
}
