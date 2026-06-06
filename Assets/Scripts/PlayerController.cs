using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public InputActionReference move;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 _moveDirection;

    [Header("Interaction")]
    public InputActionReference interact;
    private TorchMono currentTorch;

    private void OnEnable()
    {
        if (interact != null)
            interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        if (interact != null)
            interact.action.performed -= OnInteract;
    }

    private void Update()
    {
        if (move != null && move.action != null)
        {
            _moveDirection = move.action.ReadValue<Vector2>();
        }
        else
        {
            _moveDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = _moveDirection * moveSpeed;
        }
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentTorch != null)
        {
            currentTorch.Interact();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TorchMono>(out var torch))
        {
            currentTorch = torch;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TorchMono>(out var torch))
        {
            if (currentTorch == torch)
            {
                currentTorch = null;
            }
        }
    }
}

