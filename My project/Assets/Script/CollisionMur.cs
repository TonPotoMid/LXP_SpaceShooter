using UnityEngine;

public class CollisionMur : MonoBehaviour
{
    // Plus besoin des méthodes Start() et Update() vides.
    // La physique d'Unity (Collider + Rigidbody) gère automatiquement le blocage !

    private void OnCollisionEnter(Collision collision)
    {
        // Optionnel : Permet de vérifier dans la console si le joueur a bien percuté le mur
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Le joueur a percuté un mur invisible / obstacle !");
        }
    }
}

