using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Persistence;
using System;
using UnityEngine.VR.WSA;
using Academy.HoloToolkit.Unity;

public class myAnchor : MonoBehaviour
{

    public string ObjectAnchorStoreName;

    WorldAnchorStore anchorStore;

    //bool Placing = false;

    void Start()
    {
        //获取WorldAnchorStore 对象  
        WorldAnchorStore.GetAsync(AnchorStoreReady);
    }

    private void AnchorStoreReady(WorldAnchorStore store)
    {
        anchorStore = store;
        string[] ids = anchorStore.GetAllIds();
        //遍历之前保存的空间锚，载入指定id场景对象信息  
        for (int index = 0; index < ids.Length; index++)
        {
            if (ids[index] == ObjectAnchorStoreName)
            {
                WorldAnchor wa = anchorStore.Load(ids[index], gameObject);
                break;
            }
        }
    }

    // Update is called once per frame  
    void Update()
    {
        //if (Placing)
        //{
        //    //当Cube处于可移动状态，根据凝视射线的位置，更新Cube的位置  
        //    gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        //}
    }

    //void OnSelect()
    void OnTap()
    {
        if (anchorStore == null)
        {
            return;
        }

        //if (Placing)
        if (!GestureManager.IsManipulating)
            {
            //**当再次点击全息对象时，保存空间锚信息  
            //静止时保存空间锚信息
            WorldAnchor attachingAnchor = gameObject.AddComponent<WorldAnchor>();
            if (attachingAnchor.isLocated)
            {
                bool saved = anchorStore.Save(ObjectAnchorStoreName, attachingAnchor);
            }
            else
            {
                //有时空间锚能够立刻被定位到。这时候，给对象添加空间锚后，空间锚组件的isLocated属性  
                //值将会被设为true，这时OnTrackingChanged事件将不会被触发。因此，在添加空间锚组件  
                //后，推荐立刻使用初始的isLocated状态去调用OnTrackingChanged事件  
                attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
            }
        }
        else
        {
            //当全息对象已附加空间锚组件后，它不能被移动。如果你需要移动全息对象的话，那么你必须这样做：  
            //1.立刻销毁空间锚组件  
            //2.移动全息对象  
            //3.添加一个新的空间锚到全息对象上  
            WorldAnchor anchor = gameObject.GetComponent<WorldAnchor>();
            if (anchor != null)
            {
                DestroyImmediate(anchor);
            }

            string[] ids = anchorStore.GetAllIds();
            for (int index = 0; index < ids.Length; index++)
            {
                if (ids[index] == ObjectAnchorStoreName)
                {
                    bool deleted = anchorStore.Delete(ids[index]);
                    break;
                }
            }
        }

        //Placing = !Placing;
    }

    private void AttachingAnchor_OnTrackingChanged(WorldAnchor self, bool located)
    {
        if (located)
        {
            bool saved = anchorStore.Save(ObjectAnchorStoreName, self);
            self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
        }
    }
}