using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollerV2 : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    bool patrolling;
    bool arrived;

    public Transform[] patrolTargets;
    public Transform target;
    public Transform eye;

    Vector3 lastKnownPosition;

    private int destPoint;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastKnownPosition = transform.position;
    }

    bool CanSeeTarget()
    {
        bool canSee = false;
        Ray ray = new Ray(eye.position, target.transform.position - eye.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform != target)
            {
                canSee = false;
            }
            else
            {
                lastKnownPosition = target.transform.position;
                canSee = true;
            }
        }

        return canSee;
    }

    void Update()
    {
        if (agent.pathPending) // if the agent is still trying to figure out where to go 
        {
            return;
        }

        if (patrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!arrived)
                {
                    arrived = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            else
            {
                arrived = false;
            }
        }
        if (CanSeeTarget())
        {
            agent.SetDestination(target.transform.position);
            patrolling = false;
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            if (!patrolling)
            {
                agent.SetDestination(lastKnownPosition);
                if(agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
        }
        anim.SetFloat("Forward", agent.velocity.sqrMagnitude);
    }

    IEnumerator GoTonextPoint()
    {
        if(patrolTargets.Length == 0)
        {
            yield break;
        }
        patrolling = true;
        yield return new WaitForSeconds(2f);
        arrived = false;
        agent.destination = patrolTargets[destPoint].position;
        destPoint = (destPoint + 1) % patrolTargets.Length;  // % to cycle destPoint from 0
    }
}
