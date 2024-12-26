using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class usicontroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI slideerText=null;
    [SerializeField] private float maxSliderAmount=10.0f;
    public void SliderChange(float value){
   float localValue=value*maxSliderAmount;
    slideerText.text=localValue.ToString("0");
    }
}
