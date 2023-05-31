using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float sensitivity = 5.0f;
    private float currentAngle;
    private Vector3 lastPlayerPosition;

    private void Awake()
    {
        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            currentAngle = Input.GetAxis("Mouse X") * sensitivity;
            transform.RotateAround(player.position, Vector3.up, currentAngle);
        }

        transform.Translate(player.position - lastPlayerPosition, Space.World);
        lastPlayerPosition = player.position;
    }
}