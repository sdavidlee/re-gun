using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

class ControllerAssigner : MonoBehaviour
{
    public Controller controller { get; private set; }
    private IEnumerable<string> keys;

    private void Awake()
    {
        controller = GetComponent<Controller>();
    }

    private void Start()
    {
        keys =
            typeof(Controller)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(p => p.PropertyType == typeof(string))
                .GetKeys(controller)
                .RemoveElements("Horizontal", "Vertical");
    }

    private void Update()
    {
        if (IsAnyButtonDown())
        {
            PlayersManager.Instance.AddPlayer(controller);
            gameObject.SetActive(false);
        }
    }

    private bool IsAnyButtonDown()
    {
        foreach (var key in keys)
            if (Input.GetButton(key))
                return true;

        return false;
    }
}