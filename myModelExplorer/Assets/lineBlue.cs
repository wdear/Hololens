using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineBlue : MonoBehaviour {

    public GameObject preMs;
    public GameObject errorMs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTap()
    {
        errorMs.SetActive(true);
        preMs.SetActive(false);
        Invoke("popMs", 2);
    }

    void popMs()
    {
        errorMs.SetActive(false);
        preMs.SetActive(true);
    }
}
