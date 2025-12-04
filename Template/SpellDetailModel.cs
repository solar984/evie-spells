using System;
using System.Collections.Generic;
using System.Linq;
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

                int lowestLevelToUse = spell.LowestLevelToUse();

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

        public bool UsesReagents()
        {
            return (String.IsNullOrWhiteSpace(spell.components1) || spell.components1 == "-1") ? false : true;
        }
        public string FormatReagent()
        {
            if (String.IsNullOrWhiteSpace(spell.components1) || spell.components1 == "-1") return "";

            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= 4; i++)
            {
                int item_id = -1, quantity = 1;
                switch (i)
                {
                    case 1:
                        item_id = EQSpell.ConvertToInt32(spell.components1);
                        quantity = EQSpell.ConvertToInt32(spell.component_counts1);
                        break;
                    case 2:
                        item_id = EQSpell.ConvertToInt32(spell.components2);
                        quantity = EQSpell.ConvertToInt32(spell.component_counts2);
                        break;
                    case 3:
                        item_id = EQSpell.ConvertToInt32(spell.components3);
                        quantity = EQSpell.ConvertToInt32(spell.component_counts3);
                        break;
                    case 4:
                        item_id = EQSpell.ConvertToInt32(spell.components4);
                        quantity = EQSpell.ConvertToInt32(spell.component_counts4);
                        break;
                }
                if (item_id != -1)
                {
                    string quantity_str = quantity > 1 ? String.Format(" x{0}", quantity) : "";
                    sb.Append(String.Format("{2}<a href=\"http://lucy.allakhazam.com/item.html?id={0}\">{0}</a>{1}", item_id, quantity_str, i > 1 ? ", " : ""));
                }
            }

            return sb.ToString();
        }

        public string build_spell_description_token_string(string spell_description)
        {
            StringBuilder sb = new StringBuilder();
            for (int pos = 0; pos < spell_description.Length; pos++)
            {
                switch (spell_description[pos])
                {
                    case '%':
                        if (pos + 1 < spell_description.Length)
                        {
                            switch (spell_description[pos + 1])
                            {
                                case 'Y': // duration for current character level
                                case 'y':
                                case 'Z': // duration limit
                                case 'z':
                                    int duration = EQSpell.ConvertToInt32(spell.buffduration);
                                    if (spell_description[pos + 1] == 'Y' || spell_description[pos + 1] == 'y')
                                    {
                                        duration = EQSpell.CalcBuffDuration_formula(spell.LowestLevelToUse(), EQSpell.ConvertToInt32(spell.buffdurationformula), EQSpell.ConvertToInt32(spell.buffduration));
                                    }
                                    sb.AppendFormat(" {0}:{1:00}:{2:00}", 6 * duration / 3600, 6 * duration / 60 % 60, 6 * duration % 60);
                                    break;
                            }
                        }
                        pos++;
                        break;

                    case '#': // effect value low
                    case '@': // effect value high
                        if (pos + 1 < spell_description.Length)
                        {
                            int effect_slot = -1;
                            switch (spell_description[pos + 1])
                            {
                                case '1':
                                    effect_slot = 0;
                                    break;
                                case '2':
                                    effect_slot = 1;
                                    break;
                                case '3':
                                    effect_slot = 2;
                                    break;
                                case '4':
                                    effect_slot = 3;
                                    break;
                                case '5':
                                    effect_slot = 4;
                                    break;
                                case '6':
                                    effect_slot = 5;
                                    break;
                                case '7':
                                    effect_slot = 6;
                                    break;
                                case '8':
                                    effect_slot = 7;
                                    break;
                                case '9':
                                    effect_slot = 8;
                                    break;
                                case 'A':
                                case 'a':
                                    effect_slot = 9;
                                    break;
                                case 'B':
                                case 'b':
                                    effect_slot = 10;
                                    break;
                                case 'C':
                                case 'c':
                                    effect_slot = 11;
                                    break;
                            }
                            if (effect_slot >= 0)
                            {
                                int level = 70;
                                if (spell_description[pos] == '#')
                                {
                                    level = Math.Min(spell.LowestLevelToUse(), 70);
                                }

                                int max = spell.max[effect_slot];
                                int effect_id = spell.effect[effect_slot];
                                int base1 = spell.base1[effect_slot];
                                int calc = spell.calc[effect_slot];

                                int val = spell_description[pos] == '#' ? base1 : max;
                                if ((effect_id == (int)EQSpellEffectEnum.CurrentHP && calc != 123) || effect_id == (int)EQSpellEffectEnum.CurrentHPOnce || effect_id == (int)EQSpellEffectEnum.BardAEDot)
                                {
                                    val = spell.CalcSpellEffectValue(effect_slot, level);
                                }
                                sb.AppendFormat("{0}", Math.Abs(val));
                            }
                        }
                        pos++;
                        break;

                    default:
                        sb.Append(spell_description[pos]);
                        break;
                }
            }

            return sb.ToString();
        }

        public string FormatGameDescription()
        {
            if (!String.IsNullOrEmpty(spell.descnum))
            {
                int dbstrid = EQSpell.ConvertToInt32(spell.descnum);
                string dbstr = Context.EQStringDB.GetString(dbstrid, 6);

                return build_spell_description_token_string(dbstr);
            }

            return String.Empty;
        }

        public string FormatSpellCategory1()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(spell.typedescnum) && !String.IsNullOrEmpty(spell.effectdescnum))
            {
                string typedesc = Context.EQStringDB.GetString(EQSpell.ConvertToInt32(spell.typedescnum), 5);
                string effectdesc = Context.EQStringDB.GetString(EQSpell.ConvertToInt32(spell.effectdescnum), 5);
                sb.AppendFormat("{0} -> {1}", typedesc, effectdesc);
            }

            return sb.ToString();
        }

        public string FormatSpellCategory2()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(spell.typedescnum) && !String.IsNullOrEmpty(spell.effectdescnum2))
            {
                string typedesc = Context.EQStringDB.GetString(EQSpell.ConvertToInt32(spell.typedescnum), 5);
                string effectdesc = Context.EQStringDB.GetString(EQSpell.ConvertToInt32(spell.effectdescnum2), 5);
                sb.AppendFormat("{0} -> {1}", typedesc ?? "", effectdesc ?? "");
            }

            return sb.ToString();
        }

        public string FormatOldSpellEffectData_Stage(StageType o, string indentString)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}AttachTag: {2}{1}", indentString, Environment.NewLine, o.AttachTag);
            sb.AppendFormat("{0}BlitSprite: {2} {3} {4}{1}", indentString, Environment.NewLine, o.BlitSprite[0].Value, o.BlitSprite[1].Value, o.BlitSprite[2].Value);
            sb.AppendFormat("{0}DAGnum: {2} {3} {4}{1}", indentString, Environment.NewLine, o.DAGnum[0], o.DAGnum[1], o.DAGnum[2]);
            sb.AppendFormat("{0}pcloud: {2} {3} {4}{1}", indentString, Environment.NewLine, o.pcloud[0], o.pcloud[1], o.pcloud[2]);
            sb.AppendFormat("{0}SpriteEffect: {2}{1}", indentString, Environment.NewLine, o.SpriteEffect);
            sb.AppendFormat("{0}SoundNum: {2}{1}", indentString, Environment.NewLine, o.SoundNum);
            sb.AppendFormat("{0}Tint: {2} {3} {4}{1}", indentString, Environment.NewLine, String.Format("#{0:X8}", o.Tint[0]), String.Format("#{0:X8}", o.Tint[1]), String.Format("#{0:X8}", o.Tint[2]));
            sb.AppendFormat("{0}Gravity: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Gravity[0], o.Gravity[1], o.Gravity[2]);
            sb.AppendFormat("{0}NormalXYZ: {2},{3},{4} {5},{6},{7} {8},{9},{10}{1}", indentString, Environment.NewLine,
                o.NormalXYZ[0], o.NormalXYZ[1], o.NormalXYZ[2],
                o.NormalXYZ[3], o.NormalXYZ[4], o.NormalXYZ[5],
                o.NormalXYZ[6], o.NormalXYZ[7], o.NormalXYZ[8]);
            sb.AppendFormat("{0}Radius: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Radius[0], o.Radius[1], o.Radius[2]);
            sb.AppendFormat("{0}Angle: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Angle[0], o.Angle[1], o.Angle[2]);
            sb.AppendFormat("{0}Lifespan: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Lifespan[0], o.Lifespan[1], o.Lifespan[2]);
            sb.AppendFormat("{0}Velocity: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Velocity[0], o.Velocity[1], o.Velocity[2]);
            sb.AppendFormat("{0}Rate: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Rate[0], o.Rate[1], o.Rate[2]);
            sb.AppendFormat("{0}Scale: {2} {3} {4}{1}", indentString, Environment.NewLine, o.Scale[0], o.Scale[1], o.Scale[2]);

            sb.AppendFormat("{0}SpriteTAG: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.SpriteTAG[0].Value, o.SpriteTAG[1].Value, o.SpriteTAG[2].Value, o.SpriteTAG[3].Value, o.SpriteTAG[4].Value, o.SpriteTAG[5].Value,
                o.SpriteTAG[6].Value, o.SpriteTAG[7].Value, o.SpriteTAG[8].Value, o.SpriteTAG[9].Value, o.SpriteTAG[10].Value, o.SpriteTAG[11].Value);
            // these SpriteRGB values are always 0
            /*
            sb.AppendFormat("{0}SpriteRGB: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                String.Format("#{0:X6}", o.SpriteRGB[0].Red << 16 & o.SpriteRGB[0].Green << 8 & o.SpriteRGB[0].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[1].Red << 16 & o.SpriteRGB[1].Green << 8 & o.SpriteRGB[1].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[2].Red << 16 & o.SpriteRGB[2].Green << 8 & o.SpriteRGB[2].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[3].Red << 16 & o.SpriteRGB[3].Green << 8 & o.SpriteRGB[3].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[4].Red << 16 & o.SpriteRGB[4].Green << 8 & o.SpriteRGB[4].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[5].Red << 16 & o.SpriteRGB[5].Green << 8 & o.SpriteRGB[5].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[6].Red << 16 & o.SpriteRGB[6].Green << 8 & o.SpriteRGB[6].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[7].Red << 16 & o.SpriteRGB[7].Green << 8 & o.SpriteRGB[7].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[8].Red << 16 & o.SpriteRGB[8].Green << 8 & o.SpriteRGB[8].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[9].Red << 16 & o.SpriteRGB[9].Green << 8 & o.SpriteRGB[9].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[10].Red << 16 & o.SpriteRGB[10].Green << 8 & o.SpriteRGB[10].Blue),
                String.Format("#{0:X6}", o.SpriteRGB[11].Red << 16 & o.SpriteRGB[11].Green << 8 & o.SpriteRGB[11].Blue));
            */
            sb.AppendFormat("{0}RollRate: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.RollRate[0], o.RollRate[1], o.RollRate[2], o.RollRate[3], o.RollRate[4], o.RollRate[5],
                o.RollRate[6], o.RollRate[7], o.RollRate[8], o.RollRate[9], o.RollRate[10], o.RollRate[11]);
            sb.AppendFormat("{0}HeadingOffset: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.HeadingOffset[0], o.HeadingOffset[1], o.HeadingOffset[2], o.HeadingOffset[3], o.HeadingOffset[4], o.HeadingOffset[5],
                o.HeadingOffset[6], o.HeadingOffset[7], o.HeadingOffset[8], o.HeadingOffset[9], o.HeadingOffset[10], o.HeadingOffset[11]);
            sb.AppendFormat("{0}PitchOffset: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.PitchOffset[0], o.PitchOffset[1], o.PitchOffset[2], o.PitchOffset[3], o.PitchOffset[4], o.PitchOffset[5],
                o.PitchOffset[6], o.PitchOffset[7], o.PitchOffset[8], o.PitchOffset[9], o.PitchOffset[10], o.PitchOffset[11]);
            sb.AppendFormat("{0}Distance: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.Distance[0], o.Distance[1], o.Distance[2], o.Distance[3], o.Distance[4], o.Distance[5],
                o.Distance[6], o.Distance[7], o.Distance[8], o.Distance[9], o.Distance[10], o.Distance[11]);
            sb.AppendFormat("{0}EffectType: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.EffectType[0], o.EffectType[1], o.EffectType[2], o.EffectType[3], o.EffectType[4], o.EffectType[5],
                o.EffectType[6], o.EffectType[7], o.EffectType[8], o.EffectType[9], o.EffectType[10], o.EffectType[11]);
            sb.AppendFormat("{0}ScaleFactor: {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}{1}", indentString, Environment.NewLine,
                o.ScaleFactor[0], o.ScaleFactor[1], o.ScaleFactor[2], o.ScaleFactor[3], o.ScaleFactor[4], o.ScaleFactor[5],
                o.ScaleFactor[6], o.ScaleFactor[7], o.ScaleFactor[8], o.ScaleFactor[9], o.ScaleFactor[10], o.ScaleFactor[11]);

            return sb.ToString();
        }

        public string FormatOldSpellEffectData()
        {
            string indentString = "    ";
            StringBuilder sb = new StringBuilder();

            int spaix = EQSpell.ConvertToInt32(spell.SpellAffectIndex);
            if (spaix >= 0 && spaix <= 255)
            {
                var spellEffect = Context.OldSpellEffects[spaix];

                for (int stage = 0; stage < 3; stage++)
                {
                    sb.AppendFormat("{0}{1}{2}", stage, Environment.NewLine, FormatOldSpellEffectData_Stage(spellEffect.types[stage], indentString));
                }
            }

            return sb.ToString();
        }

        public string FormatNewSpellEffectData_Stage(StageTypeNew o, string indentString)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}SoundNum: {2}{1}", indentString, Environment.NewLine, o.SoundNum);
            for (int i = 0; i < 4; i++)
            {
                var e = o.Emitters[i];
                sb.AppendFormat("{0}{2}{1}", indentString, Environment.NewLine, i);
                sb.AppendFormat("{0}{0}EmitterType: {2}{1}", indentString, Environment.NewLine, e.EmitterType);
                sb.AppendFormat("{0}{0}MinLevel: {2}{1}", indentString, Environment.NewLine, e.MinLevel);
                sb.AppendFormat("{0}{0}AttachType: {2}{1}", indentString, Environment.NewLine, e.AttachType);
                sb.AppendFormat("{0}{0}DAGnum: {2}{1}", indentString, Environment.NewLine, e.DAGnum);
            }

            return sb.ToString();
        }

        public string FormatNewSpellEffectData()
        {
            string indentString = "    ";
            StringBuilder sb = new StringBuilder();

            //for (int spaix = 0; spaix < Context.NewSpellEffects.Length; spaix++)
            {
                int spaix = EQSpell.ConvertToInt32(spell.spellanim);
                if (spaix >= 0 && spaix < Context.NewSpellEffects.Length)
                {
                    var spellEffect = Context.NewSpellEffects[spaix];
                    sb.AppendFormat("{0} {1}{2}", spaix, spellEffect.Name, Environment.NewLine);

                    for (int stage = 0; stage < 3; stage++)
                    {
                        sb.AppendFormat("{0}{1}{2}", stage, Environment.NewLine, FormatNewSpellEffectData_Stage(spellEffect.Stage[stage], indentString));
                    }
                }
            }

            return sb.ToString();
        }

        public string FormatNewSpellEffectName()
        {
            int spaix = EQSpell.ConvertToInt32(spell.spellanim);
            if (spaix >= 0 && spaix < Context.NewSpellEffects.Length)
            {
                return String.Format("{0} {1}", spaix, Context.NewSpellEffects[spaix].Name);
            }

            return spell.spellanim;
        }

        public enum AudioFileType : int
        {
            NewEffectCaster,
            NewEffectTarget,
            OldEffectCaster,
            OldEffectTarget,
        };
        public int GetAudioFileNumber(AudioFileType type)
        {
            int old_effect_ix = EQSpell.ConvertToInt32(spell.SpellAffectIndex);
            int new_effect_ix = EQSpell.ConvertToInt32(spell.spellanim);
            int soundnum = -1;

            switch (type)
            {
                case AudioFileType.NewEffectCaster:
                case AudioFileType.NewEffectTarget:
                    if (new_effect_ix >= 0 && new_effect_ix < Context.NewSpellEffects.Length)
                    {
                        int stage = type == AudioFileType.NewEffectTarget ? 2 : 0;
                        soundnum = Context.NewSpellEffects[new_effect_ix].Stage[stage].SoundNum;
                    }
                    break;
                case AudioFileType.OldEffectCaster:
                case AudioFileType.OldEffectTarget:
                    if (old_effect_ix >= 0 && old_effect_ix <= Context.OldSpellEffects.Length)
                    {
                        int stage = type == AudioFileType.OldEffectTarget ? 2 : 0;
                        soundnum = Context.OldSpellEffects[old_effect_ix].types[stage].SoundNum;
                    }
                    break;
            }

            return soundnum;
        }
        public string GetAudioFileName(AudioFileType type)
        {
            int soundnum = GetAudioFileNumber(type); ;

            if (soundnum >= 0 && EQSounds.SoundNumberToFileNameMap.ContainsKey(soundnum))
            {
                return EQSounds.SoundNumberToFileNameMap[soundnum];
            }

            return null;
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
                case (int)EQSpellEffectEnum.InstantHate:
                    return "Instant Hate";
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
                case (int)EQSpellEffectEnum.ChangeAggro:
                    return "Hate Modifier";
                case (int)EQSpellEffectEnum.ChangeFrenzyRad:
                    return "NPC Aggro Radius ";
                case (int)EQSpellEffectEnum.Harmony:
                    return "NPC Assist Radius ";
                case (int)EQSpellEffectEnum.Lull:
                    return "Pacify";
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
                case (int)EQSpellEffectEnum.Hate:
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
                case (int)EQSpellEffectEnum.InstantHate:
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
                case (int)EQSpellEffectEnum.ChangeFrenzyRad:
                case (int)EQSpellEffectEnum.Harmony:
                    {
                        string levelLimit = spell.max[slot] != 0 ? String.Format(" up to level {0}", spell.max[slot]) : "";
                        return String.Format("{0} {1}{2}", EffectName(spell.effect[slot]), value_range, levelLimit);
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
                case (int)EQSpellEffectEnum.ChangeAggro:
                    {
                        value_range = FormatSpellEffectValue_range(slot, true, true);
                        goto case (int)EQSpellEffectEnum.CHA;
                    }
                case (int)EQSpellEffectEnum.Teleport:
                case (int)EQSpellEffectEnum.Teleport2:
                case (int)EQSpellEffectEnum.Translocate:
                case (int)EQSpellEffectEnum.Succor:
                    {
                        return String.Format("{0} to {1}, {2}, {3} heading {4} in {5}",
                            effect == (int)EQSpellEffectEnum.Translocate ? "Translocate" : "Teleport",
                            EQSpell.ConvertToDouble(spell.effect_base_value1),
                            EQSpell.ConvertToDouble(spell.effect_base_value2),
                            EQSpell.ConvertToDouble(spell.effect_base_value3),
                            EQSpell.ConvertToDouble(spell.effect_base_value4),
                            spell.teleport_zone);
                    }
                case (int)EQSpellEffectEnum.SkillAttack:
                    {
                        string hitchance = spell.base2[slot] != 0 ? String.Format(" ({0}% hit chance bonus)", spell.base2[slot] / 100) : "";
                        return String.Format("{0} attack for {1} base damage{2}", EQSkill.GetName(EQSpell.ConvertToInt32(spell.skill)), spell.base1[slot], hitchance);
                    }
                case (int)EQSpellEffectEnum.SkillDamageTaken:
                    {
                        string skillstr = spell.base2[slot] != -1 ? EQSkill.GetName(spell.base2[slot]) : "all";
                        return String.Format("{0} {1} skill damage taken by {2}", incdec, skillstr, value_range);
                    }
                case (int)EQSpellEffectEnum.DamageModifier:
                    {
                        string skillstr = spell.base2[slot] != -1 ? EQSkill.GetName(spell.base2[slot]) : "all";
                        return String.Format("{0} {1} skill damage dealt by {2}", incdec, skillstr, value_range);
                    }
                default:
                    {
                        return String.Format("{0} {1}", EffectName(spell.effect[slot]), FormatSpellEffectValue_range(slot, false));
                    }
            }
        }
    }
}
