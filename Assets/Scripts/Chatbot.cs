using System.Collections.Generic;
using UnityEngine;
//using TensorFlowLite;

public class Chatbot : MonoBehaviour
{
    public TextAsset modelFile;
    public string[] inputNames;
    public string[] outputNames;
    public int maxInputLength = 128;
    public int maxOutputLength = 128;

    //private Interpreter interpreter;
    private List<int[]> inputIds = new List<int[]>();
    private List<int[]> outputIds = new List<int[]>();

    void Start()
    {
      //  interpreter = new Interpreter(modelFile.bytes);

        // Resize input and output arrays
      //  interpreter.ResizeInputTensor(0, new int[] { 1, maxInputLength });
      //  interpreter.ResizeOutputTensor(0, new int[] { 1, maxOutputLength });

        // Allocate input and output arrays
        inputIds.Add(new int[maxInputLength]);
        outputIds.Add(new int[maxOutputLength]);
    }

    public string GetResponse(string input)
    {
        // Convert input string to input tensor
        int[] inputIdArray = inputIds[0];
        for (int i = 0; i < input.Length && i < maxInputLength; i++)
        {
            inputIdArray[i] = input[i];
        }
        //Tensor inputTensor = new Tensor(inputIdArray);

        //// Run inference on input tensor
        //interpreter.SetInputTensorData(0, inputTensor);
        //interpreter.Invoke();
        //interpreter.GetOutputTensorData<int>(0, outputIds[0]);

        // Convert output tensor to output string
        int[] outputIdArray = outputIds[0];
        string output = "";
        for (int i = 0; i < outputIdArray.Length && i < maxOutputLength; i++)
        {
            if (outputIdArray[i] == 0)
            {
                break;
            }
            output += (char)outputIdArray[i];
        }

        return output;
    }

    void OnDestroy()
    {
        //interpreter.Dispose();
    }
}
