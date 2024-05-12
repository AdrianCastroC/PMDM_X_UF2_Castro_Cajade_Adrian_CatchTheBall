using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLimitController : MonoBehaviour
{

    [SerializeField] AudioSource fxSource;
    [SerializeField] AudioClip failBall;
    [SerializeField] AudioClip gameOver;
    GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))  // Asegúrate de que tu bola tenga la etiqueta "Ball"
        {

            Debug.Log("Ball");
            if(game.lives > 1){
                game.RestLifes(false);
                PlaySound(failBall);
            }else{
                game.RestLifes(true);
                PlaySound(gameOver);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }
}
