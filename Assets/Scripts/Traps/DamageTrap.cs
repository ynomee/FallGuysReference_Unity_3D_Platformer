using System.Collections;
using UnityEngine;

public class DamageTrap : Trap
{
    [SerializeField] private BoxCollider _physicalCollider;
    [SerializeField] private float _blinkDuration;
    [SerializeField] private int _blinkCount;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ActivateEffect()
    {
        blockRenderer.material.color = new Color(1.0f, 0.64f, 0.0f);
    }

    protected override void DealDamage()
    {
        StartCoroutine(BlinkRed());

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null) 
                {
                    playerHealth.TakeDamage(damage);

                    PlaySound(0, volume: 0.8f);
                }
            }
        }

    }

    private IEnumerator BlinkRed()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            blockRenderer.material.color = Color.red;
            yield return new WaitForSeconds(_blinkDuration / (_blinkCount * 2));

            blockRenderer.material.color = defautlColor;
            yield return new WaitForSeconds(_blinkDuration / (_blinkCount * 2));
        }
    }

    protected override void ResetTrap()
    {
        base.ResetTrap();
    }

}
