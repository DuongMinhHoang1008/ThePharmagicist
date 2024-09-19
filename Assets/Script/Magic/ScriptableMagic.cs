using System.Collections;
using System.Collections.Generic;
using MetaMask.Editor.NaughtyAttributes;
using UnityEngine;

public enum MagicShape {
    Line,
    Fan
}

[CreateAssetMenu(fileName = "New Magic", menuName ="Magic/New Magic")]
public class ScriptableMagic : ScriptableObject
{
    [SerializeField] public string magicName;
    [SerializeField] public string description;
    [SerializeField] public Element element;
    [SerializeField] public float damage;
    [SerializeField] public float cooldown;
    [SerializeField] public float speed;
    [SerializeField] public float lifeTime;
    [SerializeField] public int number;
    [SerializeField] public MagicShape shape;
    [SerializeField] public float size = 1f;
    [SerializeField] public bool explodeOnContact;
    [SerializeField] public StatusEffect statusEffect;
    [SerializeField] public Sprite sprite;
}
