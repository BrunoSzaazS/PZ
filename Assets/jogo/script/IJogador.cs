using UnityEngine;
using UnityEngine.InputSystem;
public interface IJogadorMove
{
    void OnMovimento(InputAction.CallbackContext context);
    void OnCorrida(InputAction.CallbackContext context);
}