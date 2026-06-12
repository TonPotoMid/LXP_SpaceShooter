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

    public Rigidbody rb;

    
    private int munitions = 5;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // --- 1. DEPLACEMENT (ZQSD) ---
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 nouvellePosition = rb.position + direction * speed * Time.deltaTime;
        rb.MovePosition(nouvellePosition);

        // --- 2. DETECTION DU TIR CONDITIONNEL ---
        // Le joueur doit appuyer sur la touche ET posséder au moins 1 munition
        if (shootAction.action.WasPressedThisFrame() && munitions > 0 && bulletPrefab != null)
        {
            Tirer();
        }
    }

    void Tirer()
    {
        // Fait apparaître le projectile
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

        // On consomme une munition
        munitions--;
        print("Balle tirée ! Munitions restantes : " + munitions);

        if (munitions == 0)
        {
            print("Plus de munitions ! Arme désactivée.");
        }
    }

    // --- 3. GESTION DES COLLISIONS ---
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("powerUp"))
        {
            
            munitions += 5;
            print("Power-Up récupéré ! +10 munitions ajoutées. Total : " + munitions);

            
            Destroy(collision.gameObject);
        }
    }
}