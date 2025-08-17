using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerInput _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Move.performed += OnActivateMove;
        _input.Player.Move.canceled += OnDeactivateMove;
        _input.Player.Fire.performed += OnFire;
        _input.Player.Sweep.performed += OnSweep;
    }

    private void OnDestroy()
    {
        _input.Disable();
        _input.Player.Move.performed -= OnActivateMove;
        _input.Player.Move.canceled -= OnDeactivateMove;
        _input.Player.Fire.performed -= OnFire;
        _input.Player.Sweep.performed -= OnSweep;
    }
    
    private void Update()
    {
        // Player朝向始终面向鼠标
        if (Camera.main == null) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mousePos - transform.position);
        dir.y = 0;
        dir = dir.normalized;
        transform.up = dir;
    }
    
    private void OnDie()
    {
        GameRunner.Instance.GameOver();
        Destroy(gameObject);
    }
    
    private void OnActivateMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var move = context.ReadValue<Vector2>();
        var velocity = _rb.velocity;
        velocity.x = move.x;
        velocity.z = move.y;
        velocity = velocity.normalized * 5.0f;
        _rb.velocity = velocity;
    }
    
    private void OnDeactivateMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _rb.velocity = Vector3.zero;
    }
    
    private void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }
    
    private void OnSweep(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    public void Init()
    {
    }
}
