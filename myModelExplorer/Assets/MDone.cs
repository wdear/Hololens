using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDone : MonoBehaviour {

    public List<GameObject> restartObj;
	// Use this for initialization
	void Start () {
        Invoke("restartM", 5);
        Destroy(this.gameObject, 5);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void restartM()
    {
        GameObject.Find("rack").GetComponent<testPUI>().enabled = true;
        foreach(GameObject x in restartObj)
        {
            x.SetActive(true);
        }

    }
}
