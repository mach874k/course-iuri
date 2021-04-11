using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePoolerDemo : MonoBehaviour
{
    [SerializeField] SetPooler pooler; 

    void OnEnable ()
    {
        pooler.willEnqueue = OnEnqueue;
        pooler.didDequeue = OnDequeue;
    }
    void OnDisable ()
    {
        pooler.willEnqueue = null;
        pooler.didDequeue = null;
    }

    void OnDequeue (Poolable item)
    {
        float xPos = UnityEngine.Random.Range(-6f, 6f);
        float yPos = UnityEngine.Random.Range(-4f, 4f);
        float zPos = UnityEngine.Random.Range(-5f, 5f);
        item.transform.localPosition = new Vector3( xPos, yPos, zPos );
        item.gameObject.SetActive(true);
        Button button = item.GetComponent<Button>();
        button.onClick.AddListener( ()=>{ pooler.Enqueue(item); } );
    }

    void OnEnqueue (Poolable item)
    {
        Button button = item.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void OnAddButton ()
    {
        pooler.Dequeue();
    }
}
