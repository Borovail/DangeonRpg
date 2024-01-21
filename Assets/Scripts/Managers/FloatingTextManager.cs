//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FloatingTextManager : MonoBehaviour
//{
//    public static FloatingTextManager instance;

//    public GameObject textContainer;
//    public GameObject textPrefab;

//    private List<FloatingText> floatingTextList = new List<FloatingText>();

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(instance);
//        }

//        instance = this;
//    }   

//    private void Update()
//    {
//        foreach (var item in floatingTextList)
//        {
//            item.UpdateFloatingText();
//        }
//    }


//    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
//    {
//        var floatingText = GetFloatingText();

//        floatingText.text.text = msg;
//        floatingText.text.fontSize = fontSize;
//        floatingText.text.color = color;
//        floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
//        floatingText.motion = motion;
//        floatingText.duration = duration;

//        floatingText.Show();
//    }

//    private FloatingText GetFloatingText()
//    {
//        var txt = floatingTextList.Find(t => !t.active);

//        if (txt == null)
//        {
//            txt = new FloatingText();
//            txt.gameObject = Instantiate(textPrefab);
//            txt.gameObject.transform.SetParent(textContainer.transform);
//            txt.text = txt.gameObject.GetComponent<Text>();

//            floatingTextList.Add(txt);
//        }

//        return txt;
//    }
//}
//////static text works
///





/// Dynamic text works


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;

    public Transform UICanvas;
    public Transform WorldCanvas;

    public GameObject UIRelativeFloatingText;
    public GameObject WorldSpaceFloatingText;


    private List<FloatingText> floatingTextList = new List<FloatingText>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    private void Update()
    {
        foreach (var item in floatingTextList)
        {
            item.UpdateFloatingText();
        }
    }


    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration, FloatingTextType floatingTextType)
    {
        var floatingText = GetFloatingText(floatingTextType);

        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        floatingText.floatingTextType = floatingTextType;
        floatingText.motion = motion;
        floatingText.duration = duration;

        if (floatingTextType == FloatingTextType.UIRelativeFloatingText)
        {
            floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
            floatingText.motion *= 40;
        }
        else
            floatingText.gameObject.transform.position = position;

        floatingText.Show();
    }

    private FloatingText GetFloatingText(FloatingTextType floatingTextType)
    {
        var txt = floatingTextList.Find(t => !t.active && t.floatingTextType == floatingTextType);

        if (txt == null)
        {
            txt = new FloatingText();

            if (floatingTextType == FloatingTextType.UIRelativeFloatingText)
            {
                txt.gameObject = Instantiate(UIRelativeFloatingText);
                txt.gameObject.transform.SetParent(UICanvas, false);
            }
            else
            {
                txt.gameObject = Instantiate(WorldSpaceFloatingText);
                txt.gameObject.transform.SetParent(WorldCanvas, false);
            }

            txt.text = txt.gameObject.GetComponent<Text>();

            floatingTextList.Add(txt);
        }

        return txt;
    }
}




