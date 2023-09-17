using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 


[RequireComponent(typeof(AudioSource))]
public class Speech : MonoBehaviour
{
    public GameObject sphere;
    public float speed;
    bool isPaused;


    SpriteRenderer rend;

    [Range(0f, 1f)]
    public float Faded = 0.1f;
    [Range(0f, 1f)]
    public float Visible = 1f;

   public void Start()
    {
        rend = sphere.gameObject.GetComponent<SpriteRenderer>();
        Color c = rend.color;
        c.a = rend.color.a;  // 'a' stands for alpha channel in the color component
    }

    IEnumerator currentRunningCoroutine;


    public void StartScaling()
    {
        currentRunningCoroutine = ScaleOverTime(sphere.transform.localScale, speed);
        StartCoroutine(currentRunningCoroutine);
    }


    public void Stop()
    {
        StopAllCoroutines();

        if (sphere.gameObject.GetComponent<SpriteRenderer>().color.a > 0.5f)
        { 
            StartCoroutine(FadeOut());
        }
    }


    IEnumerator ScaleOverTime(Vector3 originalScale, float speed)
    {
        Debug.Log("appearing");

        if (sphere.gameObject.GetComponent<SpriteRenderer>().color.a <= 0.5f)
        {
            Debug.Log("increasing alpha. hopefully just once");
            sphere.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

           Vector3 destinationScale = new Vector3(15f, 15f, 1f);

        while(originalScale != destinationScale)
        {
            if (currentRunningCoroutine != null)
            {
                StopCoroutine(currentRunningCoroutine);
            }

            sphere.transform.localScale = Vector3.Lerp(originalScale, destinationScale, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Debug.Log("fadin ooout!");
        for (float i = sphere.gameObject.GetComponent<SpriteRenderer>().color.a; i >= Faded; i -= 0.05f)    // alpha is always a value between 0 and 1.
        {
            Color c = rend.color;                 // this decreases the alpha with 0.05 per frame. 
            c.a = i;
            rend.color = c;
            yield return null;
        }
        sphere.transform.localScale = new Vector3(0f, 0f, 1f);
    }

}