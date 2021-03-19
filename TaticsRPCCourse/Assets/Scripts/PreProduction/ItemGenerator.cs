using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] TextAsset weaponNames;
    [SerializeField] Sprite[] icons;
    string[] lines;

    void Start()
    {
        lines = weaponNames.text.Split(new string[] { "\r\n", "\n"}, StringSplitOptions.None);
        List<Item> items = new List<Item>(icons.Length);
        for(int i=0; i < icons.Length; ++i)
            items.Add(Create(icons[i]));
        GetComponent<ItemShop>().Load(items);
    }

    Item Create(Sprite icon)
    {
        Item retValue = new Item();
        retValue.sprite = icon;
        retValue.name = RandomName();
        retValue.attack = UnityEngine.Random.Range(0, 100);
        retValue.level = retValue.attack / 10;
        retValue.price = 50 * (retValue.level + UnityEngine.Random.Range(0, 5)) + 100;
        return retValue;
    }

    string RandomName()
    {
        string s1 = lines[UnityEngine.Random.Range(0, lines.Length)];
        string s2 = lines[UnityEngine.Random.Range(0, lines.Length)];
        if(UnityEngine.Random.Range(0, 2) == 0)
            return string.Format("{0} of {1}", s1, s2);
        return string.Format("{0} {1}", s1, s2);
    }
}
