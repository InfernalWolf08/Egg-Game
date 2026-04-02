using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Stat Additions")]
    public float speed=0;
    public float basketSizeX=0;
    public float basketSizeY=0;
    public float eggDropSpeed=0;
    public float eggSpawnRate=0;

    [Header("Attributes")]
    public Sprite sprite=null;
    public string name="Upgrade Name";
    public int price=0;

    public void Reset()
    {
        // Reset variables that start the same every time
        price=10;
    }
}