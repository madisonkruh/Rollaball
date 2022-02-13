using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // references Player's position
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // current transform position of Camera - transform position of Player  
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame, but runs after all other updates are done
    // so Camera position is only set once player has moved for that frame
    void LateUpdate()
    {
        // adjusts Camera position whenever Player moves
        transform.position = player.transform.position + offset;
    }
}