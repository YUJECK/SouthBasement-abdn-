using EnemysAI.Other;
using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI.Controllers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Sleeping))]
    public class AngryRatComponentsController : EnemyAI
    {
        private void Start()
        {
            //Написать добавление лисэнеров в ивенты
            //Продумать работу стана
            //Перестать кодить без плана
        }
    }
}