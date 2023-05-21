using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterName : MonoBehaviour
{
 
    [SerializeField]public TextMeshProUGUI NameText;
    public string Name;
    
    public void ChangeName()
    {
        NameText.text = Name.ToString();
    }

    
}
