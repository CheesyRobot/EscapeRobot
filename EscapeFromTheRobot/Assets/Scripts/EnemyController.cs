using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private List<Transform> Waypoints; // Potential Queue usage when room generation is made
    int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(Waypoints == null || Waypoints.Count == 0)
        {
            Debug.LogWarning("Waypoints list is empty or not assigned.");
        }
        else
        {
            agent.SetDestination(Waypoints[currentWaypointIndex].position);
        }
    }


    void Update()
    {
        if (Waypoints == null || Waypoints.Count == 0)
        { return; }

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            MoveToNextWaypoint();
        }
        
    }

    private void MoveToNextWaypoint()
    {
        if (Waypoints == null || Waypoints.Count == 0)
        { return; }

        currentWaypointIndex++;
        if (currentWaypointIndex >= Waypoints.Count)
        {
            currentWaypointIndex = 0;
        }

        agent.SetDestination(Waypoints[currentWaypointIndex].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered enemy trigger. Respawning player.");
            GameManager.Instance.RespawnPlayer();
        }
    }
}
