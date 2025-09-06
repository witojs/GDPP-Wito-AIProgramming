using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
    }
 
    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        if (!_isMoving)
        {
            _isMoving = true;
            int index = Random.Range(0, enemy.Waypoints.Count);
            _destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }
 
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
