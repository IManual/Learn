using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {

        //Instantiate(AssetLoaderManager.Instance.GetSingleResources("cube"));
        AssetLoaderManager.Instance.Init("SampleScene", "samplescene/prefabs/capsule.unity3d", "samplescene/prefabs/cube.unity3d");

        yield return AssetLoaderManager.Instance.LoadAssetSys("samplescene/prefabs/capsule.unity3d", "samplescene/prefabs/cube.unity3d");

        AssetLoaderManager.Instance.DebugAllAsset();
        Instantiate(AssetLoaderManager.Instance.GetSingleResources("Cube.prefab"));
        Instantiate(AssetLoaderManager.Instance.GetSingleResources("Capsule.prefab"));

        AssetLoaderManager.Instance.UnloadAsset("Cube.prefab");
        Instantiate(AssetLoaderManager.Instance.GetSingleResources("Capsule.prefab"));
        Instantiate(AssetLoaderManager.Instance.GetSingleResources("Cube.prefab"));
        Instantiate(AssetLoaderManager.Instance.GetSingleResources("Capsule.prefab"));
        yield return 0;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
