using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player died!");
    }
}