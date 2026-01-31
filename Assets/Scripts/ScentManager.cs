using OVR.API;
using OVR.Components;
using OVR.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditorInternal;
using UnityEngine;

public class ScentManager
{
    AssemblyDefinitionAsset OVRPLugin;
    OlfactoryEpithelium olfactoryEpithelium;
    private OdorAsset activeScent;

    public ScentManager(AssemblyDefinitionAsset plugin, OlfactoryEpithelium nose)
    {
        OVRPLugin = plugin;
        olfactoryEpithelium = nose;
    }

    public void SetActiveScent(ScentDatabase.Scents scent)
    {
        if (scent == ScentDatabase.Scents.NONE)
        {
            activeScent = null;
        }
        else
        {
            activeScent = Services.ScentDatabase.GetScent(scent);
        }
    }

    public void Update()
    {
        if (activeScent != null) olfactoryEpithelium.PlayOdor(activeScent, 1f);
    }
}
