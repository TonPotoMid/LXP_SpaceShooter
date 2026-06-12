using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Définition des états possibles de l'I.A.
    public enum State
    {
        Roaming, // Mode patrouille
        Chasing  // Mode poursuite
    }

    [Header("Configuration")]
    public NavMeshAgent agent;
    public GameObject player;
    public float distanceDetection = 8f; 

    private Vector3 origine = Vector3.zero;
    private Vector3 pointA = new Vector3(10f, 0f, 100f);
    private Vector3 pointB = new Vector3(250f, 0f, 150f);
    private Vector3 pointC = new Vector3(250f, 0f, 60f);

    private List<Vector3> points = new List<Vector3>();
    private int indexActuel = 0;

    // État actuel de l'I.A. 
    private State etatActuel = State.Roaming;

    void Start()
    {
        points.Add(origine);
        points.Add(pointA);
        points.Add(pointB);
        points.Add(pointC);

       
        if (points.Count > 0)
        {
            indexActuel = Random.Range(0, points.Count);
            agent.SetDestination(points[indexActuel]);
        }
    }

    void Update()
    {
        
        CheckHandler();

    
        UpdateState();
    }

 
    void UpdateState()
    {
        switch (etatActuel)
        {
            case State.Roaming:
                RoamingBehavior();
                break;

            case State.Chasing:
                ChasingBehavior();
                break;
        }
    }

    void CheckHandler()
    {
        if (player == null) return;

 
        float distanceAuJoueur = Vector3.Distance(transform.position, player.transform.position);

    
        if (distanceAuJoueur <= distanceDetection)
        {
            etatActuel = State.Chasing;
        }
  
        else
        {
            
            if (etatActuel == State.Chasing)
            {
                etatActuel = State.Roaming;
                agent.SetDestination(points[indexActuel]);
            }
        }
    }

    void RoamingBehavior()
    {
        if (points.Count <= 1) return; 

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int nouvelIndex = indexActuel;

            while (nouvelIndex == indexActuel)
            {
               
                nouvelIndex = Random.Range(0, points.Count);
            }

       
            indexActuel = nouvelIndex;
            agent.SetDestination(points[indexActuel]);
        }
    }

    void ChasingBehavior()
    {
        if (player == null) return;


        agent.SetDestination(player.transform.position);


        if (!agent.pathPending && agent.remainingDistance <= 1.5f)
        {
            print("Touché !");
        }
    }
}