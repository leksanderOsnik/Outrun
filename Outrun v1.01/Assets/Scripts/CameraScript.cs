using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform runner;
    private Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {   //calculates distance between runner and camera
        distance = transform.position - runner.position;
    }

    // Update is called once per frame
    void Update()
    {
        //sets the new position of the camera the same distance away from the player
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, distance.z + runner.position.z);
        transform.position = newPosition;

    }

    public void SetTransform(Transform t) {
        //sets the transform of the player
        runner = t;
    }


}
