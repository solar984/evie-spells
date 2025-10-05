using System;
using static Evie.EQSkillEnum;

namespace Evie
{
    public class EQSkill
    {
        public static string GetName(int val)
        {
            EQSkillEnum skill = (EQSkillEnum)val;

            switch (skill)
            {
                case Skill1HBlunt: return "1H Blunt";
                case Skill1HSlashing: return "1H Slashing";
                case Skill2HBlunt: return "2H Blunt";
                case Skill2HSlashing: return "2H Slashing";
                case SkillAbjuration: return "Abjuration";
                case SkillAlteration: return "Alteration";
                case SkillApplyPoison: return "Apply Poison";
                case SkillArchery: return "Archery";
                case SkillBackstab: return "Backstab";
                case SkillBindWound: return "Bind Wound";
                case SkillBash: return "Bash";
                case SkillBlock: return "Block";
                case SkillBrassInstruments: return "Brass Instruments";
                case SkillChanneling: return "Channeling";
                case SkillConjuration: return "Conjuration";
                case SkillDefense: return "Defense";
                case SkillDisarm: return "Disarm";
                case SkillDisarmTraps: return "Disarm Traps";
                case SkillDivination: return "Divination";
                case SkillDodge: return "Dodge";
                case SkillDoubleAttack: return "Double Attack";
                case SkillDragonPunch: return "Dragon Punch/Tail Rake";
                case SkillDualWield: return "Dual Wield";
                case SkillEagleStrike: return "Eagle Strike";
                case SkillEvocation: return "Evocation";
                case SkillFeignDeath: return "Feign Death";
                case SkillFlyingKick: return "Flying Kick";
                case SkillForage: return "Forage";
                case SkillHandtoHand: return "Hand to Hand";
                case SkillHide: return "Hide";
                case SkillKick: return "Kick";
                case SkillMeditate: return "Meditate";
                case SkillMend: return "Mend";
                case SkillOffense: return "Offense";
                case SkillParry: return "Parry";
                case SkillPickLock: return "Pick Lock";
                case Skill1HPiercing: return "1H Piercing";
                case SkillRiposte: return "Riposte";
                case SkillRoundKick: return "Round Kick";
                case SkillSafeFall: return "Safe Fall";
                case SkillSenseHeading: return "Sense Heading";
                case SkillSinging: return "Singing";
                case SkillSneak: return "Sneak";
                case SkillSpecializeAbjure: return "Specialize Abjuration";
                case SkillSpecializeAlteration: return "Specialize Alteration";
                case SkillSpecializeConjuration: return "Specialize Conjuration";
                case SkillSpecializeDivination: return "Specialize Divination";
                case SkillSpecializeEvocation: return "Specialize Evocation";
                case SkillPickPockets: return "Pick Pockets";
                case SkillStringedInstruments: return "Stringed Instruments";
                case SkillSwimming: return "Swimming";
                case SkillThrowing: return "Throwing";
                case SkillTigerClaw: return "Tiger Claw";
                case SkillTracking: return "Tracking";
                case SkillWindInstruments: return "Wind Instruments";
                case SkillFishing: return "Fishing";
                case SkillMakePoison: return "Make Poison";
                case SkillTinkering: return "Tinkering";
                case SkillResearch: return "Research";
                case SkillAlchemy: return "Alchemy";
                case SkillBaking: return "Baking";
                case SkillTailoring: return "Tailoring";
                case SkillSenseTraps: return "Sense Traps";
                case SkillBlacksmithing: return "Blacksmithing";
                case SkillFletching: return "Fletching";
                case SkillBrewing: return "Brewing";
                case SkillAlcoholTolerance: return "Alcohol Tolerance";
                case SkillBegging: return "Begging";
                case SkillJewelryMaking: return "Jewelry Making";
                case SkillPottery: return "Pottery";
                case SkillPercussionInstruments: return "Percussion Instruments";
                case SkillIntimidation: return "Intimidation";
                case SkillBerserking: return "Berserking";
                case SkillTaunt: return "Taunt";
                case SkillFrenzy: return "Frenzy";
            }

            return String.Format("Unknown{0}", val);
        }
    }

