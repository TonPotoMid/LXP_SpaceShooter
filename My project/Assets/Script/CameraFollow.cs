using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Glissez votre personnage ici dans l'inspecteur
    public float smoothSpeed = 0.125f; // Vitesse de l'amorti
    public Vector3 offset; // Décalage entre la caméra et le personnage (ex: 0, 5, -10)

    void LateUpdate()
    {
        // Position désirée de la caméra
        Vector3 desiredPosition = target.position + offset;

        // Transition fluide entre la position actuelle et la position désirée
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Appliquer la position
        transform.position = smoothedPosition;
    }
}