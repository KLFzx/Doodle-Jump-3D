using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleMainMenu : MonoBehaviour
{
    [SerializeField] private Rigidbody player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Rigidbody>();;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && player.velocity.y <= 1f) player.AddForce(new Vector2(0f, CONSTANTS.JumpForce/1.2f));
    }
}
