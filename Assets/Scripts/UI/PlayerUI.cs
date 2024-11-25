using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timeText;
    public GameObject backgroundImage;
    public GameObject gameOverText;
    public Button restartButton;
    public Button exitButton;

    private void Awake()
    {
        instance = this;
    }  
}
