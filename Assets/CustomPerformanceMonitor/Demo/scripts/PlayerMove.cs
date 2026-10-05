using UnityEngine;
using MiJuego.InputAdaptador;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public enum PlayerIndex
    {
        Player1,
        Player2
    }

    [Header("Player Identifier")]
    public PlayerIndex playerIndex = PlayerIndex.Player1;

    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private PlayerAnimation playerAnimation;
    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        bool isRunPressed = false;

        // Selección de ejes y botones según el Player seleccionado
        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                inputVector = CrossPlatformInputManager.GetMovement();
                isRunPressed = CrossPlatformInputManager.GetButtonDown("Run1") || CrossPlatformInputManager.GetButtonDown("Fire1");
                break;

            case PlayerIndex.Player2:
                inputVector = CrossPlatformInputManager.GetRotation();
                isRunPressed = CrossPlatformInputManager.GetButtonDown("Run2") || CrossPlatformInputManager.GetButtonDown("Fire2");
                break;
        }

        MoveAndRotate(inputVector.x, inputVector.y, isRunPressed);
    }

    void MoveAndRotate(float h, float v, bool isRunPressed)
    {
        // 1. Obtener dirección de movimiento
        Vector3 inputDirection = new Vector3(h, 0f, v).normalized;
        bool isMoving = inputDirection.magnitude >= 0.1f;

        // 2. Determinar si corre y la velocidad aplicada
        bool isRunning = isMoving && isRunPressed;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // 3. Orientación progresiva hacia el vector de entrada
        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 4. Dirección de movimiento en el mundo
        Vector3 moveDirection = inputDirection * currentSpeed;

        // 5. Aplicar gravedad constante
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // 6. Aplicar movimiento mediante CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // 7. Enviar parámetros al Animator
        if (playerAnimation != null)
        {
            playerAnimation.PlayAnim(h, v, isRunning);
        }
    }
}