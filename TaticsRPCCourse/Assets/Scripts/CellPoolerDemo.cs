using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPoolerDemo : MonoBehaviour
{
    [SerializeField] IndexedPooler pooler;
    [SerializeField] RectTransform content;

    void OnEnable ()
    {
        pooler.didDequeueAtIndex = DidDequeueAtIndex;
        pooler.willEnqueueAtIndex = WillEnqueueAtIndex;
    }

    void OnDisable ()
    {
        pooler.didDequeueAtIndex = null;
        pooler.willEnqueueAtIndex = null;
    }

    void DidDequeueAtIndex (Poolable item, int index)
    {
        Button button = item.GetComponent<Button>();
        button.transform.SetParent(content, false);
        button.gameObject.SetActive(true);
        button.onClick.AddListener( () => {
            Colorize(button.GetComponent<Image>());
        } );
    }

    void WillEnqueueAtIndex (Poolable item, int index)
    {
        Button button = item.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void OnColorizeButton ()
    {
        for (int i = 0; i < pooler.Collection.Count; ++i)
            Colorize( pooler.GetScript<Image>(i) );
    }

    void Colorize (Image image)
    {
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;
        image.color = new Color(r, g, b);
    }
}
