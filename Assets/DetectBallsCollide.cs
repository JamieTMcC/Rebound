using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
public class DetectBallsCollide : MonoBehaviour
{
    bool started = false;
    bool canJump = true;
    TMP_Text GameText;
    int score = 0;
    Vector2 startPos;
    Volume volume;
    Bloom bloom;

    EventHandler OnMove;


    void Start()
    {
        startPos = transform.position;
        GameText = GameObject.Find("GameText").GetComponent<TMP_Text>();
        GameText.text = "Move the ball from the centre to start the game!";
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
    }

    public void Jump()
    {
        if(started && canJump)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            canJump = false;
            StartCoroutine("JumpCooldown");
            GameObject.Find("JumpTimer").transform.localScale = new Vector3(0.3f, 2f, 1f);
        }
    }

    IEnumerator JumpCooldown()
    {
        GameObject.Find("JumpIndicator").GetComponent<SpriteRenderer>().color = Color.red;
        for(int i = 0; i < 50; i++) {
            yield return new WaitForSeconds(0.01f);
            GameObject.Find("JumpTimer").transform.localScale -= new Vector3(0, 0.04f, 0);
        }
        GameObject.Find("JumpIndicator").GetComponent<SpriteRenderer>().color = Color.white;
        canJump = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "CentreBall")
            {
   
            if (started)
            {
                
                score++;
                GameText.text = "" + score;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            started = false;
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
                GameText.text = "New High Score! Your score is: " + score;
            }
            else
            {
                GameText.text = "Game Over! Your score is: " + score;
            }
            score = 0;
            StartCoroutine("AnimateRestart");

            Invoke("RestartGame", 3);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MoveFromCentreCollider")
        {
            if (!started)
            {
                started = true;
                GameText.text = "0";
            }
        }
    }

    IEnumerator AnimateRestart()
    {
        yield return new WaitForSeconds(1.0f);
        for(int i = 0; i<25; i++)
        {
            bloom.intensity.value += 4f;
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 0; i < 25; i++)
        {
            bloom.intensity.value -= 4f;
            yield return new WaitForSeconds(0.04f);
        }
    }


    void RestartGame()
    {
        transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = startPos;
        started = false;
        GameText.text = "Move the ball from the centre to start the game!";
    }


}
