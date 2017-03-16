using UnityEngine;
using System.Collections;

public class Canvas3 : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;
    int TapFlag = 1;
    // Use this for initialization  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {

    }

    private void OnTap()
    {
        if (TapFlag == 1)
        {
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            
            TapFlag = 0;
        }
        else {
            canvas1.SetActive(true);
            canvas2.SetActive(true);
            canvas3.SetActive(true);
            TapFlag = 1;
        }
       
    }
}