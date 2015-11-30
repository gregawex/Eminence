using System.Collections;
using UnityEngine;

class LoadAssetFromAssetBundle : MonoBehaviour
{
	public static LoadAssetFromAssetBundle Instance;

	void Awake()
	{
		Instance = this; 
	}

	public Object Obj;
	
	public IEnumerator DownloadAssetBundle<T>(string asset, string url, int version) where T : Object {
		Obj = null;
		
		#if UNITY_EDITOR
		Obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/" + asset, typeof(T));
		yield return null;
		
		#else
		// Wait for the Caching system to be ready
		while (!Caching.ready)
			yield return null;
		
		// Start the download
		using(WWW www = WWW.LoadFromCacheOrDownload (url, version)){
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download:" + www.error);
			AssetBundle assetBundle = www.assetBundle;
			Obj = assetBundle.LoadAsset(asset, typeof(T));
			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload(false);
			
		} // memory is freed from the web stream (www.Dispose() gets called implicitly)
		
		#endif
	}
}