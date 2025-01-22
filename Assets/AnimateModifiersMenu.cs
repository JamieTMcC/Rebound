using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimateModifiersMenu : MonoBehaviour
{

    List<GameObject> ModifierButtons = new List<GameObject>();
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject canvas = this.gameObject;
        for(int i =0; i < canvas.transform.childCount-1; i++)
        {
            ModifierButtons.Add(canvas.transform.GetChild(i).gameObject);
            ModifierButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().alpha = 0;
        }
        ModifierButtons.Add(canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject);
        StartCoroutine(AnimateButtons());

    }

    IEnumerator AnimateButtons()
    {
        audioSource.Play();
        float MoveDownAmount = 0.0f;
        float MoveDownAmountModifier = 2f;
        RectTransform rectTransform;
        foreach(GameObject button in ModifierButtons)
        {
            rectTransform = button.GetComponent<RectTransform>();
            for (int i = 0; i < 10; i++)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - (MoveDownAmount/10));
                if (button.transform.childCount > 0)
                    button.transform.GetChild(0).GetComponent<TMP_Text>().alpha = Mathf.Log10(i-1) * 225;
                Debug.Log(Mathf.Log10(i) * 128);
                yield return new WaitForSeconds(0.02f);
            }
            MoveDownAmount += MoveDownAmountModifier;
        }
        LoadModifiers();

    }

    private void LoadModifiers()
    {
        for (int i = 0; i < ModifierButtons.Count -1; i++)
        {
            if (PlayerPrefs.HasKey(ModifierButtons[i].name))
            {
                if (PlayerPrefs.GetInt(ModifierButtons[i].name) == 1)
                {
                    ModifierButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
                }
            }
            else
            {
                PlayerPrefs.SetInt(ModifierButtons[i].name, 0);
            }
            if (PlayerPrefs.GetInt("HighScore") < Int32.Parse(ModifierButtons[i].transform.Find("ScoreReq").gameObject.GetComponent<TMP_Text>().text))
            {
                ModifierButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
                ModifierButtons[i].gameObject.GetComponent<Button>().interactable = false;

            }
        }
    }

    public void ToggleModifier(GameObject button)
    {
        if (PlayerPrefs.GetInt(button.name) == 0)
        {
            PlayerPrefs.SetInt(button.name, 1);
            button.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            PlayerPrefs.SetInt(button.name, 0);
            button.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
