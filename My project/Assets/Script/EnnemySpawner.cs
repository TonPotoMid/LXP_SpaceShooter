using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [Header("Paramètres de l'Ennemi")]
    public GameObject ennemyPreFab;
    public float cooldown;
    public float spawnMin, spawnMax;

    [Header("Gestion des Manches")]
    // RETRAIT de "public int manche = 1;" car on utilise celle de l'UI maintenant !
    [HideInInspector]
    public float durreeManche;

    private List<GameObject> ennemisActuels = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GestionnaireDeManches());
    }

    private IEnumerator GestionnaireDeManches()
    {
        while (true)
        {
            // On utilise UiController.manche à la place de la variable locale
            print("Début de la manche : " + UiController.manche);

            durreeManche = UiController.manche * 5f;

            // On fait apparaître le nombre d'ennemis basé sur la manche de l'UI
            yield return StartCoroutine(SpawnEnnemisPourManche(UiController.manche));

            yield return new WaitForSeconds(durreeManche);

            print("Manche Terminée");

            ClearEntity();

            // On augmente directement la variable static de l'UI !
            UiController.manche += 1;

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator SpawnEnnemisPourManche(int nombreDEnnemis)
    {
        for (int i = 0; i < nombreDEnnemis; i++)
        {
            Vector3 spawnPos = transform.position + Vector3.right * Random.Range(spawnMin, spawnMax);
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