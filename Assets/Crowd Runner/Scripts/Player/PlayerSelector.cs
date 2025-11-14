using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector playerSelectorPrefab;


    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
        }

        playerSelectorPrefab.SelectRunner(skinIndex);
    }
}
