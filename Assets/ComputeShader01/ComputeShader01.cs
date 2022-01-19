using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputeShader01 : MonoBehaviour
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
        // 512/8=64 ���Ǹ�x��y�������64���߳��� 
        // �������Ϊ 512*512��ͼ�񱻺�����������64�Σ���64*64��С���ӣ�ÿ��������һ���߳�����ִ��
        // ÿ���߳�����Ҫ���ú��������ڵ���8���߳�����ִ�У�Ҳ����numthreadsҪ���ô��ڵ���8*8����Ȼͼ��Ͳ�����ȫ����
        shader.Dispatch(k, inputTexture.width/8, inputTexture.height/8, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
