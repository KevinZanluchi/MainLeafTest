using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject menuPauseObj;
    public int boolInt;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume(!menuPauseObj.active);
        }
    }

    public void Resume(bool active)
    {
        menuPauseObj.SetActive(active);
        boolInt = active ? 1:0;
        Time.timeScale = boolInt;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
