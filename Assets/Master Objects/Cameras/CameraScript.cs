using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform targetPosition;
    private Transform playerTransform;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float zoomRate;
    [SerializeField] private float smallFOV;
    [SerializeField] private float largeFOV;
    [SerializeField] private Vector3 defaultMaxOffset;
    [SerializeField] public Vector3 maxOffset;
    [SerializeField] public Vector3 offset;
    private float targetFOV;

    private Camera myCamera;

    private Coroutine playerReturner;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = playerTransform;
        myCamera = GetComponent<Camera>();

        ZoomOut();
    }

    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition.position + offset, cameraSpeed * Time.deltaTime);
        transform.position = newPosition;

        float newFOV = Mathf.Lerp(myCamera.fieldOfView, targetFOV, zoomRate * Time.deltaTime);
        myCamera.fieldOfView = newFOV;
    }

    public void ZoomIn()
    {
        ZoomIn(targetPosition);
    }
    public void ZoomIn(Transform target)
    {
        targetPosition = target;
        targetFOV = smallFOV;

        Invoke("ReturnToPlayer", 10f);
    }
    public void ZoomIn(Transform target, bool instantOut)
    {
        if (instantOut)
        {
            targetPosition = target;
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            myCamera.fieldOfView = smallFOV;

            Invoke("ReturnToPlayer", 10f);
        }
        else
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
        targetFOV = largeFOV;

        Invoke("ReturnToPlayer", 10f);
    }

    public void ReturnToPlayer()
    {
        maxOffset = defaultMaxOffset;
        ZoomOut(playerTransform);

        CancelInvoke("ReturnToPlayer");
    }
}
