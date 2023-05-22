using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Keyboard : MonoBehaviour
{

    public TMP_InputField inputField;
    //public GameObject normalButtons;
    //public GameObject capsButtons;
    //private bool caps;

    //public void insertChar(string c)
    //{
    //    inputField.text += c;
    //}
    public void removeChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
    public void insertdot()
    {
        inputField.text += ".";
    }

    public void insertOne()
    {
        inputField.text += "1";
    }
    public void insertTwo()
    {
        inputField.text += "2";
    }
    public void insertThree()
    {
        inputField.text += "3";
    }
    public void insertFour()
    {
        inputField.text += "4";
    }
    public void insertFive()
    {
        inputField.text += "5";
    }
    public void insertSix()
    {
        inputField.text += "6";
    }
    public void insertSeven()
    {
        inputField.text += "7";
    }
    public void insertEight()
    {
        inputField.text += "8";
    }
    public void insertNine()
    {
        inputField.text += "9";
    }
    public void insertZero()
    {
        inputField.text += "0";
    }

}
