using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Evie.Template
{
    public class SpellDetailModel : LayoutModel
    {
        public SpellDetailModel(TemplateRenderContext context, int spell_id) : base(context)
        {
            this.spell_id = spell_id;
            var spell = context.SpellFileRecords.FirstOrDefault(s => s.id == spell_id.ToString());
            if (spell != null)
            {
                this.spell = spell;
            }
        }

        public int spell_id { get; set; }
        public EQSpell spell { get; set; }

        public EQSpell GetSpell(int spell_id)
        {
            return Context.SpellFileRecords.FirstOrDefault(s => s.id == spell_id.ToString());
        }

        public ExtraNotes[] GetNotes()
        {
            return ExtraNotes.GetNotes(spell);
        }

        public string FormatClasses()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var eqclass in EQClass.Classes)
            {
                if (spell.classes[eqclass.Number] != 255)
                {
                    sb.AppendFormat("{0}/{1} ", eqclass.ShortName, spell.classes[eqclass.Number]);
                }
            }

            return sb.Length == 0 ? "None" : sb.ToString().Trim();
        }

        public string FormatInt(string string_val)
        {
            int val = EQSpell.ConvertToInt32(string_val);
            return String.Format("{0}", val);
        }

        public string FormatDouble(string string_val)
        {
            double val = EQSpell.ConvertToDouble(string_val);
            return String.Format("{0:F3}", val);
        }

        public string FormatTimeString(string string_ms)
        {
            double duration = EQSpell.ConvertToDouble(string_ms) / 1000.0;
            string extra = "";
            if (duration > 59)
            {
                TimeSpan ts = TimeSpan.FromSeconds(duration);
                extra = " (" + FormatTimeSpan(ts) + ")";
            }

            return String.Format("{0:F3} sec{1}", duration, extra);
        }

        static string FormatTimeSpan(TimeSpan timeSpan)
        {
            string result = "";

            if (timeSpan.Days > 0)
                result += $"{timeSpan.Days} day{(timeSpan.Hours > 1 ? "s" : "")} ";

            if (timeSpan.Hours > 0)
                result += $"{timeSpan.Hours} hour{(timeSpan.Hours > 1 ? "s" : "")} ";

            if (timeSpan.Minutes > 0)
                result += $"{timeSpan.Minutes} minute{(timeSpan.Minutes > 1 ? "s" : "")} ";

            if (timeSpan.Seconds > 0)
                result += $"{timeSpan.Seconds} second{(timeSpan.Seconds > 1 ? "s" : "")}";

            return result.Trim();
        }

        public string FormatDetrimental()
        {
            int val = EQSpell.ConvertToInt32(spell.goodEffect);
            switch (val)
            {
                case 0: return "Detrimental";
                case 1: return "Beneficial";
                case 2: return "Beneficial (Group Only)";
            }
            return String.Format("Unknown ({0})", val);
        }

        public string FormatBuffDuration()
        {
            if (spell.IsBuff())
            {
                if (EQSpell.ConvertToInt32(spell.buffdurationformula) == 50)
                    return "Permanent";

                int lowestLevelToUse = 70;
                for (int eqclass = 1; eqclass <= 16; eqclass++)
                {
                    if (spell.classes[eqclass] < lowestLevelToUse)
                        lowestLevelToUse = spell.classes[eqclass];
                }

                int low = 0, high = 0;
                string lowstr = "", highstr = "";
                for (int level = lowestLevelToUse; level <= 70; level++)
                {
                    int val = EQSpell.CalcBuffDuration_formula(level, EQSpell.ConvertToInt32(spell.buffdurationformula), EQSpell.ConvertToInt32(spell.buffduration));
                    if (level == lowestLevelToUse)
                    {
                        low = val;
                        TimeSpan ts = TimeSpan.FromSeconds(val * 6);
                        lowstr = String.Format("{0} ({1} ticks at L{2})", FormatTimeSpan(ts), val, level);
                    }
                    if (val != high || level == lowestLevelToUse)
                    {
                        high = val;
                        TimeSpan ts = TimeSpan.FromSeconds(val * 6);
                        highstr = String.Format("{0} ({1} ticks at L{2})", FormatTimeSpan(ts), val, level);
                    }
                }

                if (high == low)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(low * 6);
                    return String.Format("{0} ({1} ticks)", FormatTimeSpan(ts), low);
                }

                return String.Format("{0} to {1}", lowstr, highstr);

            }

            return "Instant (not a buff)";
        }

        public string FormatBuffDurationCalc()
        {
            if (spell.IsBuff())
            {
                return String.Format("formula {0} limit {1}", spell.buffdurationformula, spell.buffduration);
            }
            else
            {
                return "Instant (not a buff)";
            }
        }

        public string FormatTargetType()
        {
            int val = EQSpell.ConvertToInt32(spell.targettype);
            List<string> targetNotes = new List<string>();
            if (EQTargetType.NeedsTargetInRange(val))
            {
                targetNotes.Add("checks range to target");
            }
            else
            {
                targetNotes.Add("doesn't use target");
            }
            if (EQTargetType.IsAreaEffectTargetType(val))
            {
                targetNotes.Add("uses AE Radius");
            }
            string targetNotesString = String.Format(" ({0})", String.Join(", ", targetNotes));
            return String.Format("{0}({1}){2}", EQTargetType.GetName(val), val, targetNotesString);
        }

        public string FormatCastTime()
        {
            int val = EQSpell.ConvertToInt32(spell.cast_time);
            if (val == 0)
                return "Instant";

            return FormatTimeString(spell.cast_time);
        }

        public string FormatSkill()
        {
            int skill_id = EQSpell.ConvertToInt32(spell.skill);
            return String.Format("{0}({1})", EQSkill.GetName(skill_id), skill_id);
        }

        public string FormatRecourse()
        {
            int recourse_spell_id = EQSpell.ConvertToInt32(spell.RecourseLink);
            EQSpell recourse_spell = GetSpell(recourse_spell_id);
            if (recourse_spell != null)
            {
                return String.Format("Recourse: Cast <a href=\"{0}.html\">{1}</a> on caster", recourse_spell_id, recourse_spell.name);
            }
            return "No recourse spell";
        }

        public string FormatHeadContent()
        {
            string icons_css = System.IO.File.ReadAllText(System.IO.Path.Combine("www", "icons.css"));
            return String.Format("<style>{0}{1}</style>", Environment.NewLine, icons_css);
        }

        public string FormatProcSpellDescription(int effect_slot)
        {
            // effect 85 has the proc spell id in base1 but for shadowknights +1 is added to the id
            // some spells are SK only and the normal id doesn't make any sense, only the +1 is ever used, so we don't want to show that
            // some spells can not be used by SK and so the spell at the +1 position is nonsense and we don't want to show that either

            EQSpell proc_spell = GetSpell(spell.GetProcSpellID(effect_slot, false));
            EQSpell proc_spell_sk = GetSpell(spell.GetProcSpellID(effect_slot, true));

            string proc_spell_html = "N/A";
            if (proc_spell != null)
                proc_spell_html = String.Format("<a href=\"{0}.html\">{1}</a>", proc_spell.id, proc_spell.name);

            string proc_spell_sk_html = "N/A";
            if (proc_spell_sk != null)
                proc_spell_sk_html = String.Format("<a href=\"{0}.html\">{1}</a>", proc_spell_sk.id, proc_spell_sk.name);

            /*
            // this is a heuristic that only works for player cast spells
            // and it's always possible for SK to gain the buff through an item click or something
            bool sk_can_use = false;
            bool other_can_use = false;
            bool nobody_can_use = true;
            for (int c = 1; c <= 16; c++)
            {
                if (spell.classes[c] != 255)
                {
                    nobody_can_use = false;
                    if (c == (int)EQClassEnum.ShadowKnight)
                        sk_can_use = true;
                    else
                        other_can_use = true;
                }
            }

            int[] selfOrPet =
            {
                (int)EQTargetTypeEnum.ST_Self_6,
                (int)EQTargetTypeEnum.ST_Pet_14,
                (int)EQTargetTypeEnum.ST_SummonedPet_38,
                (int)EQTargetTypeEnum.ST_PetMaster_47,
            };
            // SK only self buff
            if (sk_can_use && !other_can_use && selfOrPet.Contains(EQSpell.ConvertToInt32(spell.targettype)))
            {
                return String.Format("{0} {1} for SK", EffectName(spell.effect[effect_slot]), proc_spell_sk_html);
            }
            // not SK usable buff - this is hard to filter because anything can be SK usable if on an item, and we can't tell that here
            if (!sk_can_use && !nobody_can_use && selfOrPet.Contains(EQSpell.ConvertToInt32(spell.targettype)))
            {
                return String.Format("{0} {1}", EffectName(spell.effect[effect_slot]), proc_spell_html);
            }
            */

            string effectName = EffectName(spell.effect[effect_slot]);
            switch (spell.effect[effect_slot])
            {
                case (int)EQSpellEffectEnum.WeaponProc:
                    effectName = "Add Proc:"; break;
                case (int)EQSpellEffectEnum.RangedProc:
                    effectName = "Add Ranged Proc:"; break;
                case (int)EQSpellEffectEnum.DefensiveProc:
                    effectName = "Add Defensive Proc:"; break;
            }

            string mod = spell.base2[effect_slot] != 0 ? String.Format(" mod {0}", spell.base2[effect_slot]) : "";

            string sk = String.Format(" ({0} for SK)", proc_spell_sk_html);
            return String.Format("{0} {1}{2}{3}", effectName, proc_spell_html, mod, spell.effect[effect_slot] == (int)(EQSpellEffectEnum.WeaponProc) ? sk : "");
        }

        public class FSEVRRresult
        {
            public EQSpell spell { get; set; }
            public int effect_slot { get; set; }
            public bool abs { get; set; }

            public int low_value { get; set; }
            public int low_level { get; set; }
            public int high_value { get; set; }
            public int high_level { get; set; }

            public string FormattedString { get; set; }
        }
        public string FormatSpellEffectValue_range(int effect_slot, bool abs = true, bool percent = false, Func<int, int> valueConverter = null)
        {
            string formattedString = null;
            int effect_id = spell.effect[effect_slot]; ;
            bool isHasteEffect = new[] { (int)EQSpellEffectEnum.AttackSpeed, (int)EQSpellEffectEnum.AttackSpeed2, (int)EQSpellEffectEnum.AttackSpeed3 }.Contains(effect_id);
            string unitstr = percent ? "%" : "";
            if (valueConverter == null)
                valueConverter = (i) => i;

            switch (spell.calc[effect_slot])
            {
                // constant
                case 100:
                    break;

                // varies with level
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 109:
                case 110:
                case 111:
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 121:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 130:
                case 131:
                case 132:
                default:
                    {
                        // this tries to show the beginning of the range from the level players first get the spell but sometimes
                        // a spell can be found on an item even earlier than that so it doesn't always give good results.
                        // for spells that players can't memorize, we will just show the scale from level 1, for player only spells,
                        // it's nice to see the value it starts at when you first get it.
                        int lowestPlayerLevelToUse = 71;
                        for (int eqclass = 1; eqclass <= 16; eqclass++)
                        {
                            if (spell.classes[eqclass] < lowestPlayerLevelToUse)
                                lowestPlayerLevelToUse = spell.classes[eqclass];
                        }

                        int lowestLevelToUse = lowestPlayerLevelToUse <= 70 ? lowestPlayerLevelToUse : 1;
                        int low_val = 0, low_level = 0, high_val = 0, high_level = 0;
                        for (int level = lowestLevelToUse; level <= 70; level++)
                        {
                            int val = spell.CalcSpellEffectValue(effect_slot, level);
                            int orig_val = val;
                            if (abs) val = Math.Abs(val);
                            if (level == lowestLevelToUse)
                            {
                                low_val = val;
                                low_level = level;
                            }
                            if (val != high_val || level == lowestLevelToUse)
                            {
                                high_val = val;
                                high_level = level;
                            }
                        }

                        if (high_val == low_val)
                        {
                            formattedString = String.Format("{0}{1}", valueConverter(low_val), unitstr);
                        }
                        else
                        {
                            string lowstr = String.Format("{0}{1} (L{2})", valueConverter(low_val), unitstr, low_level);
                            string highstr = String.Format("{0}{1} (L{2})", valueConverter(high_val), unitstr, high_level);

                            formattedString = String.Format("{0} to {1}", lowstr, highstr);
                        }

                        break;
                    }

                // varies with remaining duration, but duration can vary with level
                case 107:
                case 108:
                case 120:
                case 122:
                    {
                        // calculate max duration as level 70 i guess, rather than trying to list out duration variation too
                        int full_duration = EQSpell.CalcBuffDuration_formula(70, EQSpell.ConvertToInt32(spell.buffdurationformula), EQSpell.ConvertToInt32(spell.buffduration));

                        int low = 0, high = 0, high_duration = 0;
                        for (int duration = full_duration; duration > 0;)
                        {
                            duration--;
                            int val = spell.CalcSpellEffectValue(effect_slot, 70, duration);
                            //if (abs) val = Math.Abs(val);
                            if (duration == full_duration || (low == 0 && val != low))
                            {
                                low = val;
                            }
                            if (val != high || duration == full_duration)
                            {
                                high = val;
                                high_duration = duration;
                            }
                        }

                        if (high == low)
                        {
                            formattedString = String.Format("{0}{1}", valueConverter(low), unitstr);
                        }
                        else
                        {
                            int pertick = 0;
                            if (spell.calc[effect_slot] == 107) pertick = 1;
                            if (spell.calc[effect_slot] == 108) pertick = 2;
                            if (spell.calc[effect_slot] == 120) pertick = 5;
                            if (spell.calc[effect_slot] == 122) pertick = 12;

                            string incdec = high > low ? "increasing" : "decreasing";
                            string tickstr = String.Format("{0} by {1} each tick", incdec, pertick);
                            string lowstr = String.Format("{0}{1} (initial)", valueConverter(low), unitstr);
                            string highstr = String.Format("{0}{1} ({2} ticks)", valueConverter(high), unitstr, full_duration - high_duration);

                            formattedString = String.Format("{0} to {1} ({2})", lowstr, highstr, tickstr);
                        }

                        break;
                    }

                // random
                case 123:
                    {
                        int low = spell.CalcSpellEffectValue(effect_slot, 70, 0, 1, 100, 0);
                        int high = spell.CalcSpellEffectValue(effect_slot, 70, 0, 1, 100, 1);
                        if (abs)
                        {
                            low = Math.Abs(low);
                            high = Math.Abs(high);
                        }
                        formattedString = String.Format("{0} to {1} (random)", low, high);

                        break;
                    }

                // varies with hp ratio
                case 137:
                case 138:
                    {
                        int low = 0, low_hp = 0, high = 0, high_hp = 0;
                        for (int cur_hp = 100; cur_hp >= 0; cur_hp--)
                        {
                            int val = spell.CalcSpellEffectValue(effect_slot, 70, 0, cur_hp, 100);
                            if (abs) val = Math.Abs(val);
                            if (cur_hp == 100 || (low == 0 && val != low))
                            {
                                low = val;
                                low_hp = cur_hp;
                            }
                            if (val != high || cur_hp == 100)
                            {
                                high = val;
                                high_hp = cur_hp;
                            }
                        }
                        if (high == low)
                        {
                            formattedString = String.Format("{0}", low);
                        }
                        else
                        {
                            string lowstr = String.Format("{0}{1} ({2}% HP)", valueConverter(low), unitstr, low_hp);
                            string highstr = String.Format("{0}{1} ({2}% HP)", valueConverter(high), unitstr, high_hp);

                            formattedString = String.Format("{0} to {1}", lowstr, highstr);
                        }

                        break;
                    }
            }

            // constant
            if (formattedString == null)
            {
                int val = spell.CalcSpellEffectValue(effect_slot);
                if (abs) val = Math.Abs(val);
                formattedString = String.Format("{0}{1}", valueConverter(val), unitstr);
            }

            return formattedString;
        }

        public string FormatEffectValues(int effect_slot)
        {
            return String.Format("{0}({1}) {2}/{3}/{4}/{5}", Enum.GetName(typeof(EQSpellEffectEnum), spell.effect[effect_slot]), spell.effect[effect_slot], spell.base1[effect_slot], spell.base2[effect_slot], spell.max[effect_slot], spell.calc[effect_slot]);
        }

        public string FormatResist()
        {
            if (spell.IsDetrimental())
            {
                if (!spell.IsResistable())
                    return "Unresistable";

                int resisttype = EQSpell.ConvertToInt32(spell.resisttype);
                int resistval = EQSpell.ConvertToInt32(spell.ResistDiff);
                string resistName = resisttype.ToString();

                switch (resisttype)
                {
                    case (int)EQResistTypeEnum.RESIST_NONE:
                        resistName = "None"; break;
                    case (int)EQResistTypeEnum.RESIST_MAGIC:
                        resistName = "Magic"; break;
                    case (int)EQResistTypeEnum.RESIST_FIRE:
                        resistName = "Fire"; break;
                    case (int)EQResistTypeEnum.RESIST_COLD:
                        resistName = "Cold"; break;
                    case (int)EQResistTypeEnum.RESIST_POISON:
                        resistName = "Poison"; break;
                    case (int)EQResistTypeEnum.RESIST_DISEASE:
                        resistName = "Disease"; break;
                    case (int)EQResistTypeEnum.RESIST_CHROMATIC:
                        resistName = "Chromatic (lowest)"; break;
                    case (int)EQResistTypeEnum.RESIST_PRISMATIC:
                        resistName = "Prismatic (average)"; break;
                    case (int)EQResistTypeEnum.RESIST_PHYSICAL:
                        resistName = "Physical"; break;
                }

                return String.Format("{0} ({1})", resistName, resistval);
            }

            return "N/A";
        }

        public string FormatAEDuration()
        {
            int duration_ms = EQSpell.ConvertToInt32(spell.AEDuration);

            return String.Format("{0} ({1} hits)", FormatTimeString(spell.AEDuration), duration_ms / 2500);
        }

        public string EffectName(int effect)
        {
            switch (effect)
            {
                case (int)EQSpellEffectEnum.CurrentHP:
                    return "Hitpoints";
                case (int)EQSpellEffectEnum.CurrentMana:
                    return "Mana";
                case (int)EQSpellEffectEnum.CurrentEndurance:
                    return "Endurance";
                case (int)EQSpellEffectEnum.ArmorClass:
                    return "AC";
                case (int)EQSpellEffectEnum.SummonItem:
                    return "Summon Item";
                case (int)EQSpellEffectEnum.SummonItemIntoBag:
                    return "Summon Item (in bag)";
                case (int)EQSpellEffectEnum.ResistPoison:
                    return "Poison Resist";
                case (int)EQSpellEffectEnum.ResistMagic:
                    return "Magic Resist";
                case (int)EQSpellEffectEnum.ResistDisease:
                    return "Disease Resist";
                case (int)EQSpellEffectEnum.ResistFire:
                    return "Fire Resist";
                case (int)EQSpellEffectEnum.ResistCold:
                    return "Cold Resist";
                case (int)EQSpellEffectEnum.TotalHP:
                    return "Max Hitpoints";
                case (int)EQSpellEffectEnum.ManaPool:
                    return "Max Mana";
                case (int)EQSpellEffectEnum.CurrentHPOnce:
                    return "HP when cast";
                case (int)EQSpellEffectEnum.CurrentManaOnce:
                    return "Mana when cast";
                case (int)EQSpellEffectEnum.CurrentEnduranceOnce:
                    return "Endurance when cast";
                case (int)EQSpellEffectEnum.AttackSpeed:
                    return "Haste v1";
                case (int)EQSpellEffectEnum.AttackSpeed2:
                    return "Haste v2";
                case (int)EQSpellEffectEnum.AttackSpeed3:
                    return "Haste v3 (overhaste)";
                case (int)EQSpellEffectEnum.MovementSpeed:
                    return "Movement Speed";
            }

            return Enum.GetName(typeof(EQSpellEffectEnum), effect);
        }

        public string FormatEffectDescription(int slot)
        {
            int effect = spell.effect[slot];
            int base1 = spell.base1[slot];
            int base2 = spell.base2[slot];
            int max = spell.max[slot];
            int calc = spell.calc[slot];
            int effect_value = spell.CalcSpellEffectValue(slot); // just checking if increase/decrease with this value

            string value_range = FormatSpellEffectValue_range(slot); // format the range of values
            string incdec = EQSpell.IsSplurtFormula(calc) ? "Modify" : effect_value >= 0 ? "Increase" : "Decrease";
            string pertick = spell.IsBuff() ? " per tick" : "";

            switch (effect)
            {
                case (int)EQSpellEffectEnum.CurrentHP:
                case (int)EQSpellEffectEnum.CurrentMana:
                case (int)EQSpellEffectEnum.CurrentEndurance:
                    {
                        return String.Format("{0} {1} by {2}{3}", incdec, EffectName(spell.effect[slot]), value_range, pertick);
                    }
                case (int)EQSpellEffectEnum.ArmorClass:
                case (int)EQSpellEffectEnum.ATK:
                case (int)EQSpellEffectEnum.STR:
                case (int)EQSpellEffectEnum.STA:
                case (int)EQSpellEffectEnum.AGI:
                case (int)EQSpellEffectEnum.DEX:
                case (int)EQSpellEffectEnum.WIS:
                case (int)EQSpellEffectEnum.INT:
                case (int)EQSpellEffectEnum.CHA:
                case (int)EQSpellEffectEnum.ResistPoison:
                case (int)EQSpellEffectEnum.ResistMagic:
                case (int)EQSpellEffectEnum.ResistDisease:
                case (int)EQSpellEffectEnum.ResistFire:
                case (int)EQSpellEffectEnum.ResistCold:
                case (int)EQSpellEffectEnum.TotalHP:
                case (int)EQSpellEffectEnum.ManaPool:
                case (int)EQSpellEffectEnum.CurrentHPOnce:
                case (int)EQSpellEffectEnum.CurrentManaOnce:
                case (int)EQSpellEffectEnum.CurrentEnduranceOnce:
                    {
                        return String.Format("{0} {1} by {2}", incdec, EffectName(spell.effect[slot]), value_range);
                    }
                case (int)EQSpellEffectEnum.DefensiveProc:
                case (int)EQSpellEffectEnum.RangedProc:
                case (int)EQSpellEffectEnum.WeaponProc:
                    {
                        return FormatProcSpellDescription(slot);
                    }
                case (int)EQSpellEffectEnum.SummonItem:
                case (int)EQSpellEffectEnum.SummonItemIntoBag:
                    {
                        string summon_qty_str = value_range != "1" ? String.Format(" x {0}", value_range) : "";
                        return String.Format("{0}: <a href=\"http://lucy.allakhazam.com/item.html?id={1}\">{1}</a>{2}", EffectName(spell.effect[slot]), base1, summon_qty_str);
                    }
                case (int)EQSpellEffectEnum.Fear:
                    {
                        string levelLimit = spell.max[slot] != 0 ? String.Format(" up to level {0}", spell.max[slot]) : "";
                        return String.Format("{0}{1}", EffectName(spell.effect[slot]), levelLimit);
                    }
                case (int)EQSpellEffectEnum.Stun:
                    {
                        string levelLimit = spell.max[slot] != 0 ? String.Format(" up to level {0}", spell.max[slot]) : "";
                        return String.Format("{0} for {1}{2}", EffectName(spell.effect[slot]), FormatTimeString(spell.base1[slot].ToString()), levelLimit);
                    }
                case (int)EQSpellEffectEnum.AttackSpeed:
                case (int)EQSpellEffectEnum.AttackSpeed2:
                case (int)EQSpellEffectEnum.AttackSpeed3:
                    {
                        if (effect != (int)EQSpellEffectEnum.AttackSpeed3) // effect 119 only has values 0-30, doesn't include the 100
                        {
                            incdec = Math.Abs(spell.base1[slot]) < 100 ? "Decrease" : "Increase";
                        }
                        var vc = (int i) =>
                        {
                            return Math.Abs(i + (effect == (int)EQSpellEffectEnum.AttackSpeed3 ? 0 : -100));
                        };
                        value_range = FormatSpellEffectValue_range(slot, false, true, vc);
                        goto case (int)EQSpellEffectEnum.CHA;
                    }
                case (int)EQSpellEffectEnum.MovementSpeed:
                    {
                        value_range = FormatSpellEffectValue_range(slot, true, true);
                        goto case (int)EQSpellEffectEnum.CHA;
                    }
                case (int)EQSpellEffectEnum.Teleport:
                case (int)EQSpellEffectEnum.Teleport2:
                case (int)EQSpellEffectEnum.Translocate:
                    {
                        return String.Format("{0} to {1}, {2}, {3} heading {4} in {5}",
                            effect == (int)EQSpellEffectEnum.Translocate ? "Translocate" : "Teleport",
                            EQSpell.ConvertToDouble(spell.effect_base_value1),
                            EQSpell.ConvertToDouble(spell.effect_base_value2),
                            EQSpell.ConvertToDouble(spell.effect_base_value3),
                            EQSpell.ConvertToDouble(spell.effect_base_value4),
                            spell.teleport_zone);
                    }
                default:
                    {
                        return String.Format("{0} {1}", EffectName(spell.effect[slot]), FormatSpellEffectValue_range(slot, false));
                    }
            }
        }
    }
}
