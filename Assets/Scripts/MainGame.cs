using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public List<MonstreInfos> Monstres;

    public int _currentMonster;
    
    public Monstre Monster;
    
    public GameObject PrefabHitPoint;

    public List<Upgrade> Upgrades;

    public GameObject PrefabUpgradeUI;

    public GameObject ParentUpgrades;
    
    public List<Upgrade> _UnlockedUpgrades;
    
    public float _timerAutoDamage;

    public static MainGame Instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int element = 0;
        Monster.SetMonster(Monstres[_currentMonster]);
        foreach (var upgrade in Upgrades)
        {
            
            GameObject go = GameObject.Instantiate(PrefabUpgradeUI, ParentUpgrades.transform, false);
            go.transform.localPosition = Vector3.down * (element * 600);
            go.GetComponent<UpgradeUI>().Initialize(upgrade);
            element++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            if (hit.collider != null)
            {
                Monstre monster = hit.collider.gameObject.GetComponent<Monstre>();
                Hit(1, monster);
            }
        }
        _timerAutoDamage += Time.deltaTime;

        if (_timerAutoDamage > 1.0f)
        {
            
            foreach (var upgrade in _UnlockedUpgrades)
            {
                Hit(upgrade.DPS, Monster);
            }
            _timerAutoDamage = 0;
        }
        
    }

    private void NextMonster()
    {
        _currentMonster++;
        if (_currentMonster >= Monstres.Count)
        {
            _currentMonster = Monstres.Count - 1;
        }
        Monster.SetMonster(Monstres[_currentMonster]);
        
    }

    void Hit(int damage, Monstre monster)
    {
        monster.Hit(damage);
        GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.Canvas.transform, false);
        go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 100;
        go.GetComponent<Text>().text = $"{damage}";
        go.transform.DOLocalMoveY(150, 0.8f);
        go.GetComponent<Text>().DOFade(0, 0.8f);
        GameObject.Destroy(go, 0.8f);
        if (monster.IsAlive() ==  false)
        {
            NextMonster();
        }
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        _UnlockedUpgrades.Add(upgrade);
        
        
    }
}
