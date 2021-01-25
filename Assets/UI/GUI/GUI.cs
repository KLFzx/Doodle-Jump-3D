using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    private Transform player;
    private TMP_Text currentPoints;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPoints = GameObject.FindGameObjectWithTag("CurrentPoints").GetComponent<TMP_Text>();
    }
    public void Restart()
    {
        player.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        currentPoints.text = Mathf.Max ( ((int)player.position.y), int.Parse(currentPoints.text) ).ToString();
    }
}
