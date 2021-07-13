using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureModifier : MonoBehaviour
{
    [SerializeField] RenderTexture LinkedTexture;
    [SerializeField] Camera LinkedCamera;
    [SerializeField] bool PerformModification = false;

    Texture2D WorkingTexture;

    // Start is called before the first frame update
    void Start()
    {
        // create our working texture
        WorkingTexture = new Texture2D(LinkedTexture.width, LinkedTexture.height, LinkedTexture.graphicsFormat, 0, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (PerformModification)
        {
            PerformModification = false;

            // make our LinkedTexture active and store the previous
            var previousRT = RenderTexture.active;
            RenderTexture.active = LinkedTexture;

            // force the linked camera to render to the texture
            LinkedCamera.Render();

            // read from the render texture (GPU side) into our Texture 2D (CPU side)
            WorkingTexture.ReadPixels(new Rect(0, 0, LinkedTexture.width, LinkedTexture.height), 0, 0);
            WorkingTexture.Apply();

            // restore the previous RT
            RenderTexture.active = previousRT;

            // modify the texture
            Color[] imagePixels = WorkingTexture.GetPixels();
            for(int index = 0; index < imagePixels.Length; ++index)
            {
                float avgValue = (imagePixels[index].r + imagePixels[index].g + imagePixels[index].b) / 3f;
                imagePixels[index] = new Color(avgValue, avgValue, avgValue, 1f);
            }
            WorkingTexture.SetPixels(imagePixels);
            WorkingTexture.Apply();

            string filePath = "OutputTexture.png";
            System.IO.File.WriteAllBytes(filePath, WorkingTexture.EncodeToPNG());
        }
    }
}
