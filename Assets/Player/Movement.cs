using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody player;
    private Joystick joystick;
    private Transform cursor;

    [SerializeField]
    private int screenScaler;
    private void Start()
    {
        screenScaler = Screen.currentResolution.width;
        player = this.GetComponent<Rigidbody>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        cursor = joystick.transform.GetChild(0).transform.GetChild(0).transform;

        

    }

    Vector3 counterMovement = new Vector3(0.2f, 0f, 0f);
    void CounterMovement(float X)
    {
        if (X == 0 && player.velocity.x  > 0) player.velocity -= counterMovement ; 
        if (X == 0 && player.velocity.x < 0) player.velocity += counterMovement ; 
    }



    Quaternion desiredRotQR = Quaternion.Euler(0f, 90f, 0f);
    Quaternion desiredRotQL = Quaternion.Euler(0f, -90f, 0f);
    void Update()
    {

        float X = joystick.Horizontal;


     
        player.AddForce(new Vector3(X, 0f, 0f) * 20);
        //if ( X!= 0 ) player.transform.GetComponent<MeshFilter>().mesh = Instantiate(Resources.Load("Voxel Models/PlayerDiablo3.fbx") ) as Mesh;

        CounterMovement(X);

        if ( X == 0 ) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,180f,0f), Time.deltaTime * 2);
        else if (X > 0) transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQR, Time.deltaTime * 2);
        else transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQL, Time.deltaTime * 2);

        player.velocity = new Vector3(Mathf.Clamp(player.velocity.x, -9, 9 ) , player.velocity.y, Mathf.Clamp(player.velocity.z, -20, 20));

        ClampZCoordinate();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && player.velocity.y <= 1f ) player.AddForce(new Vector2(0f, CONSTANTS.JumpForce) );
        BatootAndSpringHandle(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "PlatformTrigger") MakeTransparentPlatform(other.transform.parent.GetChild(0).gameObject);

        if (other.tag == "RightEndScreen") player.transform.position = new Vector3(-player.position.x + 3, player.position.y, player.position.z);
        if (other.tag == "LeftEndScreen") player.transform.position = new Vector3(-player.position.x , player.position.y, player.position.z);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlatformTrigger")
        {
            GameObject platform = other.transform.parent.GetChild(0).gameObject;
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            platform.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1f);
        }

  
    }

    void ClampZCoordinate()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
    }

    Vector3 rot180 = new Vector3(0f, 180f, 0f);

    void BatootAndSpringHandle(Collision collision)
    {
        if (collision.gameObject.tag == "Batoot")
        {
            player.AddForce(new Vector2(0f, CONSTANTS.BatootForce));
           
        }
        if (collision.gameObject.tag == "Spring") player.AddForce(new Vector2(0f, CONSTANTS.SpringForce));
    }

    void MakeTransparentPlatform(GameObject platform)
    {
        Physics.IgnoreCollision(platform.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        platform.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
    }

   

}
