using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Magic {
    [SerializeField]
    public ScriptableMagic scriptableMagic;
    [SerializeField]
    public int level = 1;
}

public class LaunchingMagicManager : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Magic firstMagic;
    [SerializeField] Magic secondMagic;
    [SerializeField] AccessoryClass accessory;
    [SerializeField] GameObject firstMagicIcon;
    [SerializeField] GameObject firstMagicLevel;
    [SerializeField] GameObject secondMagicIcon;
    [SerializeField] GameObject secondMagicLevel;
    [SerializeField] GameObject accessoryIcon;
    [SerializeField] GameObject playerElementIcon;
    Vector2 position = Vector2.zero;
    bool onFirstMagicCooldown = false;
    bool onSecondMagicCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo.Instance().UpdatePlayerGlobalMagic(ref firstMagic, ref secondMagic, ref accessory);
        UpdateMagicIcon();
        playerElementIcon.GetComponent<Image>().sprite = GlobalGameVar.Instance().elementDic[PlayerInfo.Instance().element].sprite;
        Debug.Log(playerElementIcon.GetComponent<Image>().sprite);
    }

    // Update is called once per frame
    void Update()
    {
        position = PlayerInfo.Instance().GetPlayerPos();
    }
    public void Launch(Vector2 direction, Magic magic) {
        switch(magic.scriptableMagic.shape) {
            case MagicShape.Line:
                StartCoroutine(ShootInLine(direction, magic));
                break;
            case MagicShape.Fan:
                ShootInFanShape(direction, magic);
                break;
        }
    }
    IEnumerator ShootInLine(Vector2 direction, Magic magic) {
        for (int i = 0; i < magic.scriptableMagic.number; i++) {
            ShootOneProjectile(direction, magic);
            yield return new WaitForSeconds(0.15f);
        }
    }
    void ShootInFanShape(Vector2 direction, Magic magic) {
        for (int i = 0; i < magic.scriptableMagic.number; i++) {
            float angle = -45f + (i + 1f) * 90f / (magic.scriptableMagic.number + 1f);
            angle = Vector2.SignedAngle(Vector2.right, direction) + angle;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            ShootOneProjectile(dir, magic);
        }
    }
    void ShootOneProjectile(Vector2 direction, Magic magic) {
        float dmgModifier = 1f;
        if (accessory != null) {
            switch(magic.scriptableMagic.element) {
                case Element.Metal:
                    dmgModifier = 1 + accessory.metalBuff;
                    break;
                case Element.Water:
                    dmgModifier = 1 + accessory.waterBuff;
                    break;
                case Element.Wood:
                    dmgModifier = 1 + accessory.woodBuff;
                    break;
                case Element.Fire:
                    dmgModifier = 1 + accessory.fireBuff;
                    break;
                case Element.Earth:
                    dmgModifier = 1 + accessory.earthBuff;
                    break;
                default:
                    break;
            }
        }
        
        GameObject launch = Instantiate(projectile, position + Vector2.up * 0.75f, Quaternion.Euler(0,0, Vector2.SignedAngle((direction.x >= 0) ? Vector2.right : Vector2.left, direction)));
        if(direction.x < 0) {
            launch.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(magic.scriptableMagic.sprite != null) {
            launch.GetComponent<SpriteRenderer>().sprite = magic.scriptableMagic.sprite;
        }
        launch.transform.localScale = new Vector3(magic.scriptableMagic.size, magic.scriptableMagic.size, 0);
        float damage = (int) Math.Ceiling(magic.scriptableMagic.damage * Math.Pow(1.25f ,magic.level - 1));
        launch.GetComponent<ProjectileManager>().Launch(direction, 
        magic.scriptableMagic.speed, magic.scriptableMagic.element, damage * dmgModifier, 
        magic.scriptableMagic.lifeTime, magic.scriptableMagic.explodeOnContact, 
        magic.scriptableMagic.statusEffect, magic.level);
    }
    public void LaunchFirstMagic(Vector2 direction) {
        if (!onFirstMagicCooldown && firstMagic.scriptableMagic != null) {
            Launch(direction, firstMagic);
            onFirstMagicCooldown = true;
            Invoke("OutOfFirstMagicCooldown", firstMagic.scriptableMagic.cooldown);
        }
    }
    void OutOfFirstMagicCooldown() {
        onFirstMagicCooldown = false;
    }
    public void LaunchSecondMagic(Vector2 direction) {
        if (!onSecondMagicCooldown && secondMagic.scriptableMagic != null) {
            Launch(direction, secondMagic);
            onSecondMagicCooldown = true;
            Invoke("OutOfSecondMagicCooldown", secondMagic.scriptableMagic.cooldown);
        }
    }
    void OutOfSecondMagicCooldown() {
        onSecondMagicCooldown = false;
    }
    public void ChangeFirstMagic(Magic magic) {
        firstMagic = magic;
        UpdateMagicIcon();
    }
    public void ChangeSecondMagic(Magic magic) {
        secondMagic = magic;
        UpdateMagicIcon();
    }
    public void ChangeAccessory(AccessoryClass acc) {
        accessory = acc;
        UpdateMagicIcon();
    }
    public AccessoryClass GetAccessory() {
        return accessory;
    }
    void UpdateMagicIcon() {
        if (firstMagic.scriptableMagic != null) {
            firstMagicIcon.SetActive(true);
            firstMagicIcon.GetComponent<Image>().sprite = GlobalGameVar.Instance().elementDic[firstMagic.scriptableMagic.element].sprite;
            firstMagicLevel.GetComponent<TextMeshProUGUI>().text = firstMagic.level.ToString();
        } else {
            firstMagicIcon.SetActive(false);
            firstMagicLevel.GetComponent<TextMeshProUGUI>().text = "0";
        }

        if (secondMagic.scriptableMagic != null) {
            secondMagicIcon.SetActive(true);
            secondMagicIcon.GetComponent<Image>().sprite = GlobalGameVar.Instance().elementDic[secondMagic.scriptableMagic.element].sprite;
            secondMagicLevel.GetComponent<TextMeshProUGUI>().text = secondMagic.level.ToString();
        } else {
            secondMagicIcon.SetActive(false);
            secondMagicLevel.GetComponent<TextMeshProUGUI>().text = "0";
        }

        if (accessory != null) {
            accessoryIcon.SetActive(true);
            accessoryIcon.GetComponent<Image>().sprite = accessory.itemIcon;
        } else {
            accessoryIcon.SetActive(false);
        }
    }
}
