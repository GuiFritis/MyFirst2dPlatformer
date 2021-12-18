using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    
    public enum VFXType{
        JUMP
    }

    public List<VFXManagerSetup> vfxSetups;

    public void PlayVFXByType(VFXType vfxType, Vector3 pos){
        foreach(var setup in vfxSetups){
            if(setup.vfxType == vfxType){
                var item = Instantiate(setup.prefab);
                item.transform.position = pos;
                Destroy(item, 5f);
                break;
            }
        }
    }

}

[System.Serializable]
public class VFXManagerSetup{

    public VFXManager.VFXType vfxType;
    public GameObject prefab;

}
