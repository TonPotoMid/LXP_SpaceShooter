using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [Header("Paramètres de l'Ennemi")]
    public GameObject ennemyPreFab;
    public float cooldown;

    // RETRAIT de spawnMin et spawnMax car on utilise désormais des positions fixes !

    // Tableau contenant toutes tes coordonnées X, Y, Z spécifiques
    private Vector3[] pointsDeSpawn = new Vector3[]
    {
        new Vector3(130f, 6f, 0f),
        new Vector3(130f, 6f, 60f),
        new Vector3(70f, 6f, -60f),
        new Vector3(10f, 6f, 0f),
        new Vector3(10f, 6f, 60f),
        new Vector3(10f, 6f, 100f),
        new Vector3(130f, 6f, 100f),
        new Vector3(250f, 6f, 100f),
        new Vector3(250f, 6f, 60f),
        new Vector3(250f, 6f, 0f)
    };

    private List<GameObject> ennemisActuels = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GestionnaireDeManches());
    }

    private IEnumerator GestionnaireDeManches()
    {
        while (true)
        {
            print("Début de la manche : " + UiController.manche);

            UiController.durreeManche = UiController.manche * 5f;

            yield return StartCoroutine(SpawnEnnemisPourManche(UiController.manche));

            yield return new WaitForSeconds(UiController.durreeManche);

            print("Manche Terminée");

            ClearEntity();

            yield return new WaitForSeconds(2f);
            UiController.manche += 1;
        }
    }

    private IEnumerator SpawnEnnemisPourManche(int nombreDEnnemis)
    {
        for (int i = 0; i < nombreDEnnemis; i++)
        {
            // 1. On choisit un index au hasard entre 0 et la taille du tableau (exclus)
            int indexAleatoire = Random.Range(0, pointsDeSpawn.Length);

            // 2. On récupère le Vector3 correspondant à cet index
            Vector3 spawnPos = pointsDeSpawn[indexAleatoire];

            // 3. On fait apparaître l'ennemi à cette position fixe précise
            GameObject nouvelEnnemi = Instantiate(ennemyPreFab, spawnPos, Quaternion.identity);
            ennemisActuels.Add(nouvelEnnemi);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private void ClearEntity()
    {
        foreach (GameObject ennemi in ennemisActuels)
        {
            if (ennemi != null)
            {
                Destroy(ennemi);
            }
        }
        ennemisActuels.Clear();
    }
}