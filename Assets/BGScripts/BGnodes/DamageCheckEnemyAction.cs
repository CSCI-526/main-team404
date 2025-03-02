using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamageCheckEnemy", story: "Do Damage check using [Weapon]", category: "Action", id: "287725b74203ffbff8a7d2aaa40690ab")]
public partial class DamageCheckEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Weapon;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

