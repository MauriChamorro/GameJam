using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour {

    public enum KeyItems { 
        Window = 1,
        Courtain = 2,
    }

    public KeyItems Key;

    public delegate void OnItemCollected(KeyItems key);

    public static event OnItemCollected ItemCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player") {
            if (ItemCollected != null)
            {
                ItemCollected(this.Key);
                Destroy(this.gameObject);
            }
            
        }
    }
}
