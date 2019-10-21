using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = .1f;
    Material backgroundMaterial; //used later for connecting the component in Unity inspector for scrolling
    Vector2 offset; //used for "moving" background by offsetting the y direction

    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material; //how we're getting the material on the background
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime; //Time.deltaTime makes the moving frame rate independent
            //.mainTextureOffset is how we get access to the component
    }
}
