using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] List<ScriptableObject> items;
    // axe id = 0 , car = 1 , chair  = 2 ,tree = 3
    public List<ScriptableObject> getAvailableResources() {
        return items;
    }
}
