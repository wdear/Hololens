using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class in004 : MonoBehaviour {

    public GameObject hideUI;
    public GameObject showUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTap()
    {
        hideUI.SetActive(false);
        showUI.SetActive(true);
        Destroy(showUI.gameObject, 5);
        Invoke("restartUI", 5);
    }

    private void restartUI()
    {
        GameObject.Find("rack").GetComponent<testPUI>().enabled = true;
    }
}
