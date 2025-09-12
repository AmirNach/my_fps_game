using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTextAlignment : MonoBehaviour
{
    public TMP_Text HealthText;

    void Update()
    {
        HealthText.transform.rotation = Quaternion.LookRotation(HealthText.transform.position - Camera.main.transform.position);
    }
}
