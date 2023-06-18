using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAndWhiteFilter : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            material.SetFloat("_GrayscaleAmount", 1f); // Apply full black and white filter
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            material.SetFloat("_GrayscaleAmount", 0f); // Remove the black and white filter
        }
    }
}
