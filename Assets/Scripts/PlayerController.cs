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

    private int monedas = 0;
    private int vidas = 3;

    public bool isGameOver;
    public bool youWin;

    private AudioSource playerAudioSource;
    [SerializeField] private AudioSource cameraAudioSource;

    [SerializeField] private AudioClip goodClip;
    [SerializeField] private AudioClip badClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip deathClip;

    [SerializeField] private ParticleSystem GoodParticleSystem;
    [SerializeField] private ParticleSystem BadParticleSystem;
    [SerializeField] private ParticleSystem DeadParticleSystem;
    [SerializeField] private ParticleSystem WinParticleSystem;

    private Animator anim;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("Monedas = " + monedas);
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

            playerAudioSource.PlayOneShot(goodClip, 0.4f);
            Destroy(collision.gameObject);
            monedas++;
            Debug.Log("Monedas = " + monedas);
            //GoodParticleSystem.Play();
            Instantiate(GoodParticleSystem, collision.transform.position, transform.rotation);

            if (monedas == 3)
            {
                youWin = true;
                Debug.Log("YOU WIN");
                speed = 0;
                Invoke("WinParticles", 0.75f);
                cameraAudioSource.Stop();
                cameraAudioSource.clip = winClip;
                cameraAudioSource.Play();


            }

        }

        if (collision.gameObject.CompareTag(BAD))
        {

            playerAudioSource.PlayOneShot(badClip, 0.6f);
            Destroy(collision.gameObject);
            vidas = vidas - 1;
            Debug.Log("Vidas = " + vidas);
            Instantiate(BadParticleSystem, collision.transform.position, BadParticleSystem.transform.rotation);
            anim.SetTrigger("BadCoinCollected");
           
            

            if (vidas == 0)
            {
                isGameOver = true;
                Debug.Log("GAME OVER");
                speed = 0;
                cameraAudioSource.Stop();
                cameraAudioSource.clip = deathClip;
                cameraAudioSource.Play();
                Invoke("DeadParticles", 0.75f);

            }

        }

    }

    private void DeadParticles()
    {
        Instantiate(DeadParticleSystem, transform.position, DeadParticleSystem.transform.rotation);
    }

    private void WinParticles()
    {
        Instantiate(WinParticleSystem, transform.position, WinParticleSystem.transform.rotation);
    }

}
