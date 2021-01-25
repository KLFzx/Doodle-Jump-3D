using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);


    }



}
