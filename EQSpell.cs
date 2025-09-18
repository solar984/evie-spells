using Evie.Titanium;
using System;
using System.Globalization;

namespace Evie
{
    public class EQSpell : SpellFileRecord
    {
        public EQSpell() : base()
        {
            effect = new FieldIndexer(this, "effect");
            base1 = new FieldIndexer(this, "base1");
            base2 = new FieldIndexer(this, "base2");
            max = new FieldIndexer(this, "max");
            calc = new FieldIndexer(this, "calc");

            classes = new FieldIndexer(this, "classes");
        }

        // for iterating effects
        public FieldIndexer effect { get; }
        public FieldIndexer base1 { get; }
        public FieldIndexer base2 { get; }
        public FieldIndexer max { get; }
        public FieldIndexer calc { get; }

        public FieldIndexer classes { get; }

        // this method treats a blank field in the spell file as a 0
        public static int ConvertToInt32(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return 0;

            return Convert.ToInt32(str, CultureInfo.InvariantCulture);
        }

        // this method treats a blank field in the spell file as a 0
        public static double ConvertToDouble(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return 0;

            return Convert.ToDouble(str, CultureInfo.InvariantCulture);
        }

        public class FieldIndexer
        {
            public EQSpell s { get; set; }
            public string name { get; set; }
            public FieldIndexer(EQSpell spell, string name)
            {
                this.s = spell;
                this.name = name;
            }

            public int this[int index]
            {
                get
                {
                    switch (name)
                    {
                        case "effect":
                            {
                                if (index < 0 || index > 11)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 0: return ConvertToInt32(s.effectid1);
                                    case 1: return ConvertToInt32(s.effectid2);
                                    case 2: return ConvertToInt32(s.effectid3);
                                    case 3: return ConvertToInt32(s.effectid4);
                                    case 4: return ConvertToInt32(s.effectid5);
                                    case 5: return ConvertToInt32(s.effectid6);
                                    case 6: return ConvertToInt32(s.effectid7);
                                    case 7: return ConvertToInt32(s.effectid8);
                                    case 8: return ConvertToInt32(s.effectid9);
                                    case 9: return ConvertToInt32(s.effectid10);
                                    case 10: return ConvertToInt32(s.effectid11);
                                    case 11: return ConvertToInt32(s.effectid12);
                                }
                            }
                            break;

                        case "base1":
                            {
                                if (index < 0 || index > 11)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 0: return ConvertToInt32(s.effect_base_value1);
                                    case 1: return ConvertToInt32(s.effect_base_value2);
                                    case 2: return ConvertToInt32(s.effect_base_value3);
                                    case 3: return ConvertToInt32(s.effect_base_value4);
                                    case 4: return ConvertToInt32(s.effect_base_value5);
                                    case 5: return ConvertToInt32(s.effect_base_value6);
                                    case 6: return ConvertToInt32(s.effect_base_value7);
                                    case 7: return ConvertToInt32(s.effect_base_value8);
                                    case 8: return ConvertToInt32(s.effect_base_value9);
                                    case 9: return ConvertToInt32(s.effect_base_value10);
                                    case 10: return ConvertToInt32(s.effect_base_value11);
                                    case 11: return ConvertToInt32(s.effect_base_value12);
                                }
                            }
                            break;

                        case "base2":
                            {
                                if (index < 0 || index > 11)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 0: return ConvertToInt32(s.effect_limit_value1);
                                    case 1: return ConvertToInt32(s.effect_limit_value2);
                                    case 2: return ConvertToInt32(s.effect_limit_value3);
                                    case 3: return ConvertToInt32(s.effect_limit_value4);
                                    case 4: return ConvertToInt32(s.effect_limit_value5);
                                    case 5: return ConvertToInt32(s.effect_limit_value6);
                                    case 6: return ConvertToInt32(s.effect_limit_value7);
                                    case 7: return ConvertToInt32(s.effect_limit_value8);
                                    case 8: return ConvertToInt32(s.effect_limit_value9);
                                    case 9: return ConvertToInt32(s.effect_limit_value10);
                                    case 10: return ConvertToInt32(s.effect_limit_value11);
                                    case 11: return ConvertToInt32(s.effect_limit_value12);
                                }
                            }
                            break;

