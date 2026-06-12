using UnityEngine;

public class GestionCollision : MonoBehaviour
{
    public GameObject player;
    // Cette fonction se déclenche au moment exact de l'impact
    private void OnCollisionEnter(Collision collision)
    {
        // 'collision.gameObject' représente l'objet qu'on vient de percuter
        print("Aïe ! Je viens de percuter : " + collision.gameObject.name);

        // Exemple : Si l'objet touché a le tag "Ennemi", on détruit notre objet
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(player);

        }
    }
}