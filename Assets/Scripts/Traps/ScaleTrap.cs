using UnityEngine;

public class ScaleTrap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private void Start()
    {
        _animator.SetBool("ScaleTrapActive", true);
    }
}
