using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip MenuMusic;
    public AudioClip clickFx;
    public AudioClip hoverFx;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.PlayOneShot(MenuMusic); 
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void HoverSound()
    {
        audioSrc.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        audioSrc.PlayOneShot(clickFx);
    }
}
