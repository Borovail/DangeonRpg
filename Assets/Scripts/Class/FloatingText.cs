using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public GameObject gameObject;
    public Text floatingText;

    public bool active;
    public float duration;
    public float lastShown;

    public Vector3 motion;

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

        gameObject.transform.position += motion * Time.unscaledDeltaTime;

    }

    public void SetFloatingTextSettings(FloatingTextSettings floatingTextSettings)
    {
        floatingText.text = floatingTextSettings.message;
        floatingText.fontSize = floatingTextSettings.fontSize;
        floatingText.color = floatingTextSettings.color;

        motion = floatingTextSettings.motion;
        duration = floatingTextSettings.duration;
        floatingTextType = floatingTextSettings.floatingTextType;


        if (floatingTextSettings.floatingTextType == FloatingTextType.UIRelativeFloatingText)
            gameObject.transform.position = Camera.main.WorldToScreenPoint(floatingTextSettings.position);
        else
            gameObject.transform.position = floatingTextSettings.position;
    }
}



public enum FloatingTextType
{
    UIRelativeFloatingText,
    WorldSpaceFloatingText
}