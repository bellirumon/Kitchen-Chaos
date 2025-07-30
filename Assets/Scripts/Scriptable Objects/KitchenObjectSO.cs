using UnityEngine;


[CreateAssetMenu(fileName = "DefaultKitchenObject", menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    [SerializeField] private Transform _prefab;
    public Transform Prefab => _prefab;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private string _objectName;
    public string ObjectName => _objectName;
}
