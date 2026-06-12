using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvements")]
    public float speed = 5f;
    public float rotationSpeed = 100f;

    [Header("Inputs InputSystem")]
    public InputActionReference moveAction;
    public InputActionReference shootAction;

    [Header("Prefabs & États")]
    public GameObject bulletPrefab;

    // Cet interrupteur est faux par défaut : le joueur NE PEUT PAS tirer au début
    private bool peutTirer = false;

    void Start()
    {
        print(this.transform.position);
    }

    void Update()
    {
        // --- 1. DEPLACEMENT (ZQSD) ---
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        this.transform.position += direction * speed * Time.deltaTime;

        // --- 2. DETECTION DU TIR CONDITIONNEL ---
        // Le joueur doit appuyer sur la touche ET posséder le bonus (peutTirer == true)
        if (shootAction.action.WasPressedThisFrame() && peutTirer && bulletPrefab != null)
        {
            Tirer();
        }
    }

    void Tirer()
    {
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    }

    // --- 3. GESTION DES COLLISIONS ---
    private void OnCollisionEnter(Collision collision)
    {
        // Quand on touche le power-up
        if (collision.gameObject.CompareTag("powerUp"))
        {
            print("Power-Up récupéré ! Armement activé pour 10 secondes.");

            // On lance le compte à rebours de 10 secondes
            StartCoroutine(RoutinePowerUp());

            // On détruit le power-up visuel sur la carte
            Destroy(collision.gameObject);
        }
    }

    // Coroutine qui gère le temps du pouvoir
    private IEnumerator RoutinePowerUp()
    {
        peutTirer = true; // L'interrupteur s'allume : le joueur a le droit de tirer !

        // Le jeu attend précisément 10 secondes
        yield return new WaitForSeconds(10f);

        peutTirer = false; // Les 10 secondes sont passées, l'interrupteur s'éteint.
        print("Fin du Power-Up ! Arme désactivée.");
    }
}