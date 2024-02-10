using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;

public class PlayerMovement : NetworkBehaviour {
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

    [Header("Sprinting")]
    public int runningSpeed;
    private int originalSpeed = 10;
    bool shouldSprint = true;
    public float sprintCooldown = 5f;
    private float sprintCooldownTimer;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 10f;
    public float staminaRechargeRate = 5f;
    private float currentStamina;
    private Slider stamina;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public DamageIndecator damageIndicator;
    public GameObject deathScreen;

    [Header("Questing")]
    public Quest quest;
    public List<Quest> activeQuests = new List<Quest>();

    private void Awake() {
        if (!IsOwner) return;
    }
    private void Start() 
    {

        GameObject staminaObject = GameObject.Find("StaminaBar");

        if (staminaObject != null)
        {
            stamina = staminaObject.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("StaminaBar GameObject not found!");
        }

        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        UpdateStaminaBar();
    }

    private void Update() {
        Movement();
        Sprinting();
        HandleCrouch();

        // Taking player Health TEST

        if (Input.GetKeyDown(KeyCode.R)) {
            TakeDamage(1);

        }
    }

    void Movement() {
        isGrounded = Physics.CheckSphere(groundCheck.position, .5f, mask);
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.Move(move);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            velocity.y = jumpPower;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Sprinting() {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));

        if (!isCrouching) {
            if (isRunning && shouldSprint) {
                if (currentStamina > 0) {
                    speed = runningSpeed;
                    DecreaseStamina(staminaDecreaseRate * Time.deltaTime);
                } else {
                    shouldSprint = false;
                    speed = originalSpeed;
                    sprintCooldownTimer = sprintCooldown; // Start cooldown
                }
            } else {
                if (currentStamina < maxStamina) {
                    RechargeStamina(staminaRechargeRate * Time.deltaTime);
                } else if (sprintCooldownTimer > 0) {
                    sprintCooldownTimer -= Time.deltaTime;
                } else {
                    shouldSprint = true;
                }

                speed = originalSpeed;
            }
        }
    }

    void HandleCrouch() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            isCrouching = !isCrouching;

            if (isCrouching) {
                StartCoroutine(CrouchTransition(crouchHeight));
                speed = 5;
            } else {
                StartCoroutine(CrouchTransition(originalHeight));
                speed = 10;
            }
        }
    }

   public void TakeDamage(int dmg) {
    currentHealth -= dmg;

    if (currentHealth <= 0) 
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }

    foreach (Quest quest in activeQuests) 
    {
        foreach (QuestGoal goal in quest.goals) 
        {
            goal.Damage();
            if (goal.IsReached()) 
            {
                Debug.Log($"Quest goal '{goal.goalType}' complete!");
                quest.Complete();
            }
        }
    }

    damageIndicator.UpdateHealth(currentHealth, maxHealth);
}

    IEnumerator CrouchTransition(float targetHeight) 
    {
        float elapsedTime = 0f;
        float startHeight = controller.height;

        while (elapsedTime < crouchSmoothTime) {
            controller.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime / crouchSmoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
    }

    private void DecreaseStamina(float amount) {
        currentStamina -= amount;

        if (currentStamina < 0) {
            currentStamina = 0;
        }

        UpdateStaminaBar();
    }

    private void RechargeStamina(float amount) {
        currentStamina += amount;

        if (currentStamina > maxStamina) {
            currentStamina = maxStamina;
        }

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar() {
        float fillAmount = currentStamina / maxStamina;
        if (stamina) {
            stamina.value = fillAmount;
        }
    }

    
}
