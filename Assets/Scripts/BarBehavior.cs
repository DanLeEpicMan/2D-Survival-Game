using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehavior : MonoBehaviour
{
    public float hunger;
    public float health;
    public float healthRegenPerSecond;
    public float hungerDegenPerSecondStill;
    public float hungerDegenPerSecondWalking;
    public float hungerDegenPerSecondRunning;
    [SerializeField]
    private Image healthSprite, hungerSprite;

    // Start is called before the first frame update
    void Start()
    {
        updateHealthBar();
        updateHungerBar();
    }

    // Update is called once per frame
    void Update()
    {
        health += healthRegenPerSecond * Time.deltaTime * hunger/100;
        updateHealthBar();
        
        switch (GetComponent<PlayerMovement>().moveState)
        {
            case "Still":
                hunger -= hungerDegenPerSecondStill * Time.deltaTime;
                break;
            case "Walking":
                hunger -= hungerDegenPerSecondWalking * Time.deltaTime;
                break;
            case "Running":
                hunger -= hungerDegenPerSecondRunning * Time.deltaTime;
                break;
        }
        if (hunger < 0) hunger = 0;
        updateHungerBar();
    }

    public void updateHealthBar()
    {
        Debug.Log("Entered updateHealthBar");
        healthSprite.fillAmount = health/100f;
    }

    public void updateHungerBar()
    {
        Debug.Log("Entered updateHungerBar");
        hungerSprite.fillAmount = hunger/100f;
    }
}
