using System.Collections;
using UnityEngine;

public class WindTrap : Trap
{
    [SerializeField] private float _pushForce;
    private Vector3 _windDirection;
    private Coroutine _windChangeCoroutine;

    protected override void Start()
    {
        base.Start();
        _windChangeCoroutine = StartCoroutine(ChangeWindDirection());

    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(_windDirection * _pushForce, ForceMode.Force);
            }
        }
    }

    private IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            _windDirection = new Vector3(randomX, 0f, randomZ).normalized;

            yield return new WaitForSeconds(2f);
        }
    }

    protected override void DealDamage()
    {

    }

    protected override void ActivateEffect()
    {

    }

    protected override void ResetTrap()
    {
        base.ResetTrap();
        if (_windChangeCoroutine == null)
        {
            StopCoroutine(_windChangeCoroutine);
        }
    }


}
