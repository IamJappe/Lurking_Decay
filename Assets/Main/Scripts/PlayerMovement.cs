
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float gravity = -9.81f;
    public int jumpPower = 5;
    public int speed = 10;

    [Header("Gravity")]
    public Transform groundCheck;
    public LayerMask mask;
    bool isGrounded;

    [Header("Crouch")]
    public float crouchHeight = 0.5f;
    public float originalHeight;
    public float crouchSmoothTime = 0.2f;
    public float standingSpeed = 10f;

    private bool isCrouching = false;
    private CharacterController controller;
    private Vector3 velocity;
    public GameObject crouchPostProcessing;

    [Header("Sprinting")]
    public int runningSpeed;
    private int originalSpeed = 10;
    bool shouldSprint = true;
    private bool shouldRun = true;
    public float sprintCooldown = 5f;
    private float sprintCooldownTimer;

    [Header("Stamina")]
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 10f;
    public float staminaRechargeRate = 5f;

    private float currentStamina;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        currentStamina = maxStamina;
        UpdateStaminaBar();
    }

    private void Update()
    {
        Movement();
        Sprinting();
        HandleCrouch();
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .5f, mask);
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.Move(move);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpPower;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

   void Sprinting()
{
    bool isRunning = Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));

    if (!isCrouching)
    {
        if (isRunning && shouldSprint)
        {
            if (currentStamina > 0)
            {
                speed = runningSpeed;
                DecreaseStamina(staminaDecreaseRate * Time.deltaTime);
            }
            else
            {
                shouldSprint = false;
                speed = originalSpeed;
                sprintCooldownTimer = sprintCooldown; // Start cooldown
            }
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                RechargeStamina(staminaRechargeRate * Time.deltaTime);
            }
            else if (sprintCooldownTimer > 0)
            {
                sprintCooldownTimer -= Time.deltaTime;
            }
            else
            {
                shouldSprint = true;
            }

            speed = originalSpeed;
        }
    }
}

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                StartCoroutine(CrouchTransition(crouchHeight));
                speed = 6;
                crouchPostProcessing.SetActive(true);
            }
            else
            {
                StartCoroutine(CrouchTransition(originalHeight));
                speed = 10;
                crouchPostProcessing.SetActive(false);
            }
        }
    }

    IEnumerator CrouchTransition(float targetHeight)
    {
        float elapsedTime = 0f;
        float startHeight = controller.height;

        while (elapsedTime < crouchSmoothTime)
        {
            controller.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime / crouchSmoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
    }

    private void DecreaseStamina(float amount)
    {
        currentStamina -= amount;

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        UpdateStaminaBar();
    }

    private void RechargeStamina(float amount)
    {
        currentStamina += amount;

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        float fillAmount = currentStamina / maxStamina;
        staminaSlider.value = fillAmount;
    }
}


// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [Header("Settings")]
//     public float gravity = -9.81f;
//     public int jumpPower = 5;
//     public int speed = 10;

//     [Header("Gravity")]
//     public Transform groundCheck;
//     public LayerMask mask;
//     bool isGrounded;

//     [Header("Crouch")]
//     public float crouchHeight = 0.5f;
//     public float originalHeight;
//     public float crouchSmoothTime = 0.2f;
//     public float standingSpeed = 10f;

//     private bool isCrouching = false;
//     private CharacterController controller;
//     private Vector3 velocity;
//     public GameObject crouchPostProcessing;

//     [Header("Sprinting")]
//     public int runningSpeed;
//     private int originalSpeed = 10;
//     bool shouldSprint = true;


//     // [Header("Prone")]
//     // public float proneHeight = 0.3f;
//     // public float timeToProne = 2f;
//     // private bool isProne = false;
//     // private bool canProne = true;
//     // private float controlPressTime = 0f;

//     [Header("Stamina")]
//     public Slider staminaSlider;
//     public float maxStamina = 100f;
//     public float staminaDecreaseRate = 10f;
//     public float staminaRechargeRate = 5f;

//     private float currentStamina;

//     private void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         originalHeight = controller.height;
//         currentStamina = maxStamina;
//         UpdateStaminaBar();
//     }

//     private void Update()
//     {
//         Movement();
//         Sprinting();
//         HandleCrouch();
//         //HandleProne();

//         bool isRunning = Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));

//         if (isRunning)
//         {
//             DecreaseStamina(staminaDecreaseRate * Time.deltaTime);
//         }
//         else
//         {
//             RechargeStamina(staminaRechargeRate * Time.deltaTime);
//         }
//     }

//     void Movement()
//     {
//         isGrounded = Physics.CheckSphere(groundCheck.position, .5f, mask);
//         float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

//         Vector3 move = transform.forward * vertical + transform.right * horizontal;
//         controller.Move(move);

//         if (isGrounded && velocity.y < 0)
//         {
//             velocity.y = -2f;
//         }

//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             velocity.y = jumpPower;
//         }

//         velocity.y += gravity * Time.deltaTime;
//         controller.Move(velocity * Time.deltaTime);
//     }

//     void Sprinting()
//     {
//         if (!isCrouching)
//         {
//             if (Input.GetKey(KeyCode.LeftShift))
//             {
//                 speed = runningSpeed;
//             }
//             else
//             {
//                 speed = originalSpeed;
//             }
//         }
//     }

//   void HandleCrouch()
// {
//     if (Input.GetKeyDown(KeyCode.LeftControl) && !isProne)
//     {
//         isCrouching = !isCrouching;

//         if (isCrouching)
//         {
//             StartCoroutine(CrouchTransition(crouchHeight));
//             speed = 6;
//             crouchPostProcessing.SetActive(true);
//         }
//         else
//         {
//             StartCoroutine(CrouchTransition(originalHeight));
//             speed = 10;
//             crouchPostProcessing.SetActive(false);
//         }
//     }
// }

// IEnumerator CrouchTransition(float targetHeight)
// {
//     float elapsedTime = 0f;
//     float startHeight = controller.height;

//     while (elapsedTime < crouchSmoothTime)
//     {
//         controller.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime / crouchSmoothTime);
//         elapsedTime += Time.deltaTime;
//         yield return null;
//     }

//     controller.height = targetHeight;
// }

//     private void DecreaseStamina(float amount)
//     {
//         currentStamina -= amount;

//         if (currentStamina < 0)
//         {
//             currentStamina = 0;
//         }

//         UpdateStaminaBar();
//     }

//     private void RechargeStamina(float amount)
//     {
//         currentStamina += amount;

//         if (currentStamina > maxStamina)
//         {
//             currentStamina = maxStamina;
//         }

//         UpdateStaminaBar();
//     }

//     private void UpdateStaminaBar()
//     {
//         float fillAmount = currentStamina / maxStamina;
//         staminaSlider.value = fillAmount;
//     }
// }

