using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollector : MonoBehaviour
{
    private Transform shardTransform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        shardTransform = GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        //rotate the shards around the z axis
        transform.Rotate(0, 0, 90 * Time.deltaTime);

        // if the player is 15 metres away destroy the shard
        if (transform.position.z + 15 < PlayerScript.zDisplacement) {
            Destroy(gameObject);
            
        }
        //if magnet is active and the player is 10 metres away move towards the player
        if (PlayerScript.isMagnet && (transform.position.z - 10) < PlayerScript.zDisplacement) {
            transform.position = Vector3.MoveTowards(transform.position, PlayerScript.playerPosition, 100*Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {   
        //if the shard is collected the number of shards is incremented and the object is destroyed as well as the sound is played
        if (other.tag == "Runner") 
        {
            GameManager.shardsCollected++;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().PlaySound("shardPickup");

        }
        //if a shard spawns inside an obstacle it is destroyed
        if (other.tag == "Obstacle")
        {
            Destroy(gameObject);

        }
    }
}
