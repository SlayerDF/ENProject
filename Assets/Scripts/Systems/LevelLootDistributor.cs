using System.Linq;
using UnityEngine;

public class LevelLootDistributor : MonoBehaviour
{
    [SerializeField]
    DestructibleObject[] lootDroppers;

    [SerializeField]
    GameObject[] lootToDistribute;

    void Awake()
    {
        var droppers = lootDroppers.OrderBy(x => Random.value).Take(lootToDistribute.Length).ToList();

        for (int i = 0; i < lootToDistribute.Length; i++)
        {
            var dropSystem = droppers[i].gameObject.GetComponent<DropLootSystem>();

            dropSystem.AddItem(lootToDistribute[i], 1);
        }
    }
}
