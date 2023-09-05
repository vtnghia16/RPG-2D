using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    private float length;

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }


    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        // BG dịch chuyển theo nhân vật
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);


        // BG chạy theo nhân vật
        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        { 
            xPosition = xPosition - length;
        }
    }
}
