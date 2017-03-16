using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuiM : MonoBehaviour {
    public List<GameObject> Pre;
    public GameObject Next;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(PreDone())
        {
            Next.SetActive(true);
            this.gameObject.SetActive(false);
        }
		
	}

    bool PreDone()
    {
        bool PreFlag = true;
        foreach (GameObject x in Pre)
        {
            if (x.activeSelf == true)
            {
                PreFlag = false;
                break;
            }
            
        }
        return PreFlag;
    }
}
