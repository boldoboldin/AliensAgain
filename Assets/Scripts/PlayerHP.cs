using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    
    private GameObject hpBar;
    public float hp;

    private GameObject o2Bar;
    public float o2;


    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("Canvas/UI/HPBar/HP");
        o2Bar = GameObject.Find("Canvas/UI/O2Bar/O2");
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var hpBarRectTransform = hpBar.transform as RectTransform;
        hpBarRectTransform.sizeDelta = new Vector2(hp, hpBarRectTransform.sizeDelta.y);

        var o2BarRectTransform = o2Bar.transform as RectTransform;
        o2BarRectTransform.sizeDelta = new Vector2(o2, o2BarRectTransform.sizeDelta.y);

        o2 = o2 - Time.deltaTime;

        if (o2 <= 0)
        {
            hp = hp - Time.deltaTime * 2;
        }

        if (o2 > 150) // Gambiarra
        {
            o2 = 150;
        }
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // Add tela de game over
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "O2")
        {
            o2 = o2 + 25;
        }

        if (other.gameObject.tag == "Ammo")
        {
            weapon.CollectAmmo(24);
        }
    }
}
// RectTransform objectRectTransform = theBar.GetComponent<RectTransform> 