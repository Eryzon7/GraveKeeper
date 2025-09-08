using System.Collections.Generic;
using UnityEngine;

public class AggroSystem : MonoBehaviour
{
    private Dictionary<GameObject, float> threatTable = new Dictionary<GameObject, float>();

    void Update()
    {   
        List<GameObject> keys = new List<GameObject>(threatTable.Keys);
        foreach (var player in keys)
        {
            threatTable[player] = Mathf.Max(0, threatTable[player] - Time.deltaTime * .5f); // 2 threat per second decay
        }
        
    }

    public void AddThreat(GameObject player, float amount)
    {
        if (!threatTable.ContainsKey(player))
        {
            threatTable[player] = 0;
        }
        threatTable[player] += amount;
    }

    public GameObject GetHighestThreatTarget()
    {
        GameObject target = null;
        float highestThreat = Mathf.NegativeInfinity;

        foreach (var kvp in threatTable)
        {
            if (kvp.Value > highestThreat)
            {
                highestThreat = kvp.Value;
                target = kvp.Key;
            }
        }
        return target;
    }

    public void ResetThreat(GameObject player)
    {
        if (threatTable.ContainsKey(player))
        {
            threatTable[player] = 0; 
        }
    }

    public void ClearAllThreat()
    {
        threatTable.Clear();
    }
}