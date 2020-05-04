using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshJumper : MonoBehaviour
{
    private IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;  // set to false to use specific way to traverse off-mesh links
        while(true)
        {
            if (agent.isOnOffMeshLink)  // to check if the agent is positionned on a OffMeshLink
            {
                yield return StartCoroutine(Parabola(agent, 2f, 0.5f));
                agent.CompleteOffMeshLink(); // agent will pause at an off-mesh link until this function is called
            }
            yield return null;
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData; // set the state of the OffMeshLink to the current state
        Vector3 startPos = agent.transform.position; // Link start world position
        Vector3 endPos = data.endPos; // Link end world position
        float time = 0f;
        while(time < 1f)
        {
            float yOffset = height * (time - time * time);  // parabol function
            agent.transform.position = Vector3.Lerp(startPos, endPos, time) + yOffset * Vector3.up;
            time += Time.deltaTime / duration;
            
            yield return null;
        }
    }
}


