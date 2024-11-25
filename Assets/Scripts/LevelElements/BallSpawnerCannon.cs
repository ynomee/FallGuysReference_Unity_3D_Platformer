using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerCannon : AudioSounds
{
    [Header("Cannon Settings")]

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _shootForce;
    [SerializeField] private Vector3 _baseShootDir;
    [SerializeField] private float _destroyAfterSecond;

    [SerializeField] private float _angleVarianceX = 10f;     // ������� ���� �� ��� X (� ��������)
    [SerializeField] private float _angleVarianceY = 10f;     // ������� ���� �� ��� Y (� ��������)

    [Header("Ball Physics")]

    [Tooltip("����� ����")]
    [SerializeField] private float _ballMass = 1f;
    [Tooltip("������������� �������")]
    [SerializeField] private float _ballDrag = 0.5f;
    [Tooltip("������������� ��������")]
    [SerializeField] private float _ballAngularDrag = 0.05f;

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    private IEnumerator SpawnBalls()
    {
        while (true) 
        {
            yield return new WaitForSeconds(_spawnInterval);

            GameObject newBall = Instantiate(_ballPrefab, _spawnPoint.position, _spawnPoint.rotation);

            PlaySound(0, random: true, isDestroyed: true, volume: 1f);

            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = _ballMass;
                rb.drag = _ballDrag;
                rb.angularDrag = _ballAngularDrag;

                Vector3 randomizedDirection = GetRandomizedDirection();
                rb.AddForce(randomizedDirection * _shootForce, ForceMode.Impulse);
            }    
            
            Destroy(newBall, _destroyAfterSecond);
        }
    }

    private Vector3 GetRandomizedDirection()
    {
        // ������� ��������� ���� ��� ���� X � Y
        float randomAngleX = Random.Range(-_angleVarianceX, _angleVarianceX);
        float randomAngleY = Random.Range(-_angleVarianceY, _angleVarianceY);

        Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);
        return randomRotation * _baseShootDir;
    }
}
