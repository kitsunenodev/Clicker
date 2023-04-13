using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Monstre : MonoBehaviour
{
    [SerializeField] int Life;
    
    public int _lifeMax;

    public Text TextLife;

    public Image ImageLife;

    public GameObject Visual;
    
    public Canvas Canvas;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Updatelife();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Updatelife()
    {
        TextLife.text = $"{Life}/{_lifeMax}";
        float percent = (float)Life / (float)_lifeMax;
        ImageLife.fillAmount = percent;
    }

    public void Hit(int damage)
    {
        if (damage < Life)
        {
            Life -= damage;
            Updatelife();
            Visual.transform.DOComplete();
            Visual.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);
        }
        else
        {
            Life = 0;
        }
        
    }

    public void SetMonster(MonstreInfos infos)
    {
        _lifeMax = infos.Life;
        Life = _lifeMax;
        Updatelife();
        Visual.GetComponent<SpriteRenderer>().sprite = infos.Sprite;
    }

    public bool IsAlive()
    {
        return Life > 0;
    }
}
