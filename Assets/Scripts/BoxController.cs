using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour
{
    [SerializeField] float speed;
    float waveSpeed = 2f; // Velocidad de la onda
    float waveHeight = 0.5f; // Altura de la onda
    float yOffset = -3.5f; // Posición en Y

    float circleSpeed = 6f; // Velocidad del círculo
    float circleRadius = 0.5f; // Radio del círculo
    
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }
    
    void Move(){
        int i = SceneManager.GetActiveScene().buildIndex;
        switch(i){
            case 1:
                transform.position += Vector3.right * speed * Time.deltaTime;
                if(transform.position.x > 6){
                    transform.position = new Vector3(-6, transform.position.y, transform.position.z);
                }
                break;
            case 2:
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * waveSpeed) * waveHeight + yOffset, transform.position.z);
                if(transform.position.x > 6){
                    transform.position = new Vector3(-6, transform.position.y, transform.position.z);
                }
                break;
            case 3:
                float x = transform.position.x + Time.deltaTime * speed;
                float y = Mathf.Sin(Time.time * circleSpeed) * circleRadius + yOffset;
                float z = Mathf.Cos(Time.time * circleSpeed) * circleRadius;
                transform.position = new Vector3(x, y, z);
                if(transform.position.x > 6){
                    transform.position = new Vector3(-6, transform.position.y, transform.position.z);
                }
                break;
            default:
                transform.position += Vector3.right * speed * Time.deltaTime;
                if(transform.position.x > 6){
                    transform.position = new Vector3(-6, transform.position.y, transform.position.z);
                }
                break;
        }
    }
}