                        case "max":
                            {
                                if (index < 0 || index > 11)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 0: return ConvertToInt32(s.max1);
                                    case 1: return ConvertToInt32(s.max2);
                                    case 2: return ConvertToInt32(s.max3);
                                    case 3: return ConvertToInt32(s.max4);
                                    case 4: return ConvertToInt32(s.max5);
                                    case 5: return ConvertToInt32(s.max6);
                                    case 6: return ConvertToInt32(s.max7);
                                    case 7: return ConvertToInt32(s.max8);
                                    case 8: return ConvertToInt32(s.max9);
                                    case 9: return ConvertToInt32(s.max10);
                                    case 10: return ConvertToInt32(s.max11);
                                    case 11: return ConvertToInt32(s.max12);
                                }
                            }
                            break;

                        case "calc":
                            {
                                if (index < 0 || index > 11)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 0: return ConvertToInt32(s.formula1);
                                    case 1: return ConvertToInt32(s.formula2);
                                    case 2: return ConvertToInt32(s.formula3);
                                    case 3: return ConvertToInt32(s.formula4);
                                    case 4: return ConvertToInt32(s.formula5);
                                    case 5: return ConvertToInt32(s.formula6);
                                    case 6: return ConvertToInt32(s.formula7);
                                    case 7: return ConvertToInt32(s.formula8);
                                    case 8: return ConvertToInt32(s.formula9);
                                    case 9: return ConvertToInt32(s.formula10);
                                    case 10: return ConvertToInt32(s.formula11);
                                    case 11: return ConvertToInt32(s.formula12);
                                }
                            }
                            break;

                        case "classes":
                            {
                                if (index < 1 || index > 16)
                                    throw new IndexOutOfRangeException();
                                switch (index)
                                {
                                    case 1: return ConvertToInt32(s.classes1);
                                    case 2: return ConvertToInt32(s.classes2);
                                    case 3: return ConvertToInt32(s.classes3);
                                    case 4: return ConvertToInt32(s.classes4);
                                    case 5: return ConvertToInt32(s.classes5);
                                    case 6: return ConvertToInt32(s.classes6);
                                    case 7: return ConvertToInt32(s.classes7);
                                    case 8: return ConvertToInt32(s.classes8);
                                    case 9: return ConvertToInt32(s.classes9);
                                    case 10: return ConvertToInt32(s.classes10);
                                    case 11: return ConvertToInt32(s.classes11);
                                    case 12: return ConvertToInt32(s.classes12);
                                    case 13: return ConvertToInt32(s.classes13);
                                    case 14: return ConvertToInt32(s.classes14);
                                    case 15: return ConvertToInt32(s.classes15);
                                    case 16: return ConvertToInt32(s.classes16);
                                }
                            }
                            break;

