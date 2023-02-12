using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    #region Pub Vars
    public Transform[] tiles;
    public Transform[] spawnPoints;
    public GameObject enemies;
    #endregion

    #region Priv Vars
    int[] size = { 6, 8 };
    int[,] grid;
    int emptyCount;
    List<Vector2> emptyPositions=new List<Vector2>();
    List<Vector2> groundedPositions = new List<Vector2>();
    #endregion

    void Start () {
        SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeEmptyList()
    {
        emptyCount = (size[0] * size[1]) - tiles.Length;

        for(int i=0;i<size[0];i++)
        {
            for(int j=0;j<size[1];j++)
            {
                emptyPositions.Add(new Vector2(i, j));
            }
        }
    }
    void CreateEmptyMatrix()
    {
        grid = new int[size[0], size[1]];
        for(int i =0;i<size[0];i++)
        {
            for(int j =0;j<size[1];j++)
            {
                grid[i, j] = 0;
            }
        }
        foreach(Transform t in tiles)
        {
            grid[(int)t.localPosition.x/2, (int)t.localPosition.y/2] = 1;
        }               
    }
    void CreatEmptyList()
    {
        for(int i =0;i<tiles.Length;i++)
        {
            for (int j = 0; j < emptyPositions.Count; j++)
            {
                if (emptyPositions[j].x*2 ==tiles[i].localPosition.x && emptyPositions[j].y*2== tiles[i].localPosition.y)
                {
                    emptyPositions.RemoveAt(j);
                }
           }
        }
    }
    void CreateGroundedList()
    {
        foreach(Transform t in tiles)
        {
            if (grid[(int)t.localPosition.x/2, (int)t.localPosition.y/2]==1)
            {
                groundedPositions.Add(new Vector2(t.position.x, t.position.y + 2));
            }
        }
    }
    void SpawnEnemies()
    {
        foreach(Transform t in spawnPoints)
        {
            int rand = Random.Range(0, 100);
            if(rand>80)
                {
                Vector3 spawnPos = new Vector3((t.position.x), (t.position.y), 0);
                GameObject instEnemy = Instantiate(enemies, spawnPos, Quaternion.identity) as GameObject;
            }
        }
    }
}
