using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseCheckRadius : MonoBehaviour
{
    public static MouseCheckRadius instance;
    public Transform player;
    private bool down = false;
    void Start()
    {
        if (instance == null) instance = this;
    }
    void Update()
    {
        transform.position = player.position;
    }
    public static bool IsDown()
    {
        return instance.down;
    }
    private void OnMouseExit()
    {
        down = false;
    }
    private void OnMouseDown()
    {
        down = true;
    }
    private void OnMouseUp()
    {
        down = false;
    }
}
