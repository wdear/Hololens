using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Academy.HoloToolkit.Unity
{
    public class testPUI : MonoBehaviour
    {
        public GameObject cchild;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (this.name == GazeManager.Instance.HitInfo.transform.name)
                {
                    cchild.SetActive(true);
                }
                else if(GazeManager.Instance.Hit && !GazeManager.Instance.HitInfo.transform.CompareTag("Button"))
                {
                    cchild.SetActive(false);
                }
           
        }
    }
}

