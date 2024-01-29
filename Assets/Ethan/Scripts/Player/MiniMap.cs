using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    Transform player;

    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
