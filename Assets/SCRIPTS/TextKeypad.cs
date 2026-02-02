using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class TextKeypad : MonoBehaviour
{
    public TMP_InputField charHolder;
    public GameObject btnA;
    public GameObject btnB;
    public GameObject btnC;
    public GameObject btnD;
    public GameObject btnE;
    public GameObject btnF;
    public GameObject btnG;
    public GameObject btnH;
    public GameObject btnI;
    public GameObject btnJ;
    public GameObject btnK;
    public GameObject btnL;
    public GameObject btnM;
    public GameObject btnN;
    public GameObject btnO;
    public GameObject btnP;
    public GameObject btnQ;
    public GameObject btnR;
    public GameObject btnS;
    public GameObject btnT;
    public GameObject btnU;
    public GameObject btnV;
    public GameObject btnW;
    public GameObject btnX;
    public GameObject btnY;
    public GameObject btnZ;
    public GameObject btnEnter;
    public GameObject btnClear;
    public GameObject trigger;
    public GameObject Door;

    public void buttonA()
    {
        charHolder.text = charHolder.text + "A";
    }
    public void buttonB()
    {
        charHolder.text = charHolder.text + "B";
    }
    public void buttonC()
    {
        charHolder.text = charHolder.text + "C";
    }
    public void buttonD()
    {
        charHolder.text = charHolder.text + "D";
    }
    public void buttonE()
    {
        charHolder.text = charHolder.text + "E";
    }

    public void buttonF()
    {
        charHolder.text = charHolder.text + "F";
    }

    public void buttonG()
    {
        charHolder.text = charHolder.text + "G";
    }

    public void buttonH()
    {
        charHolder.text = charHolder.text + "H";
    }

    public void buttonI()
    {
        charHolder.text = charHolder.text + "I";
    }

    public void buttonJ()
    {
        charHolder.text = charHolder.text + "J";
    }

    public void buttonK()
    {
        charHolder.text = charHolder.text + "K";
    }

    public void buttonL()
    {
        charHolder.text = charHolder.text + "L";
    }

    public void buttonM()
    {
        charHolder.text = charHolder.text + "M";
    }

    public void buttonN()
    {
        charHolder.text = charHolder.text + "N";
    }
    public void buttonO()
    {
        charHolder.text = charHolder.text + "O";
    }
    public void buttonP()
    {
        charHolder.text = charHolder.text + "P";
    }
    public void buttonQ()
    {
        charHolder.text = charHolder.text + "Q";
    }
    public void buttonR()
    {
        charHolder.text = charHolder.text + "R";
    }
    public void buttonS()
    {
        charHolder.text = charHolder.text + "S";
    }
    public void buttonT()
    {
        charHolder.text = charHolder.text + "T";
    }
    public void buttonU()
    {
        charHolder.text = charHolder.text + "U";
    }
    public void buttonV()
    {
        charHolder.text = charHolder.text + "V";
    }
    public void buttonW()
    {
        charHolder.text = charHolder.text + "W";
    }
    public void buttonX()
    {
        charHolder.text = charHolder.text + "X";
    }
    public void buttonY()
    {
        charHolder.text = charHolder.text + "Y";
    }
    public void buttonZ()
    {
        charHolder.text = charHolder.text + "Z";
    }

    public void clear()
    {
        charHolder.text = null;
    }

    public void enter()
    {
        if (charHolder.text == "LUMA")
        {
            charHolder.text = "CORRECT";
            Door.SetActive(false);
            trigger.SetActive(false);
        }
        else
        {
            charHolder.text = "INCORRECT";
        }
    }
}
