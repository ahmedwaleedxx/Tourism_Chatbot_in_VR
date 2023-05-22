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

public class MESSAGE : MonoBehaviour
{
    public TMP_Text Input_Field;
    public TMP_Text Output_Text;
    public TMP_Text Hidden_Output_Text;
    public TMP_InputField ipaddress;
    public GameObject KeyboardCanvas;
    public Button submitButton;
    public Button SubmitIPButton;
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
    private string outputSentence;
    private float typingSpeed = (float)0.0001;
    private string previousText;
    private float lastUpdateTime;
    //string ipAddress;//a variable to control the speed of the typewriter effect
    private bool isConnected = false; // Flag to track connection status
    public TMP_Text ipError;
    void Start()
    {
        //ipAddress = ipaddress.text;
        HideAllPics();
        // image.gameObject.SetActive(false);
        //if (IsIPAddressValid(ipAddress) == true)
        //{
        //    connect(ipAddress);
        //    ipError.text = "";
        //}
        //else
        //{
        //    KeyboardCanvas.gameObject.SetActive(true);
        //    ipaddress.gameObject.SetActive(true);
        //}

        //previousText = Input_Field.text;
        //lastUpdateTime = Time.time;
        //StartCoroutine(CheckTextChange());

        //try
        //{
        //    // Send and receive messages
        //    string input = Input_Field.text;
        //    CheckOption(input);
        //    SendMessageToPython(input, ipAddress);

        //}
        //catch (IOException e)
        //{
        //    Debug.LogError("Unable to read data from the transport connection: " + e.Message);
        //    //connect();
        //    //SendMessageToPython(Input_Field.text);
        //}

        // Subscribe to the onClick event of the submitButton
        //SubmitIPButton.onClick.AddListener(() =>
        //{
        //    if (ipaddress.text != null)
        //    {
        //        ipError.text = ipaddress.text;
        //        ipaddress.gameObject.SetActive(false);
        //        KeyboardCanvas.gameObject.SetActive(false);
        //        SubmitIPButton.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        ipaddress.gameObject.SetActive(true);
        //        KeyboardCanvas.gameObject.SetActive(true);
        //        SubmitIPButton.gameObject.SetActive(true);
        //    }
        //}
        //);
        submitButton.onClick.AddListener(() =>
        {

            try
            {
                // Send and receive messages
                //string input = Input_Field.text;
                CheckOption(Input_Field.text);
                SendMessageToPython(Input_Field.text, ipaddress.text);

            }
            catch (IOException e)
            {
                Debug.LogError("Unable to read data from the transport connection: " + e.Message);
                //connect();
                //SendMessageToPython(Input_Field.text);
            }

        });
    }
    //public static bool IsIPAddressValid(string ip)
    //{
    //    // Regular expression pattern to validate IP address format
    //    string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

    //    // Check if the IP address matches the pattern
    //    Match match = Regex.Match(ip, pattern);

    //    return match.Success;
    //}
    //private IEnumerator CheckTextChange()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(6f); // Wait for 2 seconds

    //        // Check if the text has not changed
    //        if (Input_Field.text == previousText && Time.time - lastUpdateTime >= 6f)
    //        {
    //            try
    //            {
    //                // Send and receive messages
    //                // Check if the text is not empty and has not changed
    //                if (Input_Field.text != "Press on A or X and Speak" && Input_Field.text == previousText && Time.time - lastUpdateTime >= 6f)
    //                {
    //                    string input = Input_Field.text;
    //                    CheckOption(input);
    //                    SendMessageToPython(input, ipAddress);
    //                }


    //            }
    //            catch (IOException e)
    //            {
    //                Debug.LogError("Unable to read data from the transport connection: " + e.Message);

    //            }
    //        }
    //        else
    //        {
    //            Input_Field.text = previousText;
    //        }

    //        previousText = Input_Field.text;
    //        lastUpdateTime = Time.time;
    //    }
    //}
    public bool connect(string ip)
    {
        try
        {
            // Set up a TCP client and connect to the Python program

            client = new TcpClient();
            KeyboardCanvas.gameObject.SetActive(false);
            ipaddress.gameObject.SetActive(false);
            client.Connect(ip, 8000);
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
        foreach (char letter in sentence)
        {
            Output_Text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            if (Output_Text.text == sentence)
            {
                yield return new WaitForSeconds(5f);

            }
        }
    }



    private void SendMessageToPython(string message, string ip)
    {
        try
        {
            if (!isConnected)
            {
                connect(ip);
            }

            var data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);

            // Receive a response from the Python program
            data = new byte[1024];
            var responseData = new System.Text.StringBuilder();
            var bytes = stream.Read(data);
            responseData.Append(System.Text.Encoding.ASCII.GetString(data, 0, bytes));
            Debug.Log("Received message from Python: " + responseData.ToString());
            Output_Text.text = "";
            Hidden_Output_Text.text = "";
            Hidden_Output_Text.text = responseData.ToString();
            //outputSentence = responseData.ToString();

            StartCoroutine(TypeSentence(Hidden_Output_Text.text));
            connect(ip);

        }
        catch (Exception e)
        {
            Debug.LogError("Error sending or receiving message from Python program: " + e.Message);
            Output_Text.text = "Error connecting to server" + e.Message;
            isConnected = false;
            //connect(ip); // Reconnect if there's an error
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
    //private void SendMessageToPython(string message, string ip)
    //{
    //    // Send a message to the Python program
    //    try
    //    {
    //        var data = System.Text.Encoding.ASCII.GetBytes(message);
    //        stream.Write(data, 0, data.Length);

    //        // Receive a response from the Python program
    //        data = new byte[1024];
    //        var responseData = new System.Text.StringBuilder();
    //        var bytes = stream.Read(data);
    //        responseData.Append(System.Text.Encoding.ASCII.GetString(data, 0, bytes));
    //        Debug.Log("Received message from Python: " + responseData.ToString());
    //        outputSentence = responseData.ToString();

    //        StartCoroutine(TypeSentence(outputSentence));
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error sending or receiving message from Python program: " + e.Message);
    //        Output_Text.text = "Connecting...";
    //        //isConnected = false;
    //        connect(ip);
    //    }
    //}


    public void OnDestroy()
    {
        stream.Close();
        client.Close();
    }
}
