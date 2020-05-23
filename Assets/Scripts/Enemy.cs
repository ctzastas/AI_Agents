using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    // Enemy data
    [SerializeField] private float SenseDistance = 3.0f;
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private Transform target;
    private float waitTime;
    // Destination paths for enemy
    [SerializeField] private Transform [] paths;
    private int destinationPath = 0;

    // State Machine for enemy
    private enum EnemyState {
        Idle,
        Patrol,
        Chase,
    }

    // Enemy state data
    private EnemyState state = EnemyState.Idle;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        // Disabling auto-braking allows for continuous movement
        agent.autoBraking = false;
        //waitTime = 10f;
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case EnemyState.Idle:{
                 // Make transition to Patrol state...
                 state = EnemyState.Patrol;
                 break;
            }
            case EnemyState.Patrol: {
                PatrolState();
                break;
            }
            case EnemyState.Chase: {
                ChaseState();
                break;
            }
        }
    }

    // Patrol state
    void PatrolState() {
        transform.position = Vector3.MoveTowards(transform.position, paths[destinationPath].position,
            patrolSpeed * Time.deltaTime);
            // Wait until next path
            waitTime -= Mathf.Round(Time.deltaTime * 100) / 100;
            if (waitTime <= 0) {
                GoToTheNextPath();
                Debug.Log(paths[destinationPath].name);
            } 
            // if player seen, make transition to Chase state...
            float distance = Vector3.Distance( transform.position, target.position);
            if (distance < SenseDistance) {
                state = EnemyState.Chase;
                agent.isStopped = false;
                SenseDistance *= 2;
                Debug.Log("Transition to Chase state...");
            }
    }

    // Chase State
    void ChaseState() {
        // Chase target
        agent.SetDestination(target.position);
        // if player lost, make transition to Patrol state...
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance >= SenseDistance) {
            agent.isStopped = true;
            state = EnemyState.Patrol;
            SenseDistance /= 2;
            Debug.Log("Transition to Patrol state...");
        }
    }

     void GoToTheNextPath() {
        if (paths.Length >= 1) {   
            // Set random destination of the agent          
            destinationPath = Random.Range(0, paths.Length);
            waitTime = 10f;
            agent.destination = paths[destinationPath].position;        
        }
    }

    // Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn.
    void OnDrawGizmos() {
        switch (state) {
            case EnemyState.Patrol:
                Gizmos.DrawWireSphere(transform.position, SenseDistance);
                break;
            case EnemyState.Chase:
                Gizmos.DrawWireSphere(transform.position, SenseDistance);
                break;
        }
    }
}