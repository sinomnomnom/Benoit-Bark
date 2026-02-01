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
    public Sprite Timber;
    public Sprite Musty;
    public Sprite Mechanica;
    public Sprite Winter;
    public Sprite Sweet;
    public Sprite Kindred;

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
            case Scents.TIMBER:
                return Timber;
            case Scents.BARNYARRD:
                return Musty;
            case Scents.MACHINA:
                return Mechanica;
            case Scents.WINTER:
                return Winter;
            case Scents.SWEET:
                return Sweet;
            case Scents.KINDRED:
                return Kindred;
            default:
                return Backbround;
        }
    }
}
