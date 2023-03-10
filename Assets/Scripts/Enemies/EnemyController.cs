using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask groundLayerMask, playerLayerMask;

    //For Patrolling State
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //For Attacking State
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject projectilePos;

    //For Determining State
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);

        //Check for Patrol
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
        }
        //Check for Chase
        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        //Check for Attack
        if (playerInSightRange && playerInAttackRange)
        {
            Attack();
        }
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = walkPoint - transform.position;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayerMask))
        {
            walkPointSet = true;
        }
    }

    void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            Rigidbody bulletRB = Instantiate(projectile, projectilePos.transform.position, transform.rotation).GetComponent<Rigidbody>();
            bulletRB.AddForce(transform.forward * 30f, ForceMode.Impulse);
            Destroy(bulletRB, 2f);
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
