using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlScript : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = 1;
    GameObject girlT;
    
    // Start is called before the first frame update
    void Start()
    {
        
        girlT= GameObject.Find("girl");

        
    }
        


    // Update is called once per frameasda
    void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
                Debug.Log("dzia³a");
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        //girlT.transform.position = girlT.transform.position + new Vector3(0, 0, 0.925f);

    }
}
