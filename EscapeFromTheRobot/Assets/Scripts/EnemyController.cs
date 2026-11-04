using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private List<Transform> Waypoints;
    int currentWaypointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
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
}
