
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Transform APos;
    public Transform BPos;
    public GameObject currentPoint;
    public PlayerPickUp playerPickUp;
    private static string PLAYER = "Player";
    private static string BOX = "Box";


    private void Awake()
    {
        playerPickUp = FindObjectOfType<PlayerPickUp>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Portal A"))
        {
            Teleport(BPos);
        }
        else if (col.CompareTag("Portal B"))
        {
            Teleport(APos);
        }
    }

    // Coroutine for teleportation
    private void Teleport(Transform destination)
    {
        if (currentPoint.tag == PLAYER)
        {
            CharacterController cc = GetComponent<CharacterController>();
            cc.enabled = false;
            if (playerPickUp.heldObj != null)
            {
                playerPickUp.StopClipping();
                playerPickUp.DropObject();
            }
            transform.position = destination.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, destination.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            cc.enabled = true;
            if (destination.up == new Vector3(0, 1, 0))
            {
                var playermove = currentPoint.GetComponent<playerMove>();
                playermove.GainVelocity();
            }
        }
        else if (currentPoint.tag == BOX)
        {
            Collider cc = GetComponent<Collider>();
            cc.enabled = false;
            transform.position = destination.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, destination.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            cc.enabled = true;
            var body = currentPoint.GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            body.AddForce(destination.up * 300f);
        }
    }
}

