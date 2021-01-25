using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenNeeded : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Update1Sec", 0f, 1f);
    }

    private void Update1Sec()
    {
        if (player.transform.position.y - this.transform.position.y > 20) Destroy(this.gameObject);
    }
}
