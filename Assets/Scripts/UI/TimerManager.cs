using UnityEngine;
using UnityEngine.UI;

public class TimerManager : ApplicationInteractions
{
    private float _startTime;
    private float _currentTime;
    private bool _timerActive = false;

    private void Start()
    {
        PlayerUI.instance.timerText.gameObject.SetActive(false);
        PlayerUI.instance.winText.gameObject.SetActive(false);
        PlayerUI.instance.backgroundImage.gameObject.SetActive(false);
        PlayerUI.instance.timeText.gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        _startTime = Time.time;
        _timerActive = true;
        PlayerUI.instance.timerText.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        _timerActive = false;
        PlayerUI.instance.timerText.gameObject.SetActive(true);

        float finalTime = _currentTime;

        PlayerUI.instance.timerText.text = FormatTime(finalTime);
    }

    private void Finished()
    {
        PlayerUI.instance.winText.gameObject.SetActive(true);
        PlayerUI.instance.winText.gameObject.SetActive(true);
        PlayerUI.instance.restartButton.gameObject.SetActive(true);
        PlayerUI.instance.backgroundImage.gameObject.SetActive(true);
        PlayerUI.instance.timeText.gameObject.SetActive(true);
        PlayerUI.instance.restartButton.onClick.AddListener(RestartGame);
        

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
    }

    private void Update()
    {
        if (_timerActive)
        {
            _currentTime = Time.time - _startTime;
            PlayerUI.instance.timerText.text = FormatTime(_currentTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    protected override void RestartGame()
    {
        base.RestartGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("StartLine"))
            {
                StartTimer();
            }
            else if(gameObject.CompareTag("FinishLine"))
            {
                StopTimer();
                Finished();
            }
        }
    }
}
