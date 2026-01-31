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

    public void SetActiveScent(OdorAsset newScent)
    {
        activeScent = newScent;
    }

    public void Update()
    {
        olfactoryEpithelium.PlayOdor(activeScent, 1f);
    }


}
