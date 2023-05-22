using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{


    public void MeetChatbotBtn()
    {
        SceneManager.LoadScene("MeetChatbot");
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void goToPyramidsbtn()
    {
        SceneManager.LoadScene("PyramidsOfGiza");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
