using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerManager, IJogadorMove
{
    public float velocidadeMovimento = 5f; // Velocidade de movimento normal
    public float velocidadeCorrida = 8f; // Velocidade de corrida
    public float groundCheckDistance = 0.5f; // Distância para verificar o chão
    public LayerMask groundMask; // Camada do chão

    private Rigidbody rb;
    private bool isGrounded;
    private Vector2 moveInput; // Entrada de movimento (Input System)
    private bool estaCorrendo; // Verifica se o jogador está correndo

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
    public void OnMovimento(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Método para capturar entrada de corrida (Input System)
    public void OnCorrida(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            estaCorrendo = true; // Começa a correr
        }
        else if (context.canceled)
        {
            estaCorrendo = false; // Para de correr
        }
    }

    // Método para mover o jogador
    private void MoveJogador()
    {
        float velocidade = estaCorrendo ? velocidadeCorrida : velocidadeMovimento; // Define a velocidade com base no estado de corrida
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * velocidade;

        // Aplica o movimento ao Rigidbody
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    // Método para correr (pode ser chamado separadamente, se necessário)
    private void CorrerJogador()
    {
        estaCorrendo = true;
    }

    // Método para parar de correr (pode ser chamado separadamente, se necessário)
    private void PararCorrida()
    {
        estaCorrendo = false;
    }
}