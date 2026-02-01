using OVR.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentDatabase : MonoBehaviour
{
    public OdorAsset[] scents;

    public Sprite Backbround;
    public Sprite Floral;
    public Sprite Smoky;

    public enum Scents { BARNYARRD, BEACH,
                         CITRUS, DESERT, EVERGREEN, FLORAL, KINDRED, 
                         MACHINA, MARINE, PETRICHOR, SAVORY, SMOKY, SWEET, TERA_SILVA, TIMBER, WINTER, NONE }

    
    public OdorAsset GetScent(Scents scent)
    {
        if (scent == Scents.NONE) {  return null; }
        return scents[(int)scent];
    }

    public Sprite GetSpriteFromScent(Scents scent)
    {
        switch (scent)
        {
            case Scents.NONE:
                return Backbround;
            case Scents.FLORAL:
                return Floral;
            case Scents.SMOKY:
                return Smoky;
            default:
                return Backbround;
        }
    }
}
