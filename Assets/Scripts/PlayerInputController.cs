using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel movementChannel;
    [SerializeField] private VoidEventChannel attackChannel;

    private GameInputActions input;

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (attackChannel != null)
        {
            attackChannel.RaiseEvent();
        }
    }

    private void Update()
    {
        if (movementChannel != null)
        {
            Vector3 direction = new Vector3
                    (input.Player.Movement.ReadValue<Vector2>().x, 0,
                        input.Player.Movement.ReadValue<Vector2>().y);

            movementChannel.RaiseEvent(direction);
        }
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Attack.performed += OnAttack;
    }
    private void OnDisable()
    {
        input.Player.Disable();
        input.Player.Attack.performed -= OnAttack;
    }

    private void Awake()
    {
        input = new GameInputActions();
    }
}
