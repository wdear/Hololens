using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> nail;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTap()
    {
        if(nail[1].activeSelf == false && nail[2].activeSelf == false && nail[3].activeSelf == false && nail[0].activeSelf == false)
        this.gameObject.SetActive(false);
    }
}
