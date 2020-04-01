using UnityEngine;

public class FactionSelectionScript : MonoBehaviour
{
    public GunEquipper equipperScript;

    public void XImperiumSelected()
    {
        //faction #1
        equipperScript.fSel1status = true;
        equipperScript.fSel2status = false;
        equipperScript.fSel3status = false;
        equipperScript.fSel4status = false;
        equipperScript.fSel5status = false;
        equipperScript.fSel6status = false;
    }

    public void FusionCoSelected()
    {
        //faction #2
        equipperScript.fSel2status = true;
        equipperScript.fSel1status = false;
        equipperScript.fSel3status = false;
        equipperScript.fSel4status = false;
        equipperScript.fSel5status = false;
        equipperScript.fSel6status = false;
    }
    public void ShadaCollectiveSelected()
    {
        //faction #3
        equipperScript.fSel3status = true;
        equipperScript.fSel1status = false;
        equipperScript.fSel2status = false;
        equipperScript.fSel4status = false;
        equipperScript.fSel5status = false;
        equipperScript.fSel6status = false;
    }
    public void ZeroGSelected()
    {
        //faction #4
        equipperScript.fSel4status = true;
        equipperScript.fSel1status = false;
        equipperScript.fSel2status = false;
        equipperScript.fSel3status = false;
        equipperScript.fSel5status = false;
        equipperScript.fSel6status = false;
    }
    public void RenovoCoreSelected()
    {
        //faction #5
        equipperScript.fSel5status = true;
        equipperScript.fSel1status = false;
        equipperScript.fSel2status = false;
        equipperScript.fSel3status = false;
        equipperScript.fSel4status = false;
        equipperScript.fSel6status = false;
    }
    public void GoldenSunSelected()
    {
        //faction #6
        equipperScript.fSel6status = true;
        equipperScript.fSel1status = false;
        equipperScript.fSel2status = false;
        equipperScript.fSel3status = false;
        equipperScript.fSel4status = false;
        equipperScript.fSel5status = false;
    }
}
