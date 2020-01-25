using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Coins;
    playercontroller playercontroller;
    public List<Vector2> PosList = new List<Vector2>();
    public float radius;
    float dist;
    public float offset = 30;
    public int maxcoin = 5;
    float radiusrot = 90;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playercontroller playercontroller = Player.GetComponent<playercontroller>();
        dist = playercontroller.transform.position.z;
        Debug.Log(playercontroller.transform.position.z);
        var position1 = new Vector3(Random.Range(radius * -1, radius), 1 , Random.Range(radius * -1, radius) + dist + offset);
        var position2 = new Vector3(Random.Range(radius * -1, radius), 1, Random.Range(radius * -1, radius) + dist + offset);
        var rotationvec = new Vector3(0, Random.Range(radiusrot -20, radiusrot + 20), 0);
        Quaternion rotation = Quaternion.Euler(rotationvec);
        for (int x = 0; x < maxcoin; x++)
        {
            GameObject coinclone = Instantiate(Coin, position1, transform.rotation);
        }

        Instantiate(Coins, position2 , rotation);

    }
       
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
