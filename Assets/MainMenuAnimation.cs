using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour
{
    TMP_Text TitleText,StartButtonText,ModifiersText,HighScoreText;
    GameObject StartButton,ModifiersButton;


    // Start is called before the first frame update
    void Start()
    {
        //Grab then disable buttons
        StartButton = GameObject.Find("StartButton");
        ModifiersButton = GameObject.Find("ModifiersButton");
        StartButton.GetComponent<Button>().interactable = false;
        ModifiersButton.GetComponent<Button>().interactable = false;

        //Get Text Components
        TitleText = GameObject.Find("GameText").GetComponent<TMP_Text>();
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
        StartButtonText = GameObject.Find("StartButton/Text").GetComponent<TMP_Text>();
        ModifiersText = GameObject.Find("ModifiersButton/Text").GetComponent<TMP_Text>();

        //Make text transparent
        StartButtonText.alpha = 0;
        TitleText.alpha = 0;
        ModifiersText.alpha = 0;
        HighScoreText.alpha = 0;

        if (CheckForHistory())
        {
            LoadHistory();
        }
        else
        {
            HighScoreText.text = "";
        }

        Invoke("DisplayMainMenu", 3);
        Invoke("MakeBallDynamic", 0.75f);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CentreBall")
        {
            Debug.Log("CentreBall Collision");
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //SceneManager.LoadScene("MainMenu");
        }
    }

    private void MakeBallDynamic()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void DisplayMainMenu()
    {
        StartCoroutine("AnimateMainMenu");
    }

    IEnumerator AnimateMainMenu()
    {
        for (int i = 0; i < 25; i++)
        {
            TitleText.alpha = Mathf.Log(2,512)*i;
            yield return new WaitForSeconds(0.05f);
        }
        float showValue = 0;
        for (int i = 0; i < 5; i++) {
            showValue = Mathf.Log(2, 512) * i * 5;
            StartButtonText.alpha = showValue;
            HighScoreText.alpha = showValue;
            ModifiersText.alpha = showValue;
            yield return new WaitForSeconds(0.1f);
        }
        StartButton.GetComponent<Button>().interactable = true;
        ModifiersButton.GetComponent<Button>().interactable = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenModifiers()
    {
        SceneManager.LoadScene("ModifiersScreen");
    }

    private bool CheckForHistory()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            return true;
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
            return false;
        }
    }

    private void LoadHistory()
    {
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }
}
