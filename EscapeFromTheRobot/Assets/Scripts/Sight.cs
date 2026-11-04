using UnityEngine;

public class Sight : MonoBehaviour
{
    public float detectionInterval = 1.0f;
    private float elapsedTime = 0.0f;
    public float detectionAngle = 45f;
    public float detectionDistance = 100f;
    [SerializeField]
    private Transform playerTransform;
    private Vector3 rayDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= detectionInterval)
        {
            CheckSight();
            elapsedTime = 0.0f;
        }
    }

    void CheckSight()
    {
        if (playerTransform == null) return;
        rayDirection = playerTransform.position - transform.position;
        if (Vector3.Angle(transform.forward, rayDirection) < detectionAngle)
        {
            Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, detectionDistance);
            if (hit.collider == null)
            {
                return;
            }
            if (hit.collider.name == "FirstPersonPlayer")
            {
                Debug.Log("Player detected");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rayDirection);
        Vector3 frontRay = transform.forward * detectionDistance;
        Vector3 leftRay = Quaternion.AngleAxis(detectionAngle, transform.up) * frontRay;
        Vector3 rightRay = Quaternion.AngleAxis(-detectionAngle, transform.up) * frontRay;
        Debug.DrawRay(transform.position, frontRay, Color.green);
        Debug.DrawRay(transform.position, leftRay, Color.green);
        Debug.DrawRay(transform.position, rightRay, Color.green);

    }
}
