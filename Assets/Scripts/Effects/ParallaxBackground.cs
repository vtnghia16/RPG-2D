using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition; // Vị trí hiện tại của hoạt cảnh
    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x; // Lấy vị trí hiện tại của hoạt cảnh
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); // KC đã di chuyển của camera
        float distanceToMove = cam.transform.position.x * parallaxEffect; // KC di chuyển của camera

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        // Vị trí sẽ tăng theo vị trí của nhân vật và ngược lại.
        if (distanceMoved > xPosition + length) 
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;

    }
}
