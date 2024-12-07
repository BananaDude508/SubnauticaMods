using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace BananaDude508.CustomStartMenu
{
    public class MenuLogoPatch : MonoBehaviour
    {
        private Vector3 defaultPos;
        float bobTime = 3f; // Seconds to fully bob
        float rotateTime = 5f; // Seconds to fully rotate
        int entityAmount = 5; // Amount of players to spawn

        public void Start()
        {
            defaultPos = transform.position;

            // GameObject srPrefab = new PerformanceConsoleCommands().sizeRefPrefab;
            // for (int i = 0; i < entityAmount; i++)
            // {
            //     Utils.SpawnPrefabAt(srPrefab, transform, Random.onUnitSphere);
            // }
        }

        public void Update ()
        {
            transform.position = defaultPos + (Vector3.up * 2 * Mathf.Sin((2 * Mathf.PI * Time.time) / bobTime));

            Quaternion r = transform.rotation;
            r.eulerAngles = Vector3.up * Mathf.Repeat(Time.time * 360/rotateTime, 360f);
            transform.rotation = r;
        }
    }
}
