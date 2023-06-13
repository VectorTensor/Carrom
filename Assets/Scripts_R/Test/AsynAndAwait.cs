using System.Threading.Tasks;
using UnityEngine;

public class AsynAndAwait : MonoBehaviour
{
    // Update is called once per frame
    async void Start()
    {
        await DoSomething();
        Debug.Log("All Done");
    }

    async Task DoSomething()
    {
        await Eating();
        Debug.Log("Done Eating");
        await Sleep();
        Debug.Log("Sleeping");
    }

    async Task Eating()
    {
        Debug.Log("Eat after 3s");
        await Task.Delay(3000);
        Debug.Log("Eatting");
    }

    async Task Sleep()
    {
        await Task.Delay(2000);
        Debug.Log("Went to sleep");
    }

}
