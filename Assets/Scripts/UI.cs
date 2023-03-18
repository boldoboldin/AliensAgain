using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text ammoUI;
    private Weapon myWeapon;

    // Start is called before the first frame update
    void Start()
    {
        myWeapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoUI.text = myWeapon.bulletsInMagazine.ToString() + " / " + myWeapon.bulletsLeft.ToString();
    }
}
