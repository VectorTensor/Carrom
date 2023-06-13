using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class gameName : MonoBehaviour
{
    [SerializeField] Text tx;

    async void Start()
    {
        await WaitDataRetrival();
        tx.text = FirebaseSetup.fd.GameName; 
    }

    async Task WaitDataRetrival()
    {
        while (string.IsNullOrEmpty(FirebaseSetup.fd.GameName))
        {
            await Task.Yield();
        }
    }
}
