using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected KeyCode homePageKey;
    [SerializeField] protected GameObject player1;
    [SerializeField] protected GameObject player2;
    [SerializeField] protected Vector2 positionStart;
    [SerializeField] protected Vector2 positionEnd;
    [SerializeField] protected bool[] gameWithSpawn;
    [SerializeField] protected float[] timerSpawn;
    [SerializeField] protected bool destroyAfterTimerSpawn;
    [SerializeField] protected GameObject[] spawnObj;
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource powerUpSound;
    [SerializeField] public AudioSource senzuBeanSound;
    public static GameManager inst;

    void Start()
    {

        inst=this;

        
        Vector2 v1 = getRandPos(positionStart, new Vector2(positionEnd.x / 2, positionEnd.y)); // Right side of the map
        Vector2 v2 = getRandPos(new Vector2(positionEnd.x / 2, positionStart.y), positionEnd); // Left side of the map

        player1.transform.position = new Vector3(v1.x,v1.y, player1.transform.position.z);
        player2.transform.position = new Vector3(v2.x, v2.y, player2.transform.position.z);
        for(int i=0;i< gameWithSpawn.Length;i++)
        if (gameWithSpawn[i])
        {
            spawnGameObj(i, timerSpawn[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(this.homePageKey))
        {
            SceneManager.LoadScene("MainScene");
        }

    }

    private Vector2 getRandPos(Vector2 start, Vector2 end)
    {
        return new Vector2(Random.Range(start.x, end.x), Random.Range(end.y, start.y));
    }

    private void spawnGameObj(int i, float timeRestart)
    {
        Vector2 randPos = getRandPos(this.positionStart, this.positionEnd);
        Vector3 positionOfSpawnedObject = new Vector3(randPos.x, randPos.y, -1);  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        GameObject newObject = Instantiate(spawnObj[i], positionOfSpawnedObject, rotationOfSpawnedObject);
        StartCoroutine(destroyAfterSec(timeRestart, newObject, i));
    }

    IEnumerator destroyAfterSec(float sec, GameObject obj, int i)
    {

        yield return new WaitForSeconds(sec);
        if(destroyAfterTimerSpawn) Destroy(obj);
        
        spawnGameObj(i, sec);
    }
}
