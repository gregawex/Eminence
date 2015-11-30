using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles")]
	public static void BuildAllAssetBundles (string outputPath, bool compressed)
	{
		if(compressed)
			BuildPipeline.BuildAssetBundles (outputPath);
		else 
			BuildPipeline.BuildAssetBundles (outputPath, BuildAssetBundleOptions.UncompressedAssetBundle);
	}
}
