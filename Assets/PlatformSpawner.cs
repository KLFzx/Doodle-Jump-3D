using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{



    int lastPlatformY;
    int lastBackgroundY;
    private Transform player;

    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> coolStuff = new List<GameObject>();
    List<GameObject> backGround = new List<GameObject>();


    private void Start()
    {

       

        lastPlatformY = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        platforms.Add(Resources.Load("Platforms/Platform") as GameObject);
        platforms.Add(Resources.Load("Platforms/PlatformMoving") as GameObject);
        platforms.Add(Resources.Load("Platforms/Platform1Time") as GameObject);


        coolStuff.Add(Resources.Load("Cool Stuff/PortalBlue") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/Up") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/JetpackObj") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/SpringObj") as GameObject);
        

        backGround.Add(Resources.Load("Background/Cloud1") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud2") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud3") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud4") as GameObject);

    }


    void CoolStuffSpawn(GameObject platform)
    {
        if ( Random.Range ( 0, 10 ) == 2 )
        {
            GameObject thing = Instantiate(coolStuff[Random.Range(0, coolStuff.Count)]);
            thing.transform.parent = platform.transform.GetChild(0);
            thing.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + 0.4f);

            if (thing.tag == "Spring" || thing.tag == "Batoot") platform.transform.GetChild(0).GetChild(0).tag = thing.tag;
            
        }
    }

    void BackgroundSpawn()
    {
        if (Mathf.Abs(player.transform.position.y - lastBackgroundY) < 16f)
        {
            GameObject backgroundObj = Instantiate(backGround[Random.Range(0, backGround.Count)]);
            backgroundObj.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            backgroundObj.transform.parent = this.transform;
            backgroundObj.transform.position = new Vector3(Random.Range(-4f, 5f), lastBackgroundY + Random.Range(3, 7) * 3,7);
           
            lastBackgroundY = (int)backgroundObj.transform.position.y;
        }
    }


    private void FixedUpdate()
    {
        if (Mathf.Abs(player.transform.position.y - lastPlatformY) < 15f )
        {
            GameObject platform = Instantiate(platforms[Random.Range(0, platforms.Count)]);
            platform.transform.parent = this.transform;
            platform.transform.position = new Vector3(Random.Range(-3f, 4f), lastPlatformY + Random.Range(3,6));
            if (platform.transform.position.x < -2.5f  )
            {
                GameObject platform2 = Instantiate(platforms[0]);
                platform2.transform.parent = this.transform;
                platform2.transform.position = new Vector3(Random.Range(0f, 4f), platform.transform.position.y);
            }
            lastPlatformY = (int) platform.transform.position.y;
            if ( player.childCount == 0 ) CoolStuffSpawn(platform);
        }
        BackgroundSpawn();
    }
}
