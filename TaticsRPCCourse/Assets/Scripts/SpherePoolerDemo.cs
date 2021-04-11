using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpherePoolerDemo : MonoBehaviour
{
    [SerializeField] StringKeyedPooler pooler;
    [SerializeField] InputField keyInput;

    void OnEnable ()
    {
        pooler.didDequeueForKey = DidDequeueForKey;
    }
    void OnDisable ()
    {
        pooler.didDequeueForKey = null;
    }

    void DidDequeueForKey (Poolable item, string key)
    {
        float xPos = UnityEngine.Random.Range(-6f, 6f);
        float yPos = UnityEngine.Random.Range(-4f, 4f);
        float zPos = UnityEngine.Random.Range(-5f, 5f);
        item.transform.localPosition = new Vector3( xPos, yPos, zPos );
        item.gameObject.SetActive(true);
        item.name = key;
    }

    public void OnAddButton ()
    {
        if (!string.IsNullOrEmpty(keyInput.text))
            pooler.DequeueByKey(keyInput.text);
    }

    public void OnRemoveButton ()
    {
        pooler.EnqueueByKey(keyInput.text);
    }
}
