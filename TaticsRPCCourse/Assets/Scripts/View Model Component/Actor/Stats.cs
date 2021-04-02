using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int this[StatTypes s]
    {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }
    int [] _data = new int[ (int)StatTypes.Count ];

    public static string WillChangeNotification (StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification (StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        return _didChangeNotifications[type];
    }

    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    public void SetValue (StatTypes type, int value, bool allowExceptions)
    {
        Debug.Log("Stats SetValue type: " + type + "\nvalue: " + value +
                    "\nallowExceptions: " + allowExceptions);

        int oldValue = this[type];
        Debug.Log("Stats SetValue oldValue: " + oldValue);
        if (oldValue == value)
            return;
        
        if (allowExceptions)
        {
            // Allow exceptions to the rule here
            ValueChangeException exc = new ValueChangeException( oldValue, value );
            // The notification is unique per stat type
            this.PostNotification(WillChangeNotification(type), exc);
            
            // Did anything modify the value?
            value = Mathf.FloorToInt(exc.GetModifiedValue());
            Debug.Log("Stats SetValue value: " + value + "\nexc.toogle: " + exc.toggle);
            // Did something nullify the change?
            if (exc.toggle == false || value == oldValue)
                return;
        }
        
        _data[(int)type] = value;
        Debug.Log("Stats SetValue _data[" + (int)type + "]: " + _data[(int)type]);
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}
