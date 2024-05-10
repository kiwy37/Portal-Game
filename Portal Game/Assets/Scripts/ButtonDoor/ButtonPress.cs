using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public AudioClip sound;
    private int number;
    private DoorOpen action;
    void Start()
    {
        number = 0;
        action = door.GetComponent<DoorOpen>();
    }

    public void OnTriggerEnter(Collider other)
    {
        tag = other.gameObject.tag;
        if (tag == "Box" || tag == "Player")
        {
            number++;
            if (number == 1) SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (tag == "Box" || tag == "Player")
        {
            number--;
            if (number == 0) SoundFXManager.Instance.PlaySoundFXClip(sound, transform, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (number == 0) action.isClosed = true;
        else action.isClosed = false;
    }
}
