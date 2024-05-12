using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] Text txtScore;
    [SerializeField] Text txtMessage;
    [SerializeField] Image[] imgLives;

    [Header("Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenuPause;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject optionsMenuGameOver;
    [SerializeField] GameObject nextLevelMenu;
    [SerializeField] GameObject optionsMenuNextLevel;

    [Header("Options Menu")]
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider fxVolume;
    [SerializeField] Toggle mute;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource fxSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip winLevelSound;

    private float lastMasterVolume;
    private float lastFxVolume;
    const int LIVES = 5;
    int score;
    public int  lives = LIVES;

    bool gameOver;
    bool paused;

    public static GameManager GetInstance(){
        return instance;
    }

    void Awake() {
        if (instance == null){
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }

        if (masterVolume != null) {
            masterVolume.onValueChanged.AddListener(ChangeVolumeMaster);
        } else {
            Debug.LogError("masterVolume is null");
        }

        if (fxVolume != null) {
            fxVolume.onValueChanged.AddListener(ChangeVolumeFX);
        } else {
            Debug.LogError("fxVolume is null");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver){
            if(paused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }

    }

    public void RestLifes(bool gameOver){
        lives--;
        this.gameOver = gameOver;

        if(gameOver){
            GameOver();
        }
    }
    

    private void OnGUI() {
        for (int i=0; i<imgLives.Length;i++){
            imgLives[i].enabled = i < lives;
        }

        txtScore.text = string.Format("{0,3:D3}", score);
    }

    public void AddScore(){
        score ++;
    }
    public int GetScore(){
        return score;
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }

    public void OpenMenu(GameObject menu)
    {
        pauseMenu.SetActive(false);
        optionsMenuPause.SetActive(false);
        gameOverMenu.SetActive(false);
        optionsMenuGameOver.SetActive(false);
        nextLevelMenu.SetActive(false);
        optionsMenuNextLevel.SetActive(false);
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        OpenMenu(pauseMenu);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        PlaySoundButton();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

public void WinLevel(){
    nextLevelMenu.SetActive(true);
    Time.timeScale = 0;
    fxSource.PlayOneShot(winLevelSound);
}
    public void NextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        if(level <3){
            SceneManager.LoadScene(level + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
        //Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
