using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSetup : MonoBehaviour
{
	bool collides;
	
	public bool isColliding
	{
		get { return collides;}
	}
    
    void OnCollisionEnter(Collision collision)
    {
    	collides = ChangeStatus(collision);
    }
    
    void OnCollisionExit(Collision collision)
    {
    	collides = ChangeStatus(collision);
    }
    
    void OnCollisionStay(Collision collision)//almost forgot about this one
    {
    	collides = ChangeStatus(collision);
    }
    
    bool ChangeStatus(Collision collision)//pass-through of a parameter yea-yea
    {
		return collision.contacts.Length > 0;
    }
}
