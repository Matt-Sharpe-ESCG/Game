using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    public Transform[] aiPoints;

    private NavMeshAgent nav;
    private int destPoint;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (aiPoints.Length == 0)
        {
            return;
        }
        nav.destination = aiPoints[destPoint].position;
        destPoint = (destPoint + 1) % aiPoints.Length;
    }
}
