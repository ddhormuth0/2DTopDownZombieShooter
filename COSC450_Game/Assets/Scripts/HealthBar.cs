using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;

    //Initializes max health for bar, must be called elswhere to intialize. Best to initialize in player stat sheet where max health is kept
    public void SetMaxHealth(int health)
    {
        //set slider max to max health
        slider.maxValue = health;
        //set slider to full
        slider.value = health;
    }

    //change slider value, called where player takes damage
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
