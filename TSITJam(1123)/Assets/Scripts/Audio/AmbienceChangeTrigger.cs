using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Audio Parametrs")]
    [SerializeField] private string parametrName;
    [SerializeField] private float parametrValue;

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
