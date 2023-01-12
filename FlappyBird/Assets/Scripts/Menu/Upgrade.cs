using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour, IPointerClickHandler
{
    public string upgradeName;
    public int cost;


    void Update()
    {
        Control();
    }

    void Control()
    {
        if (PlayerPrefs.GetString(upgradeName, "close") == "open")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(PlayerPrefs.GetString(upgradeName,"close") == "close")
        {
            if (PlayerPrefs.GetInt("coin", 0) >= cost)
            {
                PlayerPrefs.SetString(upgradeName, "open");
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) - cost);
                MenuManager.Instance.UpgradeCoin();
            }
            else
            {
                MenuManager.Instance.NoGold();
            }
        }
    }
}
