using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehavior : MonoBehaviour
{
    public int hunger;
    public int health;
    private float healthTime;
    [SerializeField]
    private Image healthSprite;

    // Start is called before the first frame update
    void Start()
    {
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthTime += Time.deltaTime;
        if (hunger != 0 && healthTime >= 100/hunger)
        {
            health++;
            healthTime = 0;
            updateHealth();
        }
    }

    public void updateHealth()
    {
        healthSprite.fillAmount = health/100;
    }
}
