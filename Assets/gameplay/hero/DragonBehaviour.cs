using UnityEngine;
using UnityEngine.AI;

public class DragonBehaviour : MonoBehaviour
{
    public Transform target;
    public float aggresionRange = 8f;
    public float attackRange = 2f;
    private NavMeshAgent agent;

    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        bool isInAggressionRange = Vector3.Distance(transform.position, target.position) < aggresionRange;
        
        if(isInAggressionRange) {
            bool isInAttackRange = Vector3.Distance(transform.position, target.position) < attackRange;
            Debug.Log("Distance: " + Vector3.Distance(transform.position, target.position));
            
            if(!isInAttackRange) {
                agent.SetDestination(target.position);
                agent.isStopped = false;
            } else {
                agent.isStopped = true;
                // attack
            }
        }
    }
}
