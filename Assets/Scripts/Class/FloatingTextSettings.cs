using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FloatingTextSettings
{
    public string message;
    public float duration;
    public int fontSize;

    public Vector3 motion;
    public Vector3 position;

    public Color color;
    public FloatingTextType floatingTextType;

    public FloatingTextSettings(string message, float duration, int fontSize, Color color,  Vector3 motion,   Vector3 position ,FloatingTextType floatingTextType )
    {
        this.message = message;
        this.duration = duration;
        this.fontSize = fontSize;
        this.color = color;
        this.motion = motion;
        this.position = position;
        this.floatingTextType = floatingTextType;
    }

}

