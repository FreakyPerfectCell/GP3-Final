using UnityEngine;

public class AutoRepair : MonoBehaviour
{

    public int repairAmounts = 3;
    public int healAmount = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && repairAmounts > 0)
        {
            Repair();
        }
    }

    private void Repair()
    {
        Player.instance.AddHealth(healAmount);
        repairAmounts--;
    }
}
