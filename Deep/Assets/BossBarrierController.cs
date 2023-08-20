using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrierController : MonoBehaviour
{
    public GameObject warningMessage;
    private bool warningWait;

    // Start is called before the first frame update
    void Start()
    {
        warningWait = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerTag")
        {
            if (collision.gameObject.GetComponent<PlayerMovement2D>().gems == 5)
            {
                Destroy(gameObject);
            }
            else
            {
                warningWait = false;
                StartCoroutine(showMessage());
            }
            
        }
    }

    IEnumerator showMessage()
    {
        if (!warningWait)
        {
            warningMessage.SetActive(true);
            yield return new WaitForSeconds(1);
            warningMessage.SetActive(false);
            warningWait = true;
        }
    }
}
