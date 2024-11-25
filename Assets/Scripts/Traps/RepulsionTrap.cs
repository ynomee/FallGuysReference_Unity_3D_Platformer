using UnityEngine;

public class RepulsionTrap : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0.05f;
                playerRigidbody.AddForce(pushDirection.normalized * _pushForce, ForceMode.Impulse);
            }
        }
    }
}
