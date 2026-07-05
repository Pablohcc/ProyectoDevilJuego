using UnityEngine;
using System.Reflection;

[DefaultExecutionOrder(-200)]
public class FixTextJobs : MonoBehaviour
{
    void Awake()
    {
        ApplyFix();
    }

    void OnEnable()
    {
        ApplyFix();
    }

    static void ApplyFix()
    {
        var assembly = typeof(UnityEngine.UIElements.UIDocument).Assembly;
        var type = assembly.GetType("UnityEngine.UIElements.UITKTextJobSystem");
        if (type == null) return;

        var field = type.GetField("s_UseJobSystem",
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (field != null)
        {
            field.SetValue(null, false);
            Debug.Log("[FixTextJobs] UITKTextJobSystem desactivado.");
        }
    }
}