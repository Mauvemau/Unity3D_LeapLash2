using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Event Broadcasters")]
    [SerializeField] private Vector3EventChannel movementChannel;
    [SerializeField] private Vector2EventChannel attackChannel;

    private GameInputActions input;

    private void OnMove()
    {
        if (movementChannel != null)
        {
            Vector3 direction = new Vector3
                    (input.Player.Movement.ReadValue<Vector2>().x, 0,
                        input.Player.Movement.ReadValue<Vector2>().y);

            movementChannel.RaiseEvent(direction);
        }
    }

    private void OnAttack()
    {
        if (input.Player.Attack.IsPressed())
        {
            Vector2 direction = input.Player.Attack.ReadValue<Vector2>();

            attackChannel.RaiseEvent(direction);
        }
    }

    private void Update()
    {
        OnMove();
        OnAttack();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }
    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Awake()
    {
        input = new GameInputActions();
    }
}
