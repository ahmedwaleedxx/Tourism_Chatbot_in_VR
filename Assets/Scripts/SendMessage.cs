using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Sockets;
using System.IO;
using TMPro;
using System.Net;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SendMessage : MonoBehaviour
{
    public TMP_Text Input_Field;
    public TMP_Text Output_Text;
    public TMP_Text Hidden_Message;
    public TMP_InputField ipaddress;
    public GameObject KeyboardCanvas;
    public Button submitButton;
    public Image AbuSimbelimage;
    public Image AlexImage;
    public Image AswanImage;
    public Image CairoImage;
    public Image CairoTowerImage;
    public Image GizaPyramidsImage;
    public Image HurghadaImage;
    public Image LuxorTempleImage;
    public Image PhialaTempleImage;
    private TcpClient client;
    private NetworkStream stream;
  //  private float typingSpeed = (float)0.0001;
  //  private string previousText;
  //  private float lastUpdateTime;
    //string ipAddress;//a variable to control the speed of the typewriter effect
    private bool isConnected = false; // Flag to track connection status
    public TMP_Text ipError;
    void Start()
    {
        HideAllPics();


        submitButton.onClick.AddListener(() =>
        {

            try
            {
                // Send and receive messages
                string input = Input_Field.text;
                CheckOption(input);
                SendMessageToPython(input);

            }
            catch (IOException e)
            {
                Debug.LogError("Unable to read data from the transport connection: " + e.Message);
                //connect();
                //SendMessageToPython(Input_Field.text);
            }

        });
    }

    public bool connect()
    {
        try
        {
            // Set up a TCP client and connect to the Python program
            client = new TcpClient();
            KeyboardCanvas.gameObject.SetActive(false);
            ipaddress.gameObject.SetActive(false);
            client.Connect(ipaddress.text, 8000);
            stream = client.GetStream();
            isConnected = true;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error while connecting to server " + e.Message);
            KeyboardCanvas.gameObject.SetActive(true);
            ipaddress.gameObject.SetActive(true);
            isConnected = false;
            return false;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        yield return new WaitForSeconds(2f);
        foreach (char letter in sentence)
        {
            Output_Text.text += letter;
            yield return new WaitForSeconds((float)0.0001);
            if (Output_Text.text == sentence)
            {
                yield return new WaitForSeconds(5f);

            }
        }
    }



    private void SendMessageToPython(string message)
    {
        try
        {
            if (!isConnected)
            {
                connect();
            }

            var data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);

            // Receive a response from the Python program
            data = new byte[1024];
            var responseData = new System.Text.StringBuilder();
            var bytes = stream.Read(data);
            responseData.Append(System.Text.Encoding.ASCII.GetString(data, 0, bytes));
            Debug.Log("Received message from Python: " + responseData.ToString());
            //Output_Text.text = "";
            Output_Text.text = "";
            Hidden_Message.text = responseData.ToString();
            //Output_Text.text = responseData.ToString();
            //Hidden_Output_Text.text = "";
            //Hidden_Output_Text.text = responseData.ToString();
            ////outputSentence = responseData.ToString();

            StartCoroutine(TypeSentence(Hidden_Message.text));
            connect();
            
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending or receiving message from Python program: " + e.Message);
            Output_Text.text = "Error connecting to server" + e.Message;
            isConnected = false;
        }
    }

    public void CheckOption(string input)
    {
        if (input.Contains("AbuSimbel"))
        {
            HideAllPics();
            AbuSimbelimage.gameObject.SetActive(true);
        }
        else if (input.Contains("Alexandria") || input.Contains("Alex"))
        {
            HideAllPics();
            AlexImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Aswan"))
        {
            HideAllPics();
            AswanImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Cairo") || input.Contains("capital of egypt"))
        {
            HideAllPics();
            CairoImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Tower"))
        {
            HideAllPics();
            CairoTowerImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Giza") || input.Contains("pyramids"))
        {
            HideAllPics();
            GizaPyramidsImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Hurghada"))
        {
            HideAllPics();
            HurghadaImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Luxor Temple") || input.Contains("Luxor"))
        {
            HideAllPics();
            LuxorTempleImage.gameObject.SetActive(true);
        }
        else if (input.Contains("Phiala"))
        {
            HideAllPics();
            PhialaTempleImage.gameObject.SetActive(true);
        }
        else
        {
            HideAllPics();
        }
    }
    public void HideAllPics()
    {
        AbuSimbelimage.gameObject.SetActive(false);
        AlexImage.gameObject.SetActive(false);
        AswanImage.gameObject.SetActive(false);
        CairoImage.gameObject.SetActive(false);
        CairoTowerImage.gameObject.SetActive(false);
        GizaPyramidsImage.gameObject.SetActive(false);
        HurghadaImage.gameObject.SetActive(false);
        LuxorTempleImage.gameObject.SetActive(false);
        PhialaTempleImage.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        stream.Close();
        client.Close();
    }
}
