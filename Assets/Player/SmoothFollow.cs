using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform player;
    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector2(transform.position.x, player.position.y + 7f),Time.deltaTime);
    }
}
