using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviourSingleton<PoolController> 
{
    [Header("Keybinds")]
    [SerializeField]
    private KeyCode testCreateBind;
    [SerializeField]
    private KeyCode testDestroyBind;
    [SerializeField]
    private KeyCode testDestroyMiddleBind;

    private void Update()
    {
        if (Input.GetKeyDown(testCreateBind))
        {
            Vector3 tf = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
            Vector3 sc = new Vector3(1, 1, 1);
            PoolManager.Instance.CreateObject(0, tf, tf, sc);
        }

        if (Input.GetKeyDown(testDestroyBind))
        {
            PoolManager.Instance.DestroyObject(0);
        }

        if (Input.GetKeyDown(testDestroyMiddleBind))
        {
            PoolManager.Instance.DestroyObject(0, Random.Range(0, 19));
        }
    }
}
