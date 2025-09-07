using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public List<Transform> Waypoints = new List<Transform>();
    [SerializeField] public float ChaseDistance;
    [SerializeField] public Player Player;
    
    [HideInInspector] public NavMeshAgent NavMeshAgent;
    [HideInInspector] public Animator Animator;
    
    private BaseState _currentState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    private bool _isDead = false;

    private void Start()
    {
        if(Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
    }
 
    private void Update()
    {
        if (_isDead) return;

        if(_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
    
    private void OnDestroy()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart -= StartRetreating;
            Player.OnPowerUpStop -= StopRetreating;
        }
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
    
    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }
 
    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }
    
    public void Dead()
    {
        NavMeshAgent.enabled = false;
        Animator.enabled = false; 
        Destroy(gameObject);
    }
}
