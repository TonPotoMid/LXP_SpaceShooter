using System.Collections;
using System.Collections.Generic; // Obligatoire pour utiliser les List
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [Header("Paramètres de l'Ennemi")]
    public GameObject ennemyPreFab;
    public float cooldown; 
    public float spawnMin, spawnMax;

    [Header("Gestion des Manches")]
    public int manche = 1;
    public float durreeManche;

    // Liste pour garder une trace des ennemis en vie et les nettoyer à la fin
    private List<GameObject> ennemisActuels = new List<GameObject>();

    void Start()
    {
        // On lance le gestionnaire de jeu principal
        StartCoroutine(GestionnaireDeManches());
    }

    // Boucle principale qui gère le déroulement du jeu
    private IEnumerator GestionnaireDeManches()
    {
        while (true)
        {
            print("Début de la manche : " + manche);

            // 1. On fait apparaître X entités (où X = numéro de la manche)
            yield return StartCoroutine(SpawnEnnemisPourManche(manche));

            // 2. On attend que le temps de la manche soit écoulé
            yield return new WaitForSeconds(durreeManche);

            // 3. Le temps est écoulé : fin de la manche
            print("Manche Terminée");

            // 4. On nettoie la carte
            ClearEntity();

            // 5. On passe à la manche suivante
            manche += 1;

            // Optionnel : un petit temps de pause de 2 secondes avant que la manche suivante ne commence
            yield return new WaitForSeconds(2f);
        }
    }

    // Coroutine qui fait apparaître précisément "nombreDEnnemis" avec le cooldown entre chaque
    private IEnumerator SpawnEnnemisPourManche(int nombreDEnnemis)
    {
        for (int i = 0; i < nombreDEnnemis; i++)
        {
            Vector3 spawnPos = transform.position + Vector3.right * Random.Range(spawnMin, spawnMax);

            // On crée l'ennemi et on le garde en mémoire dans une variable temporaire
            GameObject nouvelEnnemi = Instantiate(ennemyPreFab, spawnPos, Quaternion.identity);

            // On l'ajoute à notre liste pour pouvoir le détruire plus tard
            ennemisActuels.Add(nouvelEnnemi);

            // On attend le cooldown avant de faire apparaître le suivant
            yield return new WaitForSeconds(cooldown);
        }
    }

    // Fonction qui détruit tous les ennemis restants sur la carte
    private void ClearEntity()
    {
        // On parcourt la liste de tous les ennemis créés
        foreach (GameObject ennemi in ennemisActuels)
        {
            // On vérifie si l'ennemi existe toujours (au cas où le joueur l'aurait déjà tué)
            if (ennemi != null)
            {
                Destroy(ennemi);
            }
        }

        // On vide complètement la liste pour la manche suivante
        ennemisActuels.Clear();
    }
}