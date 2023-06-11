using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    public List<string> InspectionText;
    public bool InitiateFight = false;
    public GameObject enemy;

    Inspectable(List<string> inspectionText)
    {
        foreach (var inspection in inspectionText)
        {
            InspectionText.Add(inspection.ToString());
        }
    }

    public Inspectable copy()
    {
        Inspectable copy = new Inspectable(InspectionText);
        return copy;
    }
}
