using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KEYPAD : MonoBehaviour
{
    public TMP_InputField charHolder;
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;
    public GameObject btn5;
    public GameObject btn6;
    public GameObject btn7;
    public GameObject btn8;
    public GameObject btn9;
    public GameObject btn0;
    public GameObject enterBtn;
    public GameObject clearBtn;
    public GameObject trigger;
    public GameObject door;

    public void b1()
    {
        charHolder.text = charHolder.text + "1";
    }

    public void b2()
    {
        charHolder.text = charHolder.text + "2";
    }
    public void b3()
    {
        charHolder.text = charHolder.text + "3";
    }
    public void b4()
    {
        charHolder.text = charHolder.text + "4";
    }
    public void b5()
    {
        charHolder.text = charHolder.text + "5";
    }
    public void b6()
    {
        charHolder.text = charHolder.text + "6";
    }
    public void b7()
    {
        charHolder.text = charHolder.text + "7";
    }
    public void b8()
    {
        charHolder.text = charHolder.text + "8";
    }
    public void b9()
    {
        charHolder.text = charHolder.text + "9";
    }
    public void b0()
    {
        charHolder.text = charHolder.text + "0";
    }

    public void clear()
    {
        charHolder.text = null;
    }

    public void enter()
    {
        if (charHolder.text == "16062010")
        {
            charHolder.text = "CORRECT";
            door.SetActive(false);
            trigger.SetActive(false);
        }
        else
        {
            charHolder.text = "INCORRECT";
        }
    }

}
