using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    static Bird _instance;
    public static Bird Instance => _instance;

    [System.NonSerialized] public bool isDead,isStarted;
    [SerializeField] private GameObject menu,trail;
    [SerializeField] private Text coinText;
    [SerializeField] private float jumpForce;
    [SerializeField] AudioSource[] sounds;

    int coin;
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("State", PlayerPrefs.GetInt("selectedBird", 0));
        rb = GetComponent<Rigidbody2D>();
        coin = PlayerPrefs.GetInt("coin", 0);
        coinText.text = coin.ToString();
        if (PlayerPrefs.GetString("trail", "close") == "open") trail.SetActive(true);
    }

    void Update()
    {
        Fly();
        DeathControl();
    }

    public void StartBird()
    {
        rb.gravityScale = 2;
        isStarted = true;
    }

    void Fly()
    {
        if(!isStarted) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (rb.velocity.y > 0) transform.rotation = Quaternion.Euler(0, 0, 15);
        else transform.rotation = Quaternion.Euler(0, 0, -30);
        if(!isDead) transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
        if (Input.GetMouseButtonDown(0) && !isDead && isStarted)
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
            Invoke("OpenMenu", 0.5f);
        }
        else if(transform.position.y < -5 && !isDead)
        {
            isDead = true;
            sounds[3].Play();
            Invoke("OpenMenu", 0.5f);
        }
    }

    void OpenMenu()
    {
        menu.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe")
        {
            if (isDead) return;
            GetComponent<CircleCollider2D>().isTrigger = true;
            rb.velocity = new Vector2(0, -10);
            isDead = true;
            sounds[2].Play();
            animator.speed = 0;
            Invoke("OpenMenu", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            sounds[1].Play();
            coin = PlayerPrefs.GetInt("coin",0);
            if (PlayerPrefs.GetString("x2Coin", "close") == "open") coin += 2;
            else coin += 1;
            PlayerPrefs.SetInt("coin", coin);
            coinText.text = coin.ToString();
            Destroy(collision.gameObject);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
