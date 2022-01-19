using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputeShader02 : MonoBehaviour
{

    public Texture2D inputTexture;
    public RawImage outputImage;
    public ComputeShader shader;
    // Start is called before the first frame update
    void Start()
    {
        RenderTexture t = new RenderTexture(inputTexture.width, inputTexture.height, 24);
        t.enableRandomWrite = true;
        t.Create();
        outputImage.texture = t;

        int k = shader.FindKernel("CSMain");
        shader.SetTexture(k, "inputTexture", inputTexture);
        shader.SetTexture(k, "outputTexture", t);
        
        // ����ͼƬ����Ϊ512*512 
        // 512/8=64 ��x��y�������64���߳��� 
        // 
        shader.Dispatch(k, 60, 60, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
