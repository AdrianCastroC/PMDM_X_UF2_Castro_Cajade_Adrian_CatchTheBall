using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject helpMenu;

    [Header("Options Menu")]
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider fxVolume;
    [SerializeField] Toggle mute;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource fxSource;
    [SerializeField] AudioClip clickSound;

    private float lastMasterVolume;
    private float lastFxVolume;

    
    void Awake() 
    {
        masterVolume.onValueChanged.AddListener(ChangeVolumeMaster);
        fxVolume.onValueChanged.AddListener(ChangeVolumeFX);
    }

    void Start()
    {
        OpenMenu(mainMenu);
    }

    void Update()
    {
        
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }

    public void OpenMenu(GameObject menu)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        startMenu.SetActive(false);
        helpMenu.SetActive(false);
        menu.SetActive(true);
        PlaySoundButton();
    }

    public void PlayLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ChangeVolumeMaster(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }

    public void ChangeVolumeFX(float volume)
    {
        mixer.SetFloat("FXVolume", volume);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button clicked!");
    }

    public void SetMute()
    {
        
        if(mute.isOn)
        {
            mixer.GetFloat("MasterVolume", out lastMasterVolume);
            mixer.GetFloat("FXVolume", out lastFxVolume);
            mixer.SetFloat("MasterVolume", -80);
            mixer.SetFloat("FXVolume", -80);
        }
        else
        {
            mixer.SetFloat("MasterVolume", lastMasterVolume);
            mixer.SetFloat("FXVolume", lastFxVolume);
            PlaySoundButton();
        }
    }
}
