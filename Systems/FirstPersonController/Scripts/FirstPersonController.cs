using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool isRunning => Input.GetKey(sprintKey) && canSprint && isMoving && !isCrouching;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && controller.isGrounded && canCrouch;

    public bool shouldJump => Input.GetKey(jumpKey) && canJump && controller.isGrounded && !isCrouching;

    [Header("Function Options")]
    private bool canSprint = true;
    private bool canCrouch = true;
    public bool canLean = true;
    private bool canJump = true;
    public bool isMoving;
    public bool canFPS;
    private bool isWalking;

    [Header("Game Controls")]
    KeyCode sprintKey = KeyCode.LeftShift;
    KeyCode jumpKey = KeyCode.Space;
    KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Camera Variables")]
    private float xRotation;
    private float yRotation;
    [SerializeField] private float sensitivity = 1f;

    [Header("Moviment Variables")]
    private float speed = 0;
    private float walkSpeed = 6f;
    private float runSpeed = 7.5f;
    private float crouchSpeed = 3f;
    private float jumpForce = 1f;

    [Header("Crouch Variables")]
    [SerializeField] private float crouchHeight = 0.01f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.01f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    [SerializeField] public bool isCrouching;
    [SerializeField] private bool duringCrouchAnimation;

    [Header("PlayerObject Variables")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.4f;
    private bool isGrounded;
    private Vector3 graveffect;
    private float gravity = -12f;
    private Vector3 move;

    [Header("HeadBob Variables")]
    [SerializeField] private float walkBobSpeed = 12f;
    [SerializeField] private float WalkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 14f;
    [SerializeField] private float sprintBobAmount = 0.07f;
    [SerializeField] private float defaultYPos = 0;
    [SerializeField] private float timer;

    [Header("FootSteps Variables")]
    [SerializeField] AudioSource footSteepAudio;
    [SerializeField] AudioClip[] floor;

    [Header("Stamina Variables")]
    private float maxStamina = 100;
    private float staminaMultiplier = 5;
    private float staminaRegens = 8f; //time before starts regenerating stamina
    private float staminaValueIncrement = 5; 
    private float staminaTimeIncrement = 0.1f;
    [SerializeField] private float currentStamina;
    private Coroutine regeneratingStamina;
    private bool isRegeneratingStamina;
    private bool hasBoost;

    [Header("Leaning Variables")]
    [SerializeField] private Transform lean;
    [SerializeField] private float amount = 40f;
    [SerializeField] private float slerpAmount = 20f;
    private Quaternion initalRotation;
    private bool isLeaningLeft, isLeaningRight;
   

    [Header("Breathing Variables")]
    [SerializeField] public AudioSource heartAudio;
    [SerializeField] public AudioClip heartRuning;
    [SerializeField] private AudioSource tiredBreathAudio;
    [SerializeField] private AudioClip breathTiredRuning;

    private void Start()
    {
        //BreathingEffects
        heartAudio.volume = 0f;
        heartAudio.clip = heartRuning;

        CameraSettings();
        currentStamina = maxStamina;

        initalRotation = lean.localRotation;
        canLean = true;
    }

    public void Update()
    {
        CheckIfIsMoving();
        CheckSphere();        
        if (UIInventoryPage.canfps == true || canFPS == true)
        {
            Camera();
            Controller();
            if(canLean)
                Lean();
            HeadBob();
            FootSteps();
            Crouching();
        }
        Stamina();
        BreathEffects();
    }

    private void CameraSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultYPos = cam.transform.localPosition.y;
    }

    private void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yRotation += mouseX * sensitivity;
        xRotation -= mouseY * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(xRotation, cam.transform.localRotation.y, 0.0f);
        playerBody.transform.localRotation = Quaternion.Euler(playerBody.localRotation.x, yRotation, 0.0f);        
    }

    private void CheckSphere()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void CheckIfIsMoving()
    {
        if (move != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;
    }

    private void Controller()
    {
        float x = Input.GetAxisRaw("Horizontal"); 
        float z = Input.GetAxisRaw("Vertical"); 
        move = transform.forward * z + transform.right * x; 
        move = move.normalized; 

        if (shouldJump) 
        {
            graveffect.y = Mathf.Sqrt(jumpForce * -2f * gravity); 
            currentStamina -= 20f;
        }

        if (isRunning) 
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            isWalking = false;
        }
        else if (isCrouching)
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
            isWalking = false;
        }
        else 
        {
            controller.Move(move * walkSpeed * Time.deltaTime);
            isWalking = true;
        }

       

        controller.Move(graveffect * Time.deltaTime); 
        graveffect.y += gravity * Time.deltaTime; 
    }

    private void Crouching()
    {
        if (shouldCrouch)
            StartCoroutine(CrouchStand());

        if (shouldCrouch && Input.GetKeyDown(jumpKey))
        {
            StartCoroutine(CrouchStand());
        }
    }

    private IEnumerator CrouchStand()
    {
        duringCrouchAnimation = true;

        float timElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        while (timElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timElapsed / timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timElapsed / timeToCrouch);
            timElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    private void HeadBob()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        if (Mathf.Abs(move.x) > 0.1f || Mathf.Abs(move.z) > 0.1f)
        {
            timer += Time.deltaTime * (isRunning ? sprintBobSpeed : walkBobSpeed);
            cam.transform.localPosition = new Vector3(
                cam.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isRunning ? sprintBobAmount : WalkBobAmount),
                cam.transform.localPosition.z);
        }
    }

    private void FootSteps()
    {
        if (move == Vector3.zero) return;

        if (controller.isGrounded == true && isWalking && footSteepAudio.isPlaying == false)
        {
            footSteepAudio.pitch = Random.Range(1f, 1.2f);
            footSteepAudio.volume = Random.Range(0.05f, 0.1f);
            footSteepAudio.PlayOneShot(floor[Random.Range(0, floor.Length - 1)]);
        }
        else if (controller.isGrounded == true && isRunning && footSteepAudio.isPlaying == false)
        {
            footSteepAudio.pitch = Random.Range(1.3f, 1.4f);
            footSteepAudio.volume = Random.Range(0.1f, 0.2f);
            footSteepAudio.PlayOneShot(floor[Random.Range(0, floor.Length - 1)]);
        }
    }
    private void BreathEffects()
    {       
        if (currentStamina == 0 && isRunning == false && tiredBreathAudio.isPlaying == false)
        {
            tiredBreathAudio.volume = 1.5f;
            tiredBreathAudio.PlayOneShot(breathTiredRuning);
        }

        if (currentStamina > 0)
        {
            tiredBreathAudio.Stop();
        }
    }

    private void Stamina()
    {
        if (isRunning && move != Vector3.zero) 
        {
            if (regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }

            currentStamina -= staminaMultiplier * Time.deltaTime;

            if (currentStamina < 0)
                currentStamina = 0;

            if (currentStamina <= 0)
                canSprint = false;
        }

        if (!isRunning && currentStamina < maxStamina && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(RegenerateStamina());
            isRegeneratingStamina = true;
        }

        if (currentStamina > 0 && isRunning)
        {
            isRegeneratingStamina = false;
        }
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegens);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while (currentStamina < maxStamina)
        {
            if (currentStamina > 0)
                canSprint = true;

            currentStamina += staminaValueIncrement;

            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }

    public void StaminaChange(float val)
    {
        currentStamina =+ val;
    }

    private void Lean()
    {
        if(Input.GetKey(KeyCode.Q) && !isLeaningRight)
        {
            Quaternion newRot = Quaternion.Euler(lean.localRotation.x, lean.localRotation.y, lean.localRotation.z + amount);
            lean.localRotation = Quaternion.Slerp(lean.localRotation, newRot, Time.deltaTime * slerpAmount);
            Debug.Log("LEFT");
            isLeaningLeft = true;
        }
        else if(Input.GetKey(KeyCode.E) && !isLeaningLeft)
        {
            Quaternion newRot = Quaternion.Euler(lean.localRotation.x, lean.localRotation.y, lean.localRotation.z - amount);
            lean.localRotation = Quaternion.Slerp(lean.localRotation, newRot, Time.deltaTime * slerpAmount);            
            isLeaningRight = true;
        }
        else
        {
            lean.localRotation = Quaternion.Slerp(lean.localRotation, initalRotation, Time.deltaTime * slerpAmount);
            isLeaningRight = false;
            isLeaningLeft = false;
        }     
    }
}