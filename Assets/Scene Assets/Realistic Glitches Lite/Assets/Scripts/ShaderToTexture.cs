using UnityEngine;
using System.Collections;

public class ShaderToTexture : MonoBehaviour {

	public ComputeShader shader;
	
	void RunShader()
	{

		int kernelHandle = shader.FindKernel("CSMain");
		
		RenderTexture tex = new RenderTexture(256,256,24);
		tex.enableRandomWrite = true;
		tex.Create();
		
		shader.SetTexture(kernelHandle, "Result", tex);
		shader.Dispatch(kernelHandle, 256/8, 256/8, 1);
	}

	// Use this for initialization
	void Start () {
		RunShader();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
