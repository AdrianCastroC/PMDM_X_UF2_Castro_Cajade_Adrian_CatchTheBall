using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))  // Asegúrate de que tu bola tenga la etiqueta "Ball"
        {
            Destroy(other.gameObject);  // Destruye la bola
        }
    }
}
