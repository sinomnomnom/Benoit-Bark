using OVR.API;
using OVR.Components;
using OVR.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

public class ScentManager
{
    AssemblyDefinitionAsset OVRPLugin;
    OlfactoryEpithelium olfactoryEpithelium;
    private OdorAsset activeScent;
    private ScentDatabase.Scents ActiveScentEnum = ScentDatabase.Scents.NONE;

    public ScentManager(AssemblyDefinitionAsset plugin, OlfactoryEpithelium nose)
    {
        OVRPLugin = plugin;
        olfactoryEpithelium = nose;
    }

    public void SetActiveScent(ScentDatabase.Scents scent)
    {
        SetBackground(scent);
        ActiveScentEnum = scent;
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
    

    public void SetBackground(ScentDatabase.Scents scent)
    {
        Sprite newSprite = Services.ScentDatabase.GetSpriteFromScent(scent);
        Sprite oldSprite = Services.ScentDatabase.GetSpriteFromScent(ActiveScentEnum);
        
        Services.GameController.BackgroundMaterial.SetTexture("_MainTex", oldSprite.texture);
        Services.GameController.BackgroundMaterial.SetTexture("_BlendTex", newSprite.texture);
        Services.GameController.BackgroundMaterial.SetFloat("_Blend", 0.0f);
        FadeTexture(Services.GameController.BackgroundMaterial, .5f);
    }


    async Task FadeTexture(Material mat, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            mat.SetFloat("_Blend", blend);

            await Task.Yield();
        }

        mat.SetFloat("_Blend", 1f);
    }
}
