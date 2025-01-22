using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    AudioSource audioSource;
    AudioClip ReboundBall, BounceWall, Fail;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ReboundBall = Resources.Load<AudioClip>("BounceSound");
        BounceWall = Resources.Load<AudioClip>("WallBounceSound");
        Fail = Resources.Load<AudioClip>("FailSound");
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CentreBall")
        {
            audioSource.clip = ReboundBall;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
        if((collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Brush") && audioSource.clip != Fail)
        {
            audioSource.clip = BounceWall;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish" && audioSource.clip != Fail)
        {
            audioSource.clip = Fail;
            audioSource.Play();
        }

    }

}
