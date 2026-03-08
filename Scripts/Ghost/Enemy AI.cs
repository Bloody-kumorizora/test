using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using CatNight.Utils;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;

public class EnemyAI : MonoBehaviour {
    [Header("Detection")]
    [SerializeField] private float _chaseDistance = 4f;
    [SerializeField] private bool _alwaysChase = true;

    [Header("Roaming")]
    [SerializeField] private float _roamingDistanceMax = 10f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;
    [SerializeField] private bool _flipByScale = true;
    
    private NavMeshAgent _agent;
    private float _roamingTimer;
    private Vector3 _startPosition;

    private enum State {
        Roaming,
        Chasing
    }

    private State _state = State.Roaming;


    public bool IsRunning
    {
        get
        {
            if (_agent == null) return false;
            return _agent.velocity.magnitude > 0.1f;
        }
    }

    public float GetRoamingAnimationSpeed()
    {
        if (_agent == null) return 0f;
        return _agent.velocity.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ShowGameOver();
        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _startPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(
            transform.position,
            MovePlayer.Instance.transform.position
        );


        if (_alwaysChase || distanceToPlayer <= _chaseDistance)
            _state = State.Chasing;
        else
            _state = State.Roaming;

        switch (_state)
        {
            case State.Chasing:
                ChasePlayer();
                break;

            case State.Roaming:
                Roaming();
                break;
        }
        FlipTowardsMovement();
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(MovePlayer.Instance.transform.position);
    }


    private void Roaming()
    {
        _roamingTimer -= Time.deltaTime;

        if (_roamingTimer <= 0f)
        {
            Vector3 randomDir = Random.insideUnitSphere;
            randomDir.y = 0;

            float distance = Random.Range(_roamingDistanceMin, _roamingDistanceMax);

            Vector3 roamPosition = _startPosition + randomDir.normalized * distance;

            _agent.SetDestination(roamPosition);

            _roamingTimer = _roamingTimerMax;
        }
    }
    private void FlipTowardsMovement()
    {
        if (_agent == null) return;

        float moveX = _agent.velocity.x;

        if (Mathf.Abs(moveX) < 0.01f)
            return;

        if (_flipByScale)
        {
            Vector3 scale = transform.localScale;

            if (moveX > 0)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
    }
}