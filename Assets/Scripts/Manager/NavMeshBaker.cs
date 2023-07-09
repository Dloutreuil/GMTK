using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface Surface2D;

    private static NavMeshBaker instance;
    public static NavMeshBaker Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void BakeNavMesh()
    {
        Surface2D.BuildNavMeshAsync();
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
        //NavMeshBuilder.UpdateNavMeshDataAsync(data, GetBuildSettings(), sources, sourcesBounds);
        Debug.Log("baked nav");
    }

    private void Update()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
}