                        default:
                            throw new ArgumentException(String.Format("Unknown indexer name '{0}'", name));
                    }
                    return 0;
                }
            }
        }

        public bool IsBlankSpellEffect(int slot)
        {
            if (effect[slot] == (int)EQSpellEffectEnum.Blank)
                return true;

            // this set of flags is used as a spacer to move effects into a higher slot for creating stacking conflicts
            if (effect[slot] == (int)EQSpellEffectEnum.CHA && calc[slot] == 100 && base1[slot] == 0)
                return true;

            return false;
        }

        // the buffdurationformula is what makes it a buff.  the buffduration can be 0 and still result in a duration depending on the formula.
        public bool IsBuff()
        {
            return ConvertToInt32(buffdurationformula) != 0;
        }

        public static bool IsSplurtFormula(int calc)
        {
            return (calc == 107 || calc == 108 || calc == 120 || calc == 122);
        }

        // checked this against the titanium client and validated that it produces the same output for all formulas/durations/levels 20250913
        public static int CalcBuffDuration_formula(int level, int formula, int duration)
        {
            int temp;

            switch (formula)
            {
                case 1:
                    temp = level > 3 ? level / 2 : 1;
                    break;
                case 2:
                    temp = level > 3 ? level / 2 + 5 : 6;
                    break;
                case 3:
                    temp = 30 * level;
                    break;
                case 4: // only used by 'LowerElement'
                    temp = 50;
                    break;
                case 5:
                    temp = 2;
                    break;
                case 6:
                    temp = level / 2 + 2;
                    break;
                case 7:
                    temp = level;
                    break;
                case 8:
                    temp = level + 10;
                    break;
                case 9:
                    temp = 2 * level + 10;
                    break;
                case 10:
                    temp = 3 * level + 10;
                    break;
                case 11:
                    temp = 30 * (level + 3);
                    break;
                case 12:
                    temp = level > 7 ? level / 4 : 1;
                    break;
                case 13:
                    temp = 4 * level + 10;
                    break;
                case 14:
                    temp = 5 * (level + 2);
                    break;
                case 15:
                    temp = 10 * (level + 10);
                    break;
                case 50: // Permanent. Cancelled by casting/combat for perm invis, non-lev zones for lev, curing poison/curse
                         // counters, etc.
                    return -1;
                default:
                    // the client function has another bool parameter that if true returns -2 -- unsure
                    if (formula < 200)
                        return 0;
                    temp = formula;
                    break;
            }
            if (duration != 0 && duration < temp)
                temp = duration;
            return temp;
        }

        public int CalcSpellEffectValue(int effect_slot, int level = 70, int ticsremaining = 0, int cur_hp = 1, int max_hp = 100, int randomhigh = 1)
        {
            int effect_id = effect[effect_slot];
            int formula = calc[effect_slot];
            int base_value = base1[effect_slot];
            int max_value = max[effect_slot];
            int buff_duration_formula = EQSpell.ConvertToInt32(buffdurationformula);
            int buff_duration = EQSpell.ConvertToInt32(buffduration);

            return CalcSpellEffectValue_formula(effect_id, formula, base_value, max_value, buff_duration_formula, buff_duration, level, ticsremaining, cur_hp, max_hp, randomhigh);
        }

        public static int CalcSpellEffectValue_formula(int effect_id, int formula, int base_value, int max_value, int buff_duration_formula, int buff_duration, int level, int ticsremaining, int cur_hp, int max_hp, int randomhigh)
        {
            int base_abs = Math.Abs(base_value);
            int formula_value = 0;

            switch (formula)
            {
                case 100:
                    formula_value = 0;
                    break;

                case 101:
                    formula_value = level / 2;
                    break;

                case 102:
                    formula_value = level;
                    break;

                case 103:
                    formula_value = 2 * level;
                    break;

                case 104:
                    formula_value = 3 * level;
                    break;

                case 105:
                    formula_value = 4 * level;
                    break;

                case 107:
                    if (buff_duration_formula == 0)
                        break;
                    formula_value = (CalcBuffDuration_formula(level, buff_duration_formula, buff_duration) - ticsremaining);
                    if (base_abs >= 0)
                        formula_value = -formula_value;
                    break;

                case 108:
                    if (buff_duration_formula == 0)
                        break;
                    formula_value = 2 * (CalcBuffDuration_formula(level, buff_duration_formula, buff_duration) - ticsremaining);
                    if (base_abs >= 0)
                        formula_value = -formula_value;
                    break;

                case 109:
                    formula_value = level / 4;
                    break;

                case 110:
                    formula_value = level / 6;
                    break;

                case 111:
                    if (level > 16)
                        formula_value = 6 * (level - 16);
                    break;

                case 112:
                    if (level > 24)
                        formula_value = 8 * (level - 24);
                    break;

                case 113:
                    if (level > 34)
                        formula_value = 10 * (level - 34);
                    break;

                case 114:
                    if (level > 44)
                        formula_value = 15 * (level - 44);
                    break;

                case 115:
                    if (level > 15)
                        formula_value = 7 * (level - 15);
                    break;

                case 116:
                    if (level > 24)
                        formula_value = 10 * (level - 24);
                    break;

                case 117:
                    if (level > 34)
                        formula_value = 13 * (level - 34);
                    break;

                case 118:
                    if (level > 44)
                        formula_value = 20 * (level - 44);
                    break;

                case 119:
                    formula_value = level / 8;
                    break;

                case 120:
                    if (buff_duration_formula == 0)
                        break;
                    formula_value = 5 * (CalcBuffDuration_formula(level, buff_duration_formula, buff_duration) - ticsremaining);
                    if (base_abs >= 0)
                        formula_value = -formula_value;
                    break;

                case 121:
                    formula_value = level / 3;
                    break;

                case 122:
                    if (buff_duration_formula == 0)
                        break;
                    formula_value = 12 * (CalcBuffDuration_formula(level, buff_duration_formula, buff_duration) - ticsremaining);
                    if (base_abs >= 0)
                        formula_value = -formula_value;
                    break;

                case 123:
                    //formula_value = zone->random.Int(1, max_value - base_abs);
                    formula_value = randomhigh != 0 ? max_value - base_abs : 1;
                    break;

                case 124:
                    if (level > 50)
                        formula_value = level - 50;
                    break;

                case 125:
                    if (level > 50)
                        formula_value = 2 * (level - 50);
                    break;

                case 126:
                    if (level > 50)
                        formula_value = 3 * (level - 50);
                    break;

                case 127:
                    if (level > 50)
                        formula_value = 4 * (level - 50);
                    break;

                case 128:
                    if (level > 50)
                        formula_value = 5 * (level - 50);
                    break;

                case 129:
                    if (level > 50)
                        formula_value = 10 * (level - 50);
                    break;

                case 130:
                    if (level > 50)
                        formula_value = 15 * (level - 50);
                    break;

                case 131:
                    if (level > 50)
                        formula_value = 20 * (level - 50);
                    break;

                case 132:
                    if (level > 50)
                        formula_value = 25 * (level - 50);
                    break;

                case 137:
                    formula_value = -(base_abs * cur_hp / max_hp);
                    break;

                case 138:
                    {
                        int half_hp = max_hp / 2;
                        if (cur_hp <= half_hp)
                        {
                            formula_value = base_abs * cur_hp / half_hp;
                            formula_value = -formula_value;
                        }
                        else
                        {
                            formula_value = base_abs;
                        }
                        break;
                    }

                default:
                    formula_value = formula * level;
                    break;
            }

            // add base value
            int final_value = base_abs + formula_value;

            // slow effect is expressed as a value less than 100 and needs to be computed differently
            if (effect_id == (int)EQSpellEffectEnum.AttackSpeed && base_abs < 100)
            {
                final_value = base_abs - formula_value;
                if (max_value != 0)
                {
                    if (final_value < max_value)
                    {
                        final_value = max_value;
                    }
                }
            }

            // clamp to max
            if (max_value != 0)
            {
                int max_abs = Math.Abs(max_value);
                if (final_value > max_abs)
                    final_value = max_abs;
            }

            // adjust sign
            if (base_value < 0)
                final_value = -final_value;

            return final_value;
        }

        public int GetProcSpellID(int effect_slot, bool shadowknight)
        {
            return GetProcSpellID(this, effect_slot, shadowknight);
        }

        public static int GetProcSpellID(EQSpell spell, int effect_slot, bool shadowknight)
        {
            int proc_spell_id = spell.base1[effect_slot];

            if (shadowknight)
                return proc_spell_id + 1;

            return proc_spell_id;
        }
    }
}
