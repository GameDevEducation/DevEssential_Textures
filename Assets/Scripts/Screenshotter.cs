using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshotter : MonoBehaviour
{
    [SerializeField] bool TakeScreenshot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TakeScreenshot)
        {
            TakeScreenshot = false;

            ScreenCapture.CaptureScreenshot("Screenshot_" + Time.frameCount + ".png", 2);
        }
    }
}
