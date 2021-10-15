using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneClick : MonoBehaviour
{
    int runeIndex;
    public GameObject[] runes = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        runeIndex = 0;
        Instantiate(runes[runeIndex], new Vector3(-5.73f, -0.43f, 0), Quaternion.identity);
        Instantiate(runes[runeIndex], new Vector3(-3.69f, -0.43f, 0), Quaternion.identity);
        Instantiate(runes[runeIndex], new Vector3(-1.77f, -0.43f, 0), Quaternion.identity);
        Instantiate(runes[runeIndex], new Vector3(0.09f, -0.43f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
