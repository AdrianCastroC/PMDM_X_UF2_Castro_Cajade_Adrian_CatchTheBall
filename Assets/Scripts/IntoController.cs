using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoController : MonoBehaviour
{
    [SerializeField] AudioSource fxSource;
    [SerializeField] AudioClip intoBall;
    GameManager game;
    void Start()
    {
        game = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ball")){
            Debug.Log("Bola dentro");
            PlaySound();
            game.AddScore();
            if(game.GetScore() == 100){
                game.WinLevel();
            }
        }
    }

    void PlaySound()
    {
        fxSource.PlayOneShot(intoBall);
    }

}
