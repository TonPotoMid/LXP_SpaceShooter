using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotController : MonoBehaviour
{
    public GameObject projectilePreFab;
    public float throwForce;
    public float minThrowForce, maxThrowForce;
    public float maxThrowDistance;

    public InputActionReference mousePositionAction;
    public InputActionReference mouseClickAction;

    private Vector2 initialMousePosition;

    public LineRenderer previewLine;

    // Update is called once per frame
    void Update()
    {

        if (mouseClickAction.action.WasPressedThisFrame())
        {
            initialMousePosition = mousePositionAction.action.ReadValue<Vector2>();
            previewLine.enabled = true;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePositionAction.action.ReadValue<Vector2>());
            worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);
            previewLine.SetPosition(0, worldPosition);
        }

        if (mouseClickAction.action.IsPressed())
        {
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePositionAction.action.ReadValue<Vector2>());
            worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);
            previewLine.SetPosition(1, worldPosition);

        }


        if (mouseClickAction.action.WasReleasedThisFrame())
        {
            previewLine.enabled = false;
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePositionAction.action.ReadValue<Vector2>());
            worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);

            GameObject newProjectile = Instantiate(projectilePreFab, worldPosition, Quaternion.identity);

            Vector3 throwDirection = initialMousePosition - mousePositionAction.action.ReadValue<Vector2>();
            float throwForce = Mathf.Clamp(throwDirection.magnitude, 1f, maxThrowDistance) * maxThrowForce;
            throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);

            newProjectile.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);
            
        }
    }
}
