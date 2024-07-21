using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCircle : StaticInstance<DespawnCircle>
{
    
    public static float radius = 100.0f;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.Die();
        }
        else if (other.CompareTag("NPC"))
        {
            Destroy(other.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        
        // draw circle with radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}
