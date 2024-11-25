using System.Collections;
using UnityEngine;

public class FallTrap : Trap
{
    [SerializeField] private BoxCollider _physicalCollider;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _trapAnimator;
    [SerializeField] private float _blinkDuration;
    [SerializeField] private int _blinkCount;
    [SerializeField] private float _fallDelay;

    private bool _isTriggered = false;

    protected override void Start()
    {
        base.Start();
        _rb.isKinematic = true;
        _trapAnimator.SetBool("FallTrapActive", false);
    }

    protected override void ActivateEffect()
    {
        blockRenderer.material.color = Color.yellow;
    }

    protected override void DealDamage()
    {

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered && other.CompareTag("Player"))
        {
            _isTriggered = true;
            StartCoroutine(ActivateFallTrap());
        }
    }

    private IEnumerator ActivateFallTrap()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < _blinkCount; i++)
        {
            blockRenderer.material.color = Color.red;

            yield return new WaitForSeconds(_blinkDuration / (2 * _blinkCount));

            blockRenderer.material.color = defautlColor;

            yield return new WaitForSeconds(_blinkDuration / (2 * _blinkCount));

            blockRenderer.material.color = Color.red;

        }

        yield return new WaitForSeconds(_fallDelay);

        _rb.isKinematic = false;
        _trapAnimator.SetBool("FallTrapActive", true);

    }

    protected override void ResetTrap()
    {
        base.ResetTrap();
    }
}
