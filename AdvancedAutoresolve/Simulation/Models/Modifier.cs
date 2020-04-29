﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAutoResolve.Simulation.Models
{
    internal struct Modifiers
    {
        public Modifiers(float attackBonus, float defenseBonus)
        {
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
        }

        public float AttackBonus { get; }
        public float DefenseBonus { get; }

        public static Modifiers GetDefaultModifiers()
        {
            return new Modifiers(0.7f, 0.7f);
        }

    }
}