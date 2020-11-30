using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOn : MonoBehaviour
{
    private Transform thisBlast;
    private Vector3 thisIncrease, currentIncrease;

    // Start is called before the first frame update
    void Start()
    {
        thisBlast = GetComponent<Transform>();
        thisIncrease = new Vector3(3.25f, 3.25f, 3.25f);
        StartCoroutine(byebyeWorld());
    }

    // Update is called once per frame
    void Update()
    {
        currentIncrease = thisIncrease * Time.deltaTime;
        thisBlast.localScale += currentIncrease;
    }

    IEnumerator byebyeWorld()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }
}
