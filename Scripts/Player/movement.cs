
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour {

    public static MovePlayer Instance { get; private set; }

    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private int _maxHealth = 2;
    

    private Rigidbody2D rb;

    private float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private int _currentHealth;
    private bool _isAlive;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Die()
    {
        _isAlive = false;

        rb.linearVelocity = Vector2.zero;
        enabled = false;

        Debug.Log("Игрок умер");
    }
    

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        Vector2 movement = inputVector.normalized * _moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        _isRunning = inputVector.magnitude > _minMovingSpeed;

        FlipPlayer(inputVector.x);
    }

    public void TakeDamage(int damage)
    {
        if (!_isAlive) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void FlipPlayer(float moveX)
    {
        if (Mathf.Abs(moveX) < 0.01f)
            return;

        Vector3 scale = transform.localScale;

        if (moveX > 0)
            scale.x = -Mathf.Abs(scale.x); 
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
}