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

        // 假如图片像素为512*512 
        // 512/8=64 就是给x和y方向各分64个线程组 
        // 可以理解为 512*512的图像被横竖各划分了64次，有64*64个小格子，每个格子由一个线程组来执行
        // 每个线程组需要设置横竖各大于等于8的线程数来执行，也就是numthreads要配置大于等于8*8，不然图像就不能完全绘制
        shader.Dispatch(k, inputTexture.width/8, inputTexture.height/8, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
