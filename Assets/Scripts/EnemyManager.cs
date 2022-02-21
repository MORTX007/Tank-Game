using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float health;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    [SerializeField] float attackRange;
    [SerializeField] float extendedAttackRange;
    [SerializeField] float stopRange;
    [SerializeField] bool playerInAttackRange;
    [SerializeField] bool playerInStopRange;
    public bool gotAttacked;

    [SerializeField] Transform cannonOrigin;
    [SerializeField] float range;
    [SerializeField] float damage;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInStopRange = Physics.CheckSphere(transform.position, stopRange, whatIsPlayer);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(cannonOrigin.position, cannonOrigin.right, out hit, 1000f);
        if (!playerInAttackRange) { Patroling(); }
        if (hit.transform == player && (playerInAttackRange || gotAttacked)) { AttackPlayer(); }
        if (hit.transform != player && (playerInAttackRange || gotAttacked)) { ChasePlayer(); }
    }

    private void Patroling()
    {
        if (!walkPointSet) { SearchWalkPoint(); }

        if (walkPointSet) { agent.SetDestination(walkPoint); }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer ()
    {
        if (!playerInStopRange) { agent.SetDestination(player.position); }
        else if (playerInStopRange) { agent.SetDestination(transform.position); }

        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);

        if (!alreadyAttacked)
        {
            RaycastHit hit;
            if (Physics.Raycast(cannonOrigin.position, cannonOrigin.right, out hit, range))
            {
                print("hey");
                PlayerController target = hit.transform.GetComponent<PlayerController>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0) { health -= damage; }
        if (health <=0) { Die(); }
    }

    void Die()
    {
        Destroy(gameObject, 1f);
    }
}