    public enum EQSkillEnum
    {
        /*13855*/
        Skill1HBlunt = 0,
        /*13856*/
        Skill1HSlashing,
        /*13857*/
        Skill2HBlunt,
        /*13858*/
        Skill2HSlashing,
        /*13859*/
        SkillAbjuration,
        /*13861*/
        SkillAlteration, // 5
        /*13862*/
        SkillApplyPoison,
        /*13863*/
        SkillArchery,
        /*13864*/
        SkillBackstab,
        /*13866*/
        SkillBindWound,
        /*13867*/
        SkillBash, // 10
        /*13871*/
        SkillBlock,
        /*13872*/
        SkillBrassInstruments,
        /*13874*/
        SkillChanneling,
        /*13875*/
        SkillConjuration,
        /*13876*/
        SkillDefense, // 15
        /*13877*/
        SkillDisarm,
        /*13878*/
        SkillDisarmTraps,
        /*13879*/
        SkillDivination,
        /*13880*/
        SkillDodge,
        /*13881*/
        SkillDoubleAttack, // 20
        /*13882*/
        SkillDragonPunch,
        /*13924*/
        SkillTailRake = SkillDragonPunch, // Iksar Monk equivilent
        /*13883*/
        SkillDualWield,
        /*13884*/
        SkillEagleStrike,
        /*13885*/
        SkillEvocation,
        /*13886*/
        SkillFeignDeath, // 25
        /*13888*/
        SkillFlyingKick,
        /*13889*/
        SkillForage,
        /*13890*/
        SkillHandtoHand,
        /*13891*/
        SkillHide,
        /*13893*/
        SkillKick, // 30
        /*13894*/
        SkillMeditate,
        /*13895*/
        SkillMend,
        /*13896*/
        SkillOffense,
        /*13897*/
        SkillParry,
        /*13899*/
        SkillPickLock, // 35
        /*13900*/
        Skill1HPiercing,                // Changed in RoF2(05-10-2013)
        /*13903*/
        SkillRiposte,
        /*13904*/
        SkillRoundKick,
        /*13905*/
        SkillSafeFall,
        /*13906*/
        SkillSenseHeading, // 40
        /*13908*/
        SkillSinging,
        /*13909*/
        SkillSneak,
        /*13910*/
        SkillSpecializeAbjure,          // No idea why they truncated this one..especially when there are longer ones...
        /*13911*/
        SkillSpecializeAlteration,
        /*13912*/
        SkillSpecializeConjuration, // 45
        /*13913*/
        SkillSpecializeDivination,
        /*13914*/
        SkillSpecializeEvocation,
        /*13915*/
        SkillPickPockets,
        /*13916*/
        SkillStringedInstruments,
        /*13917*/
        SkillSwimming, // 50
        /*13919*/
        SkillThrowing,
        /*13920*/
        SkillTigerClaw,
        /*13921*/
        SkillTracking,
        /*13923*/
        SkillWindInstruments,
        /*13854*/
        SkillFishing, // 55
        /*13853*/
        SkillMakePoison,
        /*13852*/
        SkillTinkering,
        /*13851*/
        SkillResearch,
        /*13850*/
        SkillAlchemy,
        /*13865*/
        SkillBaking, // 60
        /*13918*/
        SkillTailoring,
        /*13907*/
        SkillSenseTraps,
        /*13870*/
        SkillBlacksmithing,
        /*13887*/
        SkillFletching,
        /*13873*/
        SkillBrewing, // 65
        /*13860*/
        SkillAlcoholTolerance,
        /*13868*/
        SkillBegging,
        /*13892*/
        SkillJewelryMaking,
        /*13901*/
        SkillPottery,
        /*13898*/
        SkillPercussionInstruments, // 70
        /*13922*/
        SkillIntimidation,
        /*13869*/
        SkillBerserking,
        /*13902*/
        SkillTaunt,
        /*05837*/
        SkillFrenzy, // 74		
    }
}
