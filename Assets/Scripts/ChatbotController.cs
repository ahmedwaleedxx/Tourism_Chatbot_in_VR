//using UnityEngine;
//using UnityEngine.UI;
//using NetMQ;
//using NetMQ.Sockets;
//using System;
//using System.Text;
//using System.Diagnostics;
//using Debug = UnityEngine.Debug;
//using TensorFlowLite;

//public class ChatbotController : MonoBehaviour
//{
//    public InputField inputField;
//    public Text outputText;
//    public Button submitButton;

//    private RequestSocket socket;
//    private Process pythonProcess;
//    void Start()
//    {
//        // Start the Python script as a subprocess
//        pythonProcess = new Process();
//        pythonProcess.StartInfo.FileName = "python";
//        pythonProcess.StartInfo.Arguments = "C:/Users/ahmed_2tllwxa/OneDrive/Desktop/Python Web Socket/Last.py";
//        pythonProcess.StartInfo.CreateNoWindow = true;
//        pythonProcess.StartInfo.UseShellExecute = false;
//        pythonProcess.Start();
//        try
//        {
//            // Create a ZMQ request socket and connect to the chatbot server
//            socket = new RequestSocket();
//            socket.Connect("tcp://localhost:5555");
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError($"Failed to connect to chatbot server: {ex.Message}");
//        }

//        // Add listener for the input field's EndEdit event
//        inputField.onEndEdit.AddListener(delegate { OnSubmit(); });

//        // Add listener for the submit button's onClick event
//        submitButton.onClick.AddListener(delegate { OnSubmit(); });
//    }

//    void OnDestroy()
//    {
//        try
//        {
//            // Dispose of the ZMQ socket when the application is shutting down
//            socket?.Dispose();
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError($"Failed to dispose of socket: {ex.Message}");
//        }
//    }

//    void OnSubmit()
//    {
//        // Send the input text to the chatbot server and receive the response
//        string input = inputField.text;
//        byte[] messageBytes = Encoding.UTF8.GetBytes(input);
//        socket.SendFrame(messageBytes);

//        NetMQMessage responseMessage = new NetMQMessage();
//        bool hasResponse = socket.TryReceiveMultipartMessage(TimeSpan.FromSeconds(5), ref responseMessage);

//        if (hasResponse)
//        {
//            string response = Encoding.UTF8.GetString(responseMessage.Last.Buffer);

//            // Update the output text with the response
//            outputText.text = response;
//        }
//        else
//        {
//            Debug.Log("Chatbot server did not respond.");
//        }
//    }
//}
