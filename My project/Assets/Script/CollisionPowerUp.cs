using UnityEngine;

public class CollisionPowerUp : MonoBehaviour
{
    // OPTION A : Si la case "Is Trigger" du Collider de ton Power-Up est COCHÉE (Recommandé)
    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est bien le joueur qui passe sur le Power-Up
        if (other.CompareTag("Player"))
        {
            print("Le Joueur a ramassé le Power-Up !");

            // On détruit l'objet sur lequel est attaché ce script (le Power-Up lui-même)
            Destroy(gameObject);
        }
    }

    // OPTION B : Si la case "Is Trigger" est DÉCOCHÉE (Collision physique solide)
    private void OnCollisionEnter(Collision collision)
    {
        // On vérifie si l'objet qui nous rentre dedans est le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Le Joueur a percuté le Power-Up !");

            // On détruit l'objet sur lequel est attaché ce script (le Power-Up lui-même)
            Destroy(gameObject);
        }
    }
}