using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform targetPosition;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float zoomRate;
    [SerializeField] public Vector3 maxOffset;
    [SerializeField] public Vector3 offset;
    [SerializeField] private Color backgroundColor;
    private float targetCameraSize;

    private Camera myCamera;

    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
        myCamera = GetComponent<Camera>();

        myCamera.backgroundColor = backgroundColor;

        ZoomOut();
    }

    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition.position + offset, cameraSpeed * Time.deltaTime);
        transform.position = newPosition;

        float newSize = Mathf.Lerp(myCamera.orthographicSize, targetCameraSize, zoomRate * Time.deltaTime);
        myCamera.orthographicSize = newSize;
    }

    public void ZoomIn()
    {
        ZoomIn(targetPosition);
    }
    public void ZoomIn(Transform target)
    {
        targetPosition = target;
        targetCameraSize = 3;
    }
    public void ZoomIn(Transform target, bool instantOut)
    {
        if (instantOut)
        {
            transform.position = target.position;
            myCamera.orthographicSize = 3;
        } else
        {
            ZoomIn(target);
        }
    }

    public void ZoomOut()
    {
        ZoomOut(targetPosition);
    }
    public void ZoomOut(Transform target)
    {
        targetPosition = target;
        targetCameraSize = 5;
    }
}
