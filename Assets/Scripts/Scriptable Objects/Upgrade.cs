using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Stat Additions")]
    public float speed;
    public float basketSizeX;
    public float basketSizeY;
    public float eggDropSpeed;
    public float eggSpawnRate;

    [Header("Attributes")]
    public Sprite sprite=null;
    public string name="Upgrade Name";
}