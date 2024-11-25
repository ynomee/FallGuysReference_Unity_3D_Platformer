using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : AudioSounds
{
    public float damage;
    [SerializeField] private float _rechargeTime;
    [SerializeField] private float _activationDelay;

    protected bool isActive = false;
    protected Renderer blockRenderer;
    protected Color defautlColor;

    protected virtual void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        defautlColor = blockRenderer.material.color;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrapRoutine());
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (!isActive && other.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrapRoutine());
        }
    }

    private IEnumerator ActivateTrapRoutine()
    {
        isActive = true;

        ActivateEffect();

        yield return new WaitForSeconds(_activationDelay);

        DealDamage();

        yield return new WaitForSeconds(_rechargeTime);

        ResetTrap();
        isActive = false;

    }

    protected abstract void ActivateEffect();
    protected abstract void DealDamage();
    protected virtual void ResetTrap()
    {
        blockRenderer.material.color = defautlColor;
    }
}
