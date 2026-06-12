using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject powerUpPrefab; // Le Prefab du Power-Up à glisser ici
    public float cooldown = 20f;     // Le temps d'attente entre chaque vague de bonus (20 secondes)

    // Tableau contenant tes 3 coordonnées X, Y, Z spécifiques
    private Vector3[] pointsDeSpawn = new Vector3[]
    {
        new Vector3(130f, 0f, 160f),
        new Vector3(13f, 0f, -35f),
        new Vector3(250f, 0f, -60f)
    };

    void Start()
    {
        // On lance la Coroutine au démarrage du jeu
        StartCoroutine(GestionnairePowerUp());
    }

    private IEnumerator GestionnairePowerUp()
    {
        // Boucle infinie qui s'exécute tout au long de la partie
        while (true)
        {
            // 1. On attend 20 secondes avant de faire apparaître les bonus
            yield return new WaitForSeconds(cooldown);

            if (powerUpPrefab != null)
            {
                // 2. On parcourt TOUT le tableau de positions grâce à une boucle foreach
                foreach (Vector3 spawnPos in pointsDeSpawn)
                {
                    // 3. On fait apparaître un exemplaire du Power-Up sur la position actuelle de la boucle
                    Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
                }

                print("3 Power-Ups ont été générés simultanément sur la carte !");
            }
        }
    }
}