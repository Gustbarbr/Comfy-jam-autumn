using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawnTimer : MonoBehaviour
{
    public GameObject client;

    private void Update()
    {
        if(!client.activeSelf)
            StartCoroutine(EnterShop());
    }

    IEnumerator EnterShop()
    {
        yield return new WaitForSeconds(5);
        client.SetActive(true);
    }
}
