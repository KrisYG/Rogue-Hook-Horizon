using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelHub");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
