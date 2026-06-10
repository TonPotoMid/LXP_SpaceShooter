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
    public float distanceDetection = 8f; // Distance à laquelle l'I.A. repère le joueur

    private Vector3 origine = Vector3.zero;
    private Vector3 pointA = new Vector3(10f, 0f, 100f);
    private Vector3 pointB = new Vector3(250f, 0f, 150f);
    private Vector3 pointC = new Vector3(250f, 0f, 60f);

    private List<Vector3> points = new List<Vector3>();
    private int indexActuel = 0;

    // État actuel de l'I.A. (commence en patrouille)
    private State etatActuel = State.Roaming;

    void Start()
    {
        points.Add(origine);
        points.Add(pointA);
        points.Add(pointB);
        points.Add(pointC);

        // On choisit un premier point au hasard pour commencer
        if (points.Count > 0)
        {
            indexActuel = Random.Range(0, points.Count);
            agent.SetDestination(points[indexActuel]);
        }
    }

    void Update()
    {
        // 1. On vérifie constamment la distance avec le joueur pour changer d'état
        CheckHandler();

        // 2. On exécute le comportement lié à l'état actuel
        UpdateState();
    }

    // Gère la logique de chaque état
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

    // Scanne la distance entre l'agent et le joueur
    void CheckHandler()
    {
        if (player == null) return;

        // Calcul de la distance réelle entre l'agent et le joueur
        float distanceAuJoueur = Vector3.Distance(transform.position, player.transform.position);

        // Si le joueur est proche, on passe en mode Chasing (Poursuite)
        if (distanceAuJoueur <= distanceDetection)
        {
            etatActuel = State.Chasing;
        }
        // Sinon, on reste ou on retourne en mode Roaming (Patrouille)
        else
        {
            // Si on vient juste de perdre le joueur, on réinitialise la destination vers le point de patrouille actuel
            if (etatActuel == State.Chasing)
            {
                etatActuel = State.Roaming;
                agent.SetDestination(points[indexActuel]);
            }
        }
    }

    void RoamingBehavior()
    {
        if (points.Count <= 1) return; // Sécurité s'il n'y a pas assez de points

        // Si l'agent est arrivé à son point de patrouille actuel, il en choisit un autre au hasard
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int nouvelIndex = indexActuel;

            // Cette boucle "while" force l'I.A. à chercher un autre index tant qu'elle tombe sur le même point
            while (nouvelIndex == indexActuel)
            {
                // Random.Range(0, 4) va choisir un nombre entier entre 0 et 3 (le maximum est exclu)
                nouvelIndex = Random.Range(0, points.Count);
            }

            // On valide le nouvel index et on envoie l'agent
            indexActuel = nouvelIndex;
            agent.SetDestination(points[indexActuel]);
        }
    }

    void ChasingBehavior()
    {
        if (player == null) return;

        // L'agent suit la position du joueur en temps réel
        agent.SetDestination(player.transform.position);

        // Si l'agent est très proche du joueur (ex: moins de 1.5 unité métrique)
        if (!agent.pathPending && agent.remainingDistance <= 1.5f)
        {
            print("Touché !");
        }
    }
}