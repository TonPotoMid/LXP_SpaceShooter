using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvements")]
    public float speed = 5f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 100f;

    [Header("Inputs InputSystem")]
    public InputActionReference moveAction;
    public InputActionReference shootAction;

    [Header("Prefabs & États")]
    public GameObject bulletPrefab;

    public Rigidbody rb;

    private Vector2 oldInput;
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

        if (oldInput != null && Vector2.Dot(moveInput, oldInput) < 0.45)
        {
         //   rb.linearVelocity = Vector3.zero;
        }
        oldInput = moveInput;

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 nouvellePosition = rb.position + direction * speed * Time.deltaTime;
        //rb.MovePosition(nouvellePosition);
        rb.AddForce(direction * speed, ForceMode.VelocityChange);

        if (moveInput.magnitude < 0.1f)
            rb.linearVelocity = Vector3.zero;

        //if (rb.linearVelocity.magnitude > maxSpeed) rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // --- 2. DETECTION DU TIR CONDITIONNEL ---
        // Le joueur doit appuyer sur la touche ET posséder au moins 1 munition
        if (shootAction.action.WasPressedThisFrame() && munitions > 0 && bulletPrefab != null)
        {
            Tirer();
        }
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed) rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

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