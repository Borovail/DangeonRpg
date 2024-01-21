//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FloatingText
//{
//    public bool active;
//    public GameObject gameObject;
//    public Text text;
//    public Vector3 motion;
//    public float duration;
//    public float lastShown;

//    public void Show()
//    {
//        active = true;
//        lastShown = Time.time;
//        gameObject.SetActive(true);

//    }

//    public void Hide()
//    {
//        active = false;
//        gameObject.SetActive(false);

//    }

//    public void UpdateFloatingText()
//    {
//        if (!active) return;

//        if (Time.time - lastShown > duration)
//            Hide();

//        gameObject.transform.position += motion * Time.deltaTime;

//    }
//}
//// Dynamic text works







/////static text works

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gameObject;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;
    public FloatingTextType floatingTextType;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        active = false;
        gameObject.SetActive(false);

    }

    public void UpdateFloatingText()
    {
        if (!active) return;

        if (Time.time - lastShown > duration)
            Hide();

        gameObject.transform.position += motion * Time.deltaTime;

    }
}



public enum FloatingTextType
{
    UIRelativeFloatingText,
    WorldSpaceFloatingText
}