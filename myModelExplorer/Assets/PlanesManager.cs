using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class PlanesManager : Singleton<PlanesManager>
{
    [Tooltip("When checked, the SurfaceObserver will stop running after a specified amount of time.")]
    public bool limitScanningByTime = true;

    [Tooltip("How much time (in seconds) that the SurfaceObserver will run after being started; used when 'Limit Scanning By Time' is checked.")]
    public float scanTime = 30.0f;

    [Tooltip("Material to use when rendering Spatial Mapping meshes while the observer is running.")]
    public Material defaultMaterial;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    [Tooltip("Minimum number of floor planes required in order to exit scanning/processing mode.")]
    public uint minimumFloors = 1;

    [Tooltip("Minimum number of wall planes required in order to exit scanning/processing mode.")]
    public uint minimumWalls = 1;

    /// <summary>  
    /// 标记表面网格是否处理完成  
    /// </summary>  
    private bool meshesProcessed = false;

    private void Start()
    {
        //设置Surface表面材质  
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);
        //注册生成平面后的回调事件  
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    /// <summary>  
    /// 每帧调用  
    /// </summary>  
    private void Update()
    {
        //检查表面网格是否处理完成，且扫描空间的时间是否有限制  
        if (!meshesProcessed && limitScanningByTime)
        {
            //检查是否超出了扫描上限时间  
            if (limitScanningByTime && ((Time.time - SpatialMappingManager.Instance.StartTime) < scanTime))
            {
            }
            else
            {
                //当达到扫描的时间，停止对空间的扫描映射  
                if (SpatialMappingManager.Instance.IsObserverRunning())
                {
                    SpatialMappingManager.Instance.StopObserver();
                }
                //创建平面  
                CreatePlanes();
                //标记表面网格处理完成  
                meshesProcessed = true;
            }
        }
    }

    /// <summary>  
    /// 当SurfaceMeshesToPlanes中 生成Planes处理完成，调用该事件  
    /// </summary>  
    /// <param name="source">事件源</param>  
    /// <param name="args">事件参数</param>  
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        //水平平面列表(地面，桌面等)  
        List<GameObject> horizontal = new List<GameObject>();
        //垂直平面列表(墙面等垂直面)  
        List<GameObject> vertical = new List<GameObject>();
        //获取所有的水平平面  
        horizontal = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Table | PlaneTypes.Floor);
        //获取所有的垂直平面  
        vertical = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Wall);
        //检查垂直平面和水平平面的数量是否达到了最少设置的数量，如果未达到，重新进行空间扫描  
        if (horizontal.Count >= minimumFloors && vertical.Count >= minimumWalls)
        {
            //删除顶点  
            RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);
            //设置第二表面材质  
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
            //获取平面后，在世界中生成物体，传入参数为上面获取到的，水平平面，垂直平面的列表  
           //ObjectCollectionManager.Instance.GenerateItemsInWorld(horizontal, vertical);
        }
        else
        {
            //未扫描到可以放置物体的垂直平面和水平平面，重新进行空间扫描  
            SpatialMappingManager.Instance.StartObserver();
            //重新标记表面网格未处理完成  
            meshesProcessed = false;
        }
    }

    /// <summary>  
    /// 将扫描得到的空间映射信息转换成平面  
    /// </summary>  
    private void CreatePlanes()
    {
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            surfaceToPlanes.MakePlanes();
        }
    }

    /// <summary>  
    /// 从空间映射中删除生成的三角形  
    /// </summary>  
    /// <param name="boundingObjects"></param>  
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    /// <summary>  
    /// 释放资源  
    /// </summary>  
    private void OnDestroy()
    {
        if (SurfaceMeshesToPlanes.Instance != null)
        {
            SurfaceMeshesToPlanes.Instance.MakePlanesComplete -= SurfaceMeshesToPlanes_MakePlanesComplete;
        }
    }
}
