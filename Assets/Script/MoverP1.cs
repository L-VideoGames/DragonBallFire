using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverP1 : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected string horizontalName;
    [SerializeField] protected string verticalName;
    [SerializeField] protected SpriteRenderer normalSpriteRenderer;
    [SerializeField] protected SpriteRenderer powerUpSpriteRenderer;
    [SerializeField] protected string weakFireName;
    [SerializeField] protected KeyCode fireKeyR;
    [SerializeField] protected KeyCode fireKeyL;
    [SerializeField] protected KeyCode PowerKey;
    [SerializeField] protected GameObject fireObj;
    [SerializeField] protected GameObject powerFireObj;
    [SerializeField] protected Transform greenH;
    [SerializeField] protected float deleyShootTime;
    [SerializeField] protected float deleyPowerShootTime;

    private bool[] canShoot = {true,true}; // [0]=Left, [1]=Right
    private float life100; // How much is 100 percent life
    private bool isPowerUp; // 

    void Start()
    {
        life100 = this.greenH.transform.localScale.x;
        isPowerUp = false;
    }

    void Update()
    {
        float x = Input.GetAxis(horizontalName); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        float y = Input.GetAxis(verticalName);     // +1 if up arrow is pushed, -1 if down arrow is pushed, 0 otherwise
        if (isPowerUp)
        {
            if (x < 0) { this.powerUpSpriteRenderer.flipX = true; }
            if (x > 0) { this.powerUpSpriteRenderer.flipX = false; }
            Vector3 movementVector = new Vector3(x, y, 0) * (speed+2) * Time.deltaTime;
            transform.position += movementVector;

            if (Input.GetKeyDown(this.PowerKey))
            {
                if (this.powerUpSpriteRenderer.flipX)
                {
                    StartCoroutine(shootPowerEnergy(spawnPowerObject(-1)));
                }
                else
                {
                    StartCoroutine(shootPowerEnergy(spawnPowerObject(1)));
                }
                

            }
            if (Input.GetKeyDown(this.fireKeyR) && canShoot[1])
            {
                StartCoroutine(afterShoot(1, spawnObject(1), this.deleyShootTime-1));
            }
            if (Input.GetKeyDown(this.fireKeyL) && canShoot[0])
            {
                StartCoroutine(afterShoot(0, spawnObject(-1), this.deleyShootTime-1));
            }
        }
        else
        {
            if (x < 0){ this.normalSpriteRenderer.flipX = true;}
            if (x > 0){ this.normalSpriteRenderer.flipX = false;}
            Vector3 movementVector = new Vector3(x, y, 0) * speed * Time.deltaTime;
            transform.position += movementVector;
            if (Input.GetKeyDown(this.fireKeyR) && canShoot[1])
            {
                StartCoroutine(afterShoot(1, spawnObject(1), this.deleyShootTime));
            }
            if (Input.GetKeyDown(this.fireKeyL) && canShoot[0])
            {
                StartCoroutine(afterShoot(0, spawnObject(-1), this.deleyShootTime));
            //spawnObject(-1);
            }
        }



    }

    // Delay between shots
    IEnumerator afterShoot(int shootType, GameObject fire, float timeDeley) {

        canShoot[shootType] = false;
        yield return new WaitForSeconds(timeDeley);
        canShoot[shootType] = true;
        Destroy(fire);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == weakFireName) // Fire enter
        {

            GameManager.inst.hitSound.Play();
            Destroy(other.gameObject);
            getBeat(0.1f);
            if (this.greenH.localScale.x <= 0.001)
            {
                string winScene = this.tag == "Vegeta" ? "GokuScene" : "VegetaScene";
                SceneManager.LoadScene(winScene);
            }
        }
        else if (other.tag == "Power"+weakFireName) // Power fire enter
        {

            GameManager.inst.hitSound.Play();
            Destroy(other.gameObject);
            getBeat(0.2f);
            if (this.greenH.localScale.x <= 0.001)
            {
                string winScene = this.tag == "Vegeta" ? "GokuScene" : "VegetaScene";
                SceneManager.LoadScene(winScene);
            }
        }
        else if (other.tag == "Heal") // Heal enter
        {
            GameManager.inst.senzuBeanSound.Play();
            Destroy(other.gameObject);
            
            if (this.greenH.localScale.x < life100)
            {
                getBeat(-0.1f);
            }
        }
        else if (other.tag == "PowerUp") // Power Up enter
        {
            GameManager.inst.powerUpSound.Play();
            Destroy(other.gameObject);
            StartCoroutine(afterPowerUp(10f));

        }
    }

    // Set power up on the player
    IEnumerator afterPowerUp(float timerPowerUp)
    {
        if (!isPowerUp)
        {
            this.normalSpriteRenderer.enabled = false;
            this.powerUpSpriteRenderer.enabled = true;
            isPowerUp = true;
            yield return new WaitForSeconds(timerPowerUp);
            isPowerUp = false;
            this.normalSpriteRenderer.enabled = true;
            this.powerUpSpriteRenderer.enabled = false;
        }

    }

    // Takes life
    private void getBeat(float powerStrik)
    {
        this.greenH.transform.localScale -= new Vector3(powerStrik, 0, 0);
        this.greenH.transform.localPosition -= new Vector3(powerStrik*0.5f, 0, 0);
    }

    // Places a POWER shoot in the required direction
    protected virtual GameObject spawnPowerObject(int direction)
    {
        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.

        GameObject newObject = Instantiate(powerFireObj, positionOfSpawnedObject, rotationOfSpawnedObject);
        Vector3 vel = new Vector3(direction * 5, 0, 0);

        // Step 2: modify the velocity of the new object.
        GenkidamaAction newObjectMover = newObject.GetComponent<GenkidamaAction>();
        if (newObjectMover)
        {
            newObjectMover.SetDirection(direction);
        }

        return newObject;
    }

    IEnumerator shootPowerEnergy(GameObject fire)
    {
        isPowerUp = false;
        this.normalSpriteRenderer.enabled = true;
        this.powerUpSpriteRenderer.enabled = false;
        yield return new WaitForSeconds(deleyPowerShootTime);     
        Destroy(fire);
    }

    // Places a shoot in the required direction
    protected virtual GameObject spawnObject(int direction)
    {
        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        if (direction < 0)
        {
            fireObj.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (direction > 0)
        {
            fireObj.GetComponent<SpriteRenderer>().flipX = false;
        }
        GameObject newObject = Instantiate(fireObj, positionOfSpawnedObject, rotationOfSpawnedObject);
        Vector3 vel = new Vector3(direction * 5, 0, 0);

        // Step 2: modify the velocity of the new object.
        ShootMover newObjectMover = newObject.GetComponent<ShootMover>();
        if (newObjectMover)
        {
            newObjectMover.SetVelocity(vel);
        }

        return newObject;
    }

}
