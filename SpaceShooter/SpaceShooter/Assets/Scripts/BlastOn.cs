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
        thisIncrease = new Vector3(6.0f, 6.0f, 6.0f);
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
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
