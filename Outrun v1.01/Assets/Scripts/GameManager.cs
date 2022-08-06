using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Gameobjects used by the script
    public GameObject cameraObject;
    public GameObject[] tileArray;
    public GameObject runnerObject;
    public GameObject shard;
    public GameObject magnetPowerUp;

    //variables responsible for the tiles
    public float tileSpawn= 0;
    public float tileOffset = 50;
    public int visibleTiles = 6;
    public static int tilesSpawned = 0;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerLocation;

    //variables responsible for the tiles and the score
    public static int shardsCollected;
    private float[] shardXLocations = { -2.7f, 0, 2.7f };
    private int visibleShards = 10;
    private float shardSpawn = 15;
    private float shardOffset = 5;
    public int score;
    public static int highScore;
    public Text scoreText, endScoreText, highScoreText;
   
    //variables responsible for the magnet power up
    public Text countdown;
    public static float timeLeft;
    private bool spawnMagnet;
   
    void Start()
    {
        //if the game is not in game spawn only the empty tiles to allow for an animated moving menu
        if (GameStateManager.gameState != 1)
        {
           //spawns 6 tiles 
            for (int i = 0; i < visibleTiles; i++)
            {
                SpawnTile(0);
            }

        }

        else {
            //when game starts set the collected shards to 0
            shardsCollected = 0;
            //spawn 6 tiles at the start
            for (int i = 0; i < visibleTiles; i++)
            {
                //first 2 empty to let the player spawn safely
                if (i < 2)
                    SpawnTile(0);
                else//the tiles are chose at random from the array
                    SpawnTile(Mathf.RoundToInt(Random.Range(0, tileArray.Length)));
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

        //if the is magnet is false do not display the countdown timer and reset the timer
        if (!PlayerScript.isMagnet)
        {
            float duration = 7.0f;
            timeLeft = duration;
            countdown.text = "";
        }
        //if the is magnet is true display the timer
        if (PlayerScript.isMagnet) 
        {   
            
            timeLeft -= 1 * Time.deltaTime;
            countdown.text = System.Math.Round(timeLeft,2) + "S";
        }
        
        //if the state is in game
        if (GameStateManager.gameState == 1)
        {   
            //calculate and display score by multiplying the number of shards by 10
            score = shardsCollected * 10;
            scoreText.text = ("SCORE: " + score);
            //if current score is higher than the high score set new highscore
            if (score > highScore)
                highScore = score;

            //if the players location - 50 (safe zone) is greater than the the next tile zPosition - the amount of visible tiles multiplied by the tileLength
            //Instantiate and destroy 1 tile game object
            if (playerLocation.position.z - 50 > tileSpawn - (visibleTiles * tileOffset))
            {
                    SpawnTile(Mathf.FloorToInt(Random.Range(0, tileArray.Length)));
                DespawnTile();
            }
            //spawn shards
            if (playerLocation.position.z - 5 > shardSpawn - (visibleShards * shardOffset))
            {
                //spawn the shard by choosing a random x location (lane)
                SpawnShard(Mathf.FloorToInt(Random.Range(0, shardXLocations.Length)));
            }
        }
        //if the game is in menu or game over screen keep spawning only empty tiles
        else if(GameStateManager.gameState == 0 || GameStateManager.gameState == 2)
        {
            if (playerLocation.position.z - 50 > tileSpawn - (visibleTiles * tileOffset))
            {
                if (tileArray.Length != 0)
                {
                    SpawnTile(0);
                    DespawnTile();
                }
            }
        }
        //when game over screen is present display Score and high Score
        if (GameStateManager.gameState == 2) 
        {
            endScoreText.text = ("SCORE: " +score);
            highScoreText.text = ("HIGHSCORE: " + highScore);
        }

        //every 9 tiles allow the magnet to be spawned
        if ((tilesSpawned + 1) % 10 == 0) 
        {
            spawnMagnet = true;
        }
        // every 10 tiles spawn the magnet
        if (tilesSpawned % 10 == 0 && spawnMagnet) {
            SpawnPowerUp();
        }
        

    }


    //instantiates the magnet in a similar manner to the shard
    public void SpawnPowerUp() 
    {

            
        Instantiate(magnetPowerUp, new Vector3(0, 1.5f, shardSpawn), Quaternion.identity);
        spawnMagnet = false;
        
    
    }

   
    //spawn a tile and add it to the list of active tiles
    public void SpawnTile(int t) 
    {   //every second tile rotate everything by 180 degrees around the y axis to ensure proper showcase of the used prefabs
        if ((tilesSpawned % 2) == 0)
        {
            
            GameObject go = Instantiate(tileArray[t], transform.forward * tileSpawn, transform.rotation);
            activeTiles.Add(go);
        }
        else
        {
            
            GameObject go = Instantiate(tileArray[t], transform.forward * tileSpawn, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            activeTiles.Add(go);
        }
        tilesSpawned++; //increment the number of tiles spawned
        tileSpawn += tileOffset; //add the offset to the tile spawn z location
    }

    //remove from the list and destroy the tile at the first index
    private void DespawnTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    //instantiate an object and increase the shardSpawn z location
    private void SpawnShard(int s) 
    {   
        GameObject go = Instantiate(shard, new Vector3(shardXLocations[s], 1.5f, shardSpawn), Quaternion.identity);
        
        shardSpawn += shardOffset;
        
    }


}
