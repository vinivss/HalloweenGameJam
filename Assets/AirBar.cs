using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirBar : MonoBehaviour
{
    public Slider slider;

    public void SetBreath(float breath)
    {
        slider.value = breath;
    }
}
