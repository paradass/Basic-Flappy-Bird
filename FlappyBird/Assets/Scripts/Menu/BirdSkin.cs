using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BirdSkin : MonoBehaviour, IPointerClickHandler
{
    public string lockName;
    public int birdCount,cost;
    
    void Update()
    {
        Control();
    }

    void Control()
    {
        if (PlayerPrefs.GetString(lockName, "close") == "open")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("selectedBird", 0) == birdCount)
            {
                transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(2).gameObject.SetActive(false);
            }
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
        if (PlayerPrefs.GetString(lockName, "close") == "open")
        {
            PlayerPrefs.SetInt("selectedBird", birdCount);
        }
        else
        {
            if(PlayerPrefs.GetInt("coin",0) >= cost)
            {
                PlayerPrefs.SetString(lockName, "open");
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) - cost);
                PlayerPrefs.SetInt("selectedBird", birdCount);
                MenuManager.Instance.UpgradeCoin();
            }
            else
            {
                MenuManager.Instance.NoGold();
            }
        }
    }
}
