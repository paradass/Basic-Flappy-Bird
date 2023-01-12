using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance => _instance;

    [SerializeField] GameObject menu, market;
    [SerializeField] Text coin;
    [SerializeField] AudioSource errorSound;

    Color oldColor;

    private void Awake()
    {
        _instance = this;
    }
    public void UpgradeCoin()
    {
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Market()
    {
        PlayerPrefs.SetString("bird0", "open");
        menu.SetActive(false);
        market.SetActive(true);
        UpgradeCoin();
    }
    public void Menu()
    {
        menu.SetActive(true);
        market.SetActive(false);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("coin", 0);
        PlayerPrefs.SetInt("selectedBird", 0);
        PlayerPrefs.SetString("bird1", "close");
        PlayerPrefs.SetString("bird2", "close");
        PlayerPrefs.SetString("x2Coin", "close");
        PlayerPrefs.SetString("trail", "close");

    }

    public void NoGold()
    {
        errorSound.Play();
        oldColor = coin.color;
        coin.color = Color.red;
        Invoke("FinishNoGold", 1);
    }

    void FinishNoGold()
    {
        coin.color = oldColor;
    }
}
