using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuimButton : MonoBehaviour {
    public GameObject PreUI;
    public GameObject NextUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTap()
    {
        NextUI.SetActive(true);
        GameObject.Find("rack").GetComponent<testPUI>().enabled = false;
        PreUI.SetActive(false);       
    }
}
