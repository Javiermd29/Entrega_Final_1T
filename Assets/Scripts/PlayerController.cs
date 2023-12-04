using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{

    //CONSTANTES
    private const string GOOD = "Good Coin";
    private const string BAD = "Bad Coin";

    private float speed = 10f;

    private float horizontalInput;
    private float verticalInput;

    private float xRange = 9.5f;
    private float zRange = 7f;

    private int puntos = 0;
    private int vidas = 3;

    public bool isGameOver;

    private void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log("Puntos = " + puntos);
        Debug.Log("Vidas = " + vidas);
    }

    void Update()
    {

        

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);

        PlayerInBound();

    }

    private void PlayerInBound()
    {

        Vector3 pos = transform.position;

        if (pos.x <= -xRange)
        {
            pos.x = -xRange;
        }

        if (pos.x > xRange)
        {
            pos.x = xRange;
        }

        if (pos.z <= -zRange)
        {
            pos.z = -zRange;
        }

        if (pos.z > zRange)
        {
            pos.z = zRange;
        }

        transform.position = pos;

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag(GOOD))
        {
            Destroy(collision.gameObject);
            puntos = puntos + 5;
            Debug.Log("Puntos = " + puntos);

        }

        if (collision.gameObject.CompareTag(BAD))
        {

            Destroy(collision.gameObject);
            vidas = vidas - 1;
            Debug.Log("Vidas = " + vidas);

            if (vidas == 0)
            {
                isGameOver = true;
                Debug.Log("GAME OVER");
            }
            

        }

        Destroy(collision.gameObject);

    }

}
