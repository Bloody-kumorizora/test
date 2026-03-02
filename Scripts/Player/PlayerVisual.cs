using UnityEngine;

public class PlayerVisual : MonoBehaviour {
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IsRunning = "IsRunning";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(IsRunning, MovePlayer.Instance.IsRunning());
    }
}