using System;
using System.Collections.Generic;
using System.Linq;

namespace Evie
{
    public class ExtraNotes
    {
        public static ExtraNotes[] GetNotes(EQSpell spell)
        {
            // notes can be attached to various elements of the spell so they display on every page where it's appropriate
            List<ExtraNotes> list;
            list = Notes.Where(n => n.Predicate(spell)).ToList();
            return list.ToArray();
        }

        public Predicate<EQSpell> Predicate { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public static List<ExtraNotes> Notes;
        static ExtraNotes()
        {
            Notes = new List<ExtraNotes>
            ([
                new ExtraNotes()
                {
                    Name = "Stun",
                    Predicate = (s) =>
                    {
                        int ix = s.SpellEffectIndex((int)EQSpellEffectEnum.Stun);
                        if(ix >= 0)
                        {
                            if(s.base1[ix] < 1000)
                                return true;
                        }
                        return false;
                    },
                    Content = "NPCs cannot be stunned for less than a second.  They will still be interrupted but the stun duration is 0."
                },
                new ExtraNotes()
                {
                    Name = "Add Proc",
                    Predicate = s => s.IsEffectInSpell((int)EQSpellEffectEnum.WeaponProc),
                    Content = "Effect 85 adds a weapon proc.  The spell ID of the proc spell is stored in base1 but when the effect is applied to a ShadowKnight the spell ID is incremented by 1.  This is a hack that seems to have been put in for <a href=\"359.html\">Vampiric Embrace</a> and created technical debt for the EverQuest designers such that they had to create a second copy of every proc buff.  They sometimes didn't do this if the spell is never supposed to be used by a ShadowKnight, and they also didn't create non ShadowKnight versions of some things that are only ever used by ShadowKnights.  This site displays both spells but sometimes one or the other is never going to be used in game."
                },
                new ExtraNotes()
                {
                    Name = "Complete Heal",
                    Predicate = s => s.IsEffectInSpell((int)EQSpellEffectEnum.CompleteHeal),
                    Content = "This effect is used on the item Donal's Chestplate of Mourning.  It is a beneficial buff but it cannot be clicked off and prevents a second application of the same buff until it expires.  It also provides one point of mana regeneration that stacks with everything and works on bards too."
                },
                new ExtraNotes()
                {
                    Name = "Haste",
                    Predicate = s => new[] { (int)EQSpellEffectEnum.AttackSpeed,  (int)EQSpellEffectEnum.AttackSpeed2,  (int)EQSpellEffectEnum.AttackSpeed3 }.Any(e=>s.IsEffectInSpell(e)),
                    Content = "Haste provided by the effects Haste v1(11), Haste v2(98) and Haste v3(119) adds together.  Haste v1 and Haste v2 have a cap of 100%, but Haste v3 (overhaste) can push this value up to 125%.  If more than one buff with the same haste effect is present, the highest is used unless a slow is present in which case the slow takes priority and haste is not added."
                },
                new ExtraNotes()
                {
                    Name = "Recourse",
                    Predicate = s => EQSpell.ConvertToInt32(s.RecourseLink) != 0,
                    Content = "Recourse is a second spell that is cast on the caster of the first spell when it is applied."
                },
                new ExtraNotes()
                {
                    Name = "AEDuration",
                    Predicate = s => s.IsAEDurationSpell(),
                    Content = "Area effect spells (target type AECaster(4) and AETarget(8)) that have AEDuration repeat the spell every 2.5 sec at the initial location until the AEDuration expires.  Players can move away from the location to avoid the extra waves."
                },
                new ExtraNotes()
                {
                    Name = "Buff",
                    Predicate = s=> EQSpell.ConvertToInt32(s.buffdurationformula) != 0,
                    Content = "Buff spell durations are measured in ticks.  The duration is decremented every 6 seconds but due to how this is implemented in the game, the first and last ticks can end up being shorter than 6 seconds.  Spells that have a periodic effect will have duration + 1 effect applications before expiring."
                }
            ]);
        }
    }
}
