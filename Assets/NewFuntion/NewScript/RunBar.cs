using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunBar : MonoBehaviour
{
    public static RunBar ins;

    public Image Bar1;
    public Image Bar2;
    public float PlayerEndurance;//Bar2
    public float CurrentEndurance;//Bar1
    public bool Current;

    private void Awake()
    {
        ins = this;
        CurrentEndurance = PlayerEndurance;
    }

    // Update is called once per frame
    void Update()
    {
        Bar1.GetComponent<Image>().fillAmount = CurrentEndurance / PlayerEndurance;

        if (Current == true)
        {
            if (Bar2.GetComponent<Image>().fillAmount > Bar1.GetComponent<Image>().fillAmount)
            {
                Bar2.fillAmount -= 0.005f;
            }
            else
            {
                Bar2.fillAmount = Bar1.fillAmount;
            }
        }
        if (CurrentEndurance>=1)
        {
            CurrentEndurance = 1;
        }
        if (CurrentEndurance <= 0)
        {
            CurrentEndurance = 0;
            PlayerMovement_JoyStick.ins.speed = 5;
        }
    }
}
