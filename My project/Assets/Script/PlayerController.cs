using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Ajout d'une valeur par défaut pour éviter que l'objet soit immobile
    public float rotationSpeed = 100f;

    public InputActionReference moveAction;
    public InputActionReference shootAction;

    public GameObject bulletPrefab;

    void Start()
    {
        print(this.transform.position);
        // Optionnel : Vous aviez un tir au démarrage ici, je l'ai laissé si c'est voulu.
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        // --- DEPLACEMENT ---
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        this.transform.position += direction * speed * Time.deltaTime;

        // --- ROTATION (Touches M et K) ---
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.M))
        {
            rotationInput = 1f; // Tourne à droite (ou sens horaire)
        }
        else if (Input.GetKey(KeyCode.K))
        {
            rotationInput = -1f; // Tourne à gauche (ou sens anti-horaire)
        }

        // CORRECTION : Utilisation de transform.Rotate pour appliquer l'angle sur l'axe Y (Vector3.up)
        this.transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);

        // --- TIR ---
        if (shootAction.action.WasPressedThisFrame() && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }
    }
}