using TMPro;
using UnityEngine;


public class UiController : MonoBehaviour
{

    public TextMeshProUGUI mancheText;


    public static int manche = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mancheText.text = "Manche : " + manche;
    }
}
