using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chest")
        {
            Debug.Log("Enter");
            UIManager.Instance.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Chest")
        {
            Debug.Log("Exit");
            UIManager.Instance.gameObject.SetActive(true);
        }
    }
}
