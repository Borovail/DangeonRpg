using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : Singleton<FloatingTextManager>
{
    public Transform UICanvas;
    public Transform WorldCanvas;

    public GameObject UIRelativeFloatingText;
    public GameObject WorldSpaceFloatingText;


    private List<FloatingText> floatingTextList = new();

   

    private void Update()
    {
        foreach (var item in floatingTextList)
        {
            item.UpdateFloatingText();
        }
    }


    public void Show(FloatingTextSettings floatingTextSettings)
    {
        floatingTextSettings.duration /= 2;
        var floatingText = GetFloatingText(floatingTextSettings.floatingTextType);
            
        floatingText.SetFloatingTextSettings(floatingTextSettings);

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
                txt.gameObject.transform.SetParent(UICanvas);
            }
            else
            {
                txt.gameObject = Instantiate(WorldSpaceFloatingText);
                txt.gameObject.transform.SetParent(WorldCanvas, false);
            }

            txt.floatingText = txt.gameObject.GetComponent<Text>();

            floatingTextList.Add(txt);
        }

        return txt;
    }
}




