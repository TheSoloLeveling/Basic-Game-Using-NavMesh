using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 15; // wait before seek the next destination
    public float aggroRange = 10; // distance where the NPC increase speed and seek the player
    public Transform[] waypoints; // array of waypoints of the NPC patrol

    int index; // current waypoint index
    float speed, agentSpeed; // current agent speed and NavMeshAgent speed
    Transform player; // reference of the player transform

    Animator animator; // Animator of NPC
    NavMeshAgent agent; // NavMeshAgent of NPc

    [Header("Use for Debugging Aggro Radius")]  // put a bolded message that describe the variables
    public bool showAggro = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agentSpeed = agent.speed; // setting the initial speed of the agent
        }

        player = GameObject.FindGameObjectWithTag("Player").transform; // acceding the player transform via a tag
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f); // Reapeat the tick method every half second
                                          // Check the player if it's static 

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);  //invoke the patrol method for every patrolTime,the random range is the delay for the start of the cycle
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1; // If the index is the last index of the waypoints array set index at 0 if not Increment index by 1
    }

    void Tick()
    {
        agent.destination = waypoints[index].position; // set the destination of the agent to the current waypoint position
        agent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (showAggro)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }

        else
        {
            return;
        }
       
    }

}
