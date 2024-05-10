using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool isClosed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider>().enabled = isClosed;
        GetComponent<MeshRenderer>().enabled = isClosed;
    }
}
