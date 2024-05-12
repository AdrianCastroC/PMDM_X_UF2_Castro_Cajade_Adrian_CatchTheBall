using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    GameObject currentBall;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float force;
    [SerializeField] float gravity;
    [SerializeField] Transform spawnPoint;

    [SerializeField] AudioSource fxSource;
    [SerializeField] AudioClip dropBall;
    void Start()
    {
        SpawnBall();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentBall != null){
            Rigidbody2D rb = currentBall.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = Vector2.down * force;
            rb.gravityScale = gravity;
            PlayDropBall();
            currentBall = null;

            SpawnBall();
        }
        
    }
    void SpawnBall()
    {
        if (currentBall == null)
        {
            currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = currentBall.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void PlayDropBall()
    {
        fxSource.PlayOneShot(dropBall);
    }
}
