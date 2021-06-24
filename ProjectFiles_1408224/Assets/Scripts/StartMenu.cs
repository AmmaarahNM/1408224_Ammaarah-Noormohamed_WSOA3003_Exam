using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator squirrel;
    // Start is called before the first frame update
    void Start()
    {
        squirrel.SetInteger("AnimState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChanger(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
