using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float idleTimeMin = 2f;
    public float idleTimeMax = 5f;

    private NavMeshAgent agent;
    private Animator animator;

    public float Health = 30;

    public GameObject ragdoll;

    private bool isRunning;

    private Inventory inventory;
    public GameObject bloodsplatter;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(Wander());
        isRunning = false;
        inventory = FindAnyObjectByType<Inventory>();
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Instantiate(bloodsplatter, transform);
        //Destroy(bloodsplatter, 2);
        if (!isRunning) 
        {
            agent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine(Wander());
        }
        isRunning = true;

        
        if (Health < 0)
        {
            inventory.AddItem(4, 2);
            Instantiate(ragdoll, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public IEnumerator Wander()
    {
        while (true)
        {
            agent.isStopped = false;
            Vector3 newDestination = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newDestination);
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }
            if (!isRunning)
            {
                float idleTime = Random.Range(idleTimeMin, idleTimeMax);
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                yield return new WaitForSeconds(idleTime);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", true);
            }
            else
            {
                agent.speed = 6;
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                
                yield return null;
            }
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
