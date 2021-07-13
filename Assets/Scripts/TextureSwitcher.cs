using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TextureSwitcher : MonoBehaviour
{
    [SerializeField] List<Texture2D> AvailableTextures;
    [SerializeField] float TextureSwitchInterval = 10f;

    float TimeUntilNextSwitch = 0f;
    
    MeshRenderer LinkedMR;
    MaterialPropertyBlock PropertyBlock;

    // Start is called before the first frame update
    void Start()
    {
        LinkedMR = GetComponent<MeshRenderer>();

        // create the property block
        PropertyBlock = new MaterialPropertyBlock();


    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilNextSwitch -= Time.deltaTime;
        if (TimeUntilNextSwitch <= 0)
        {
            TimeUntilNextSwitch = TextureSwitchInterval;

            // set a random texture
            PropertyBlock.SetTexture("_MainTex", AvailableTextures[Random.Range(0, AvailableTextures.Count)]);

            // apply the property block
            LinkedMR.SetPropertyBlock(PropertyBlock);            
        }

    }
}
