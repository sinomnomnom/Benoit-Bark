using OVR.API;
using OVR.Components;
using OVR.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
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

    public ScentManager() { }

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
        //if (activeScent != null) olfactoryEpithelium.PlayOdor(activeScent, 1f);
    }


    private CancellationTokenSource fadeCTS;

    public void SetBackground(ScentDatabase.Scents scent)
    {
       
        fadeCTS?.Cancel();
        fadeCTS = new CancellationTokenSource();

        Sprite newSprite = Services.ScentDatabase.GetSpriteFromScent(scent);
        Sprite oldSprite = Services.ScentDatabase.GetSpriteFromScent(ActiveScentEnum);

       
        Material mat = Services.GameController.BackgroundMaterial;

        mat.SetTexture("_MainTex", oldSprite.texture);
        mat.SetTexture("_BlendTex", newSprite.texture);
        mat.SetFloat("_Blend", 0.0f);
        FadeTexture(mat, 0.5f, fadeCTS.Token);
    }

    async Task FadeTexture(Material mat, float duration, CancellationToken ct)
    {
        float t = 0f;
        while (t < duration)
        {
            if (ct.IsCancellationRequested) return;

            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            mat.SetFloat("_Blend", blend);
            await Task.Yield();
        }

        if (!ct.IsCancellationRequested)
        {
            mat.SetFloat("_Blend", 1f);
            mat.SetTexture("_MainTex", mat.GetTexture("_BlendTex"));
        }
    }
}
