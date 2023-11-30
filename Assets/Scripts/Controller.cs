using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    TurnManager turnManager;

    public void Test (InputAction.CallbackContext context) {
        if (!context.performed)
            return;

    }
}
