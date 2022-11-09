using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    static Bird _instance;
    public static Bird Instance => _instance;

    [System.NonSerialized] public bool isDead;
    [SerializeField] private Text scoreText;
    [SerializeField] private float jumpForce;
    [SerializeField] AudioSource[] sounds;
    int score;
    Rigidbody2D rb;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Fly();
        DeathControl();
    }

    void Fly()
    {
        if (rb.velocity.y > 0) transform.rotation = Quaternion.Euler(0, 0, 15);
        else transform.rotation = Quaternion.Euler(0, 0, -30);

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            rb.velocity = Vector2.up * jumpForce;
            sounds[0].Play();
        }
    }

    void DeathControl()
    {
        if(transform.position.y > 5 && !isDead)
        {
            isDead = true;
            sounds[3].Play();
            Invoke("Restart", 1);
        }
        else if(transform.position.y < -5 && !isDead)
        {
            isDead = true;
            sounds[3].Play();
            Invoke("Restart", 1);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            if (isDead) return;
            rb.velocity = new Vector2(0, -10);
            isDead = true;
            sounds[2].Play();
            Invoke("Restart", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            sounds[1].Play();
            score += 1;
            scoreText.text = score.ToString();
        }
    }
}
