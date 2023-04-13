using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Image Image;

    public Text Texte;

    public Text TextCost;

    public Upgrade _upgrade;

    public GameObject bouton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize(Upgrade upgrade)
    {
        _upgrade = upgrade;
        Image.sprite = upgrade.Sprite;
        Texte.text = upgrade.Name + System.Environment.NewLine + upgrade.Description;
        TextCost.text = upgrade.Cost + "$";
    }

    public void OnClick()
    {
        MainGame.Instance.AddUpgrade(_upgrade);
        bouton.SetActive(false);
    }
}
