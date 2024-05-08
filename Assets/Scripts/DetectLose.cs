using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectLose : MonoBehaviour
{
    public float loseHeight = -10f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= loseHeight)
        {
            SceneManager.LoadScene("LevelHub");
        }
    }
}
