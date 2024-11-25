using UnityEngine;

public class PropellerTrap : AudioSounds
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _pushForce;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0.05f;
                PlaySound(0, volume: 0.5f);
                playerRb.AddForce(pushDirection.normalized * _pushForce, ForceMode.Impulse);
            }
        } 
    }
}
