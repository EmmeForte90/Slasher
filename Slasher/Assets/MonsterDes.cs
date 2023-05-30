using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterDes : MonoBehaviour
{
   [SerializeField]public TextMeshProUGUI NameText;
   [SerializeField]public TextMeshProUGUI DesText;

    public string Name;
    [TextAreaAttribute]
    public string Des;

    
    public void ChangeName()
    {
        NameText.text = Name.ToString();
        DesText.text = Des.ToString();
    }
}
