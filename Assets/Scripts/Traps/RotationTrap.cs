using UnityEngine;

public class RotationTrap : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _rotationCenter;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                Vector3 directionToCenter = other.transform.position - _rotationCenter.position;
                directionToCenter.y = 0;

                Vector3 force = Vector3.Cross(Vector3.up, directionToCenter).normalized * _rotationSpeed;

                playerRigidbody.AddForce(force, ForceMode.Acceleration);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
