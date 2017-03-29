using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_ChangeScene : MonoBehaviour {
    public string SceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTap()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
