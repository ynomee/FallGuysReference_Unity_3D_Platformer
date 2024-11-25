using UnityEngine;

public class PlayerHealth : ApplicationInteractions
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private AudioSource _audioSource;

    private bool isDead = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();

        PlayerUI.instance.gameOverText.SetActive(false);
        PlayerUI.instance.backgroundImage.SetActive(false);
        PlayerUI.instance.restartButton.gameObject.SetActive(false);
        PlayerUI.instance.exitButton.gameObject.SetActive(false);
        _audioSource.GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                IsDead();
            }
            Debug.Log(_currentHealth + " Здоровья осталось");
            UpdateHealthBar();
        }

    }

    private void UpdateHealthBar()
    {
        PlayerUI.instance.healthBar.value = _currentHealth / _maxHealth;
        PlayerUI.instance.healthText.text = _currentHealth.ToString();
    }

    private void IsDead()
    {
        isDead = true;
        Debug.Log("Убит");
        PlayerUI.instance.gameOverText.SetActive(true);
        PlayerUI.instance.backgroundImage.SetActive(true);
        PlayerUI.instance.restartButton.gameObject.SetActive(true);
        PlayerUI.instance.exitButton.gameObject.SetActive(true);
        PlayerUI.instance.healthBar.gameObject.SetActive(false);
        Time.timeScale = 0; //Stop the time

        PlayerUI.instance.restartButton.onClick.AddListener(RestartGame);
        PlayerUI.instance.exitButton.onClick.AddListener(ExitGame);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    protected override void RestartGame()
    {
        base.RestartGame();
    }

    protected override void ExitGame()
    {
        base.ExitGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            _audioSource.Play();
            IsDead();
        }
    }

}
