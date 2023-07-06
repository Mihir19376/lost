using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Animator axeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitAndDissolve());
        Debug.Log(axeAnimator.GetCurrentAnimatorStateInfo(0).length + axeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    IEnumerator WaitAndDissolve()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Mosjvbhfr");
        Destroy(gameObject);
    }
}
