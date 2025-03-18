using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficlight : MonoBehaviour
{
    int firstP=0, secondP=0, thirdP=0, fourP=0;
    GameObject tlight, tred, tyellow, tgreen;
    public Material oldRed, red, oldYellow, yellow, oldGreen, green;
    // Start is called before the first frame update
    void Start()
    {
        tlight = GameObject.Find("tlight1");
        tred = tlight.transform.GetChild(3).gameObject;
        tyellow = tlight.transform.GetChild(1).gameObject;
        tgreen = tlight.transform.GetChild(2).gameObject;

        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        while(true)
        {
        firstP = 1; // on start is red
        tred.GetComponent<Renderer>().material = red; 
        yield return new WaitForSeconds(3);

        //wait for 3 seconds- to phrase II
        firstP = 0; secondP = 1; // already is red & yellow
        tyellow.GetComponent<Renderer>().material = yellow;
        yield return new WaitForSeconds(3);

        //wait for 3 seconds- to phrase III
        tred.GetComponent<Renderer>().material = oldRed;
        tyellow.GetComponent<Renderer>().material = oldYellow;
        tgreen.GetComponent<Renderer>().material = green;
        secondP = 0; thirdP = 1; // already is green
        yield return new WaitForSeconds(3);

        //wait for 3 seconds- to phrase IV
        tgreen.GetComponent<Renderer>().material = oldGreen;
        tyellow.GetComponent<Renderer>().material = yellow;
        thirdP = 0; fourP = 1; // already is yellow
        yield return new WaitForSeconds(3);

        //wait for 3 seconds- to phrase I
        tred.GetComponent<Renderer>().material = red;
        tyellow.GetComponent<Renderer>().material = oldYellow;
        fourP = 0; firstP = 1; // already is red; 
        yield return new WaitForSeconds(3);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
