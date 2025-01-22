using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMainMenu : MonoBehaviour
{

    public void GoBack()
    {
        Debug.Log("Going back to main menu");
        SceneManager.LoadScene("MainMenu");
    }
}
