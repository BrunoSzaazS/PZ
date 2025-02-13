using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerManager
{
    public float moveSpeed = 5f; // Velocidade de movimento normal
    public float runSpeed = 8f; // Velocidade de corrida
    public float groundCheckDistance = 0.5f; // Distância para verificar o chão
    public LayerMask groundMask; // Camada do chão

    private Rigidbody rb;
    private bool isGrounded;
    private Vector2 moveInput; // Entrada de movimento (Input System)
    private bool isRunning; // Verifica se o jogador está correndo

    void Start()
    {
        // Obtém o componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verifica se o jogador está no chão
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }

    void FixedUpdate()
    {
        // Move o jogador (usando FixedUpdate para física)
        MoveJogador();
    }

    // Método para capturar entrada de movimento (Input System)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Método para capturar entrada de corrida (Input System)
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true; // Começa a correr
        }
        else if (context.canceled)
        {
            isRunning = false; // Para de correr
        }
    }

    // Método para mover o jogador
    private void MoveJogador()
    {
        float speed = isRunning ? runSpeed : moveSpeed; // Define a velocidade com base no estado de corrida
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * speed;

        // Aplica o movimento ao Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    // Método para correr (pode ser chamado separadamente, se necessário)
    private void CorrerJogador()
    {
        isRunning = true;
    }

    // Método para parar de correr (pode ser chamado separadamente, se necessário)
    private void PararCorrida()
    {
        isRunning = false;
    }
}