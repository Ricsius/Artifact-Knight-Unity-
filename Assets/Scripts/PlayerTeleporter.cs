using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform PlayerTransform;
    public Vector2 Position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.name == "Player")
        {
            otherCollider.gameObject.transform.position = Position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
