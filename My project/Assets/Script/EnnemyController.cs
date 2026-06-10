using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [Header("Déplacement")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float obstacleDetectionDistance = 1f;

    [Header("Détection du Joueur")]
    [SerializeField] private float playerDetectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer; // Permet de cibler uniquement le joueur

    private Vector3 currentDirection;
    private Transform playerTransform;

    // États de l'IA
    private bool isChasing = false;
    private float stateTimer = 0f;

    void Start()
    {
        ChooseRandomDirection();
    }

    void Update()
    {
        // On incrémente le chronomètre avec le temps réel écoulé
        stateTimer += Time.deltaTime;

        if (isChasing)
        {
            ExecuteChaseState();
        }
        else
        {
            ExecutePatrolState();
        }
    }

    // --- COMPORTEMENT DE PATROUILLE (ALÉATOIRE) ---
    void ExecutePatrolState()
    {
        // Déplacement continu dans la direction actuelle
        transform.position += currentDirection * speed * Time.deltaTime;

        // Condition 1 : Changement de direction toutes les 5 secondes
        // Condition 2 : Changement si un obstacle (mur) est détecté devant
        if (stateTimer >= 5f || Physics.Raycast(transform.position, currentDirection, obstacleDetectionDistance))
        {
            ChooseRandomDirection();
        }

        // Vérification constante si le joueur est à proximité
        CheckForPlayer();
    }

    void ChooseRandomDirection()
    {
        stateTimer = 0f; // Réinitialise le chrono pour les 20 prochaines secondes

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        currentDirection = directions[Random.Range(0, directions.Length)];
        transform.rotation = Quaternion.LookRotation(currentDirection);
    }

    // --- COMPORTEMENT DE TRAQUE ---
    void CheckForPlayer()
    {
        // Crée une sphère invisible autour de l'ennemi pour détecter le joueur
        Collider[] targetsInMinusRadius = Physics.OverlapSphere(transform.position, playerDetectionRadius, playerLayer);

        if (targetsInMinusRadius.Length > 0)
        {
            // Joueur trouvé ! On passe en mode traque
            playerTransform = targetsInMinusRadius[0].transform;
            isChasing = true;
            stateTimer = 0f; // Réinitialise le chrono pour les 40 secondes de traque
        }
    }

    void ExecuteChaseState()
    {
        // Si les 40 secondes sont écoulées, on arrête de suivre et on reprend la patrouille
        if (stateTimer >= 40f)
        {
            isChasing = false;
            ChooseRandomDirection();
            return;
        }

        if (playerTransform != null)
        {
            // Calcul de la direction vers le joueur (uniquement sur le plan horizontal X et Z)
            Vector3 directionToPlayer = (playerTransform.position - transform.position);
            directionToPlayer.y = 0f; // Évite que l'ennemi ne s'incline si le joueur saute
            directionToPlayer.Normalize();

            // Déplacement vers le joueur
            transform.position += directionToPlayer * speed * Time.deltaTime;

            // Rotation pour faire face au joueur
            if (directionToPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
    }

    // Visualisation de la zone de détection dans l'éditeur Unity (Pratique pour débugger !)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}