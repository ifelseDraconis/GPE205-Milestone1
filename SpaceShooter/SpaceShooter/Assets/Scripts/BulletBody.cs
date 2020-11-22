using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBody : MonoBehaviour
{
    private Transform thisBulletTran;
    private GameManager thisManager;
    private float thisBulletSpeed;
    private float thisBulletLifespan;
    private Collider warHead;

    // Start is called before the first frame update
    void Start()
    {
        thisBulletTran = GetComponent<Transform>();
        thisBulletTran.Translate(Vector3.forward * 10.0f);

        thisManager = GameManager.thisManager;
        thisBulletSpeed = thisManager.BulletSpeed;
        thisBulletLifespan = thisManager.BulletDuration;

        warHead = GetComponent<Collider>();

        StartCoroutine(byebyeWorld());
        StartCoroutine(sailAway());
    }

    // Update is called once per frame
    void Update()
    {
        // this makes the bullet move
        thisBulletTran.Translate(Vector3.forward * Time.deltaTime * thisBulletSpeed);
    }

    // this just sends a message that the bullet is dead
    void OnDestroy()
    {

    }

    IEnumerator byebyeWorld()
    {
        yield return new WaitForSeconds(thisBulletLifespan);
        Destroy(gameObject);
    }

    IEnumerator sailAway()
    {
        yield return new WaitForSeconds(0.5f);
        warHead.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
