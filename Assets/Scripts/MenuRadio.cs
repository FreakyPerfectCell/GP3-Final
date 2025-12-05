using UnityEngine;
using System.Collections;

public class MenuRadio : MonoBehaviour
{

    public AudioSource Audio1;
    public AudioSource Audio2;
    public AudioSource Audio3;
    int ranNum;

    void Start()
    {
        ranNum = Random.Range(0, 3);
        {
            if (ranNum == 0)
            {
                Radio1();
            }
            if (ranNum == 1)
            {
                Radio2();
            }
            if (ranNum == 2)
            {
                Radio3();
            }
        }
    }

    public void Radio1()
    {
        StartCoroutine(Rad1());
    }

    IEnumerator Rad1()
    {
        Audio1.Play();
        yield return new WaitForSeconds(188f);
        Radio2();
    }

    public void Radio2()
    {
        StartCoroutine(Rad2());
    }

    IEnumerator Rad2()
    {
        Audio2.Play();
        yield return new WaitForSeconds(187f);
        Radio3();
    }

    public void Radio3()
    {
        StartCoroutine(Rad3());
    }

    IEnumerator Rad3()
    {
        Audio3.Play();
        yield return new WaitForSeconds(183f);
        Radio1();
    }
}
