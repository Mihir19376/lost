using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldController : MonoBehaviour
{
    float bobbingAmplitude = .05f;
    float bobbingFrequency = 1f;
    Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * bobbingFrequency) * bobbingAmplitude;
        transform.position = tempPos;
    }
}
