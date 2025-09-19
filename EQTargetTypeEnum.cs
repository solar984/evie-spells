using System;
using static Evie.EQTargetTypeEnum;

namespace Evie
{
    public class EQTargetType
    {
        public static string GetName(int val)
        {
            EQTargetTypeEnum targetType = (EQTargetTypeEnum)val;

            switch (targetType)
            {
                case ST_TargetOptional_1:
                    return "TargetOptional";
                case ST_AEClientV1_2:
                    return "AEClientV1";
                case ST_GroupTeleport_3:
                    return "GroupTeleport";
                case ST_AECaster_4:
                    return "AECaster";
                case ST_Target_5:
                    return "Target";
                case ST_Self_6:
                    return "Self";
                case ST_UNK_7:
                    return "UNK_7";
                case ST_AETarget_8:
                    return "AETarget";
                case ST_Animal_9:
                    return "Animal";
                case ST_Undead_10:
                    return "Undead";
                case ST_Summoned_11:
                    return "Summoned";
                case ST_Flying_Unused_12:
                    return "Flying";
                case ST_Tap_13:
                    return "Tap";
                case ST_Pet_14:
                    return "Pet";
                case ST_Corpse_15:
                    return "Corpse";
                case ST_Plant_16:
                    return "Plant";
                case ST_Giant_17:
                    return "Giant";
                case ST_Dragon_18:
                    return "Dragon";
                case ST_Coldain_Unused_19:
                    return "Coldain";
                case ST_TargetAETap_20:
                    return "TargetAETap";
                case ST_UNK_21:
                    return "UNK_21";
                case ST_UNK_22:
                    return "UNK_22";
                case ST_UNK_23:
                    return "UNK_23";
                case ST_UndeadAE_24:
                    return "UndeadAE";
                case ST_SummonedAE_25:
                    return "SummonedAE";
                case ST_UNK_26:
                    return "UNK_26";
                case ST_Insect1_27:
                    return "Insect1";
                case ST_Insect2_28:
                    return "Insect2";
                case ST_UNK_29:
                    return "UNK_29";
                case ST_UNK_30:
                    return "UNK_30";
                case ST_UNK_31:
                    return "UNK_31";
                case ST_AETargetHateList_32:
                    return "AETargetHateList";
                case ST_HateList_33:
                    return "HateList";
                case ST_LDoNChest_Cursed_34:
                    return "LDONChest_Cursed";
                case ST_Muramite_35:
                    return "Muramite";
                case ST_AreaClientOnly_36:
                    return "AreaClientOnly";
                case ST_AreaNPCOnly_37:
                    return "AreaNPCOnly";
                case ST_SummonedPet_38:
                    return "SummonedPet";
                case ST_GroupNoPets_39:
                    return "GroupNoPets";
                case ST_AEBard_40:
                    return "AEBard";
                case ST_Group_41:
                    return "Group";
                case ST_Directional_42:
                    return "Directional";
                case ST_GroupClientAndPet_43:
                    return "GroupClientAndPet";
                case ST_Beam_44:
                    return "Beam";
                case ST_Ring_45:
                    return "Ring";
                case ST_TargetsTarget_46:
                    return "TargetsTarget";
                case ST_PetMaster_47:
                    return "PetMaster";
                case ST_UNK_48:
                    return "UNK_48";
                case ST_UNK_49:
                    return "UNK_49";
                case ST_TargetAENoPlayersPets_50:
                    return "TargetAENoPlayersPets";
            }

            return String.Format("Unknown{0}", (int)targetType);
        }

        public static bool NeedsTargetInRange(int targetType)
        {
            // titanium client target type list
            switch (targetType)
            {
                // 0 and 1 are 'target optional' but if they have a target it has to be range checked
                case 0:
                case (int)ST_TargetOptional_1:

                case (int)ST_Target_5:
                case (int)ST_AETarget_8:
                case (int)ST_Animal_9:
                case (int)ST_Undead_10:
                case (int)ST_Summoned_11:
                case (int)ST_Flying_Unused_12: // not used in the spell file
                case (int)ST_Tap_13:
                case (int)ST_Pet_14: // auto targets pet
                case (int)ST_Corpse_15:
                case (int)ST_Plant_16:
                case (int)ST_Giant_17:
                case (int)ST_Dragon_18:
                case (int)ST_TargetAETap_20:
                case (int)ST_UNK_21: // not used in the spell file
                case (int)ST_UNK_22: // not used in the spell file
                case (int)ST_UNK_23: // not used in the spell file
                case (int)ST_Insect1_27: // not used in the spell file
                case (int)ST_Insect2_28: // not used in the spell file
                case (int)ST_LDoNChest_Cursed_34:
                case (int)ST_Muramite_35:
                case (int)ST_SummonedPet_38: // auto targets pet
                case (int)ST_GroupNoPets_39:
                case (int)ST_GroupClientAndPet_43:

                    return true;
            }

            return false;
        }

        public static bool IsAreaEffectTargetType(int targetType)
        {
            switch (targetType)
            {
                case (int)ST_AEClientV1_2:
                case (int)ST_GroupTeleport_3:
                case (int)ST_AECaster_4:
                case (int)ST_AETarget_8:
                case (int)ST_TargetAETap_20:
                case (int)ST_UndeadAE_24:
                case (int)ST_SummonedAE_25:
                case (int)ST_AETargetHateList_32:
                case (int)ST_HateList_33:
                case (int)ST_AreaClientOnly_36:
                case (int)ST_AreaNPCOnly_37:
                case (int)ST_AEBard_40:
                case (int)ST_Group_41:
                case (int)ST_Directional_42:
                    return true;
            }

            return false;
        }
    }

    public enum EQTargetTypeEnum : int
    {
        /* 01 */
        ST_TargetOptional_1 = 0x01, //only used for targeted projectile spells
        /* 02 */
        ST_AEClientV1_2 = 0x02,
        /* 03 */
        ST_GroupTeleport_3 = 0x03,
        /* 04 */
        ST_AECaster_4 = 0x04,
        /* 05 */
        ST_Target_5 = 0x05,
        /* 06 */
        ST_Self_6 = 0x06,
        /* 07 */
        ST_UNK_7 = 7,// NOT USED
        /* 08 */
        ST_AETarget_8 = 0x08,
        /* 09 */
        ST_Animal_9 = 0x09,
        /* 10 */
        ST_Undead_10 = 0x0a,
        /* 11 */
        ST_Summoned_11 = 0x0b,
        /* 12 */
        ST_Flying_Unused_12 = 12, // NOT USED error is 218 (This spell only works on things that are flying.)
        /* 13 */
        ST_Tap_13 = 0x0d,
        /* 14 */
        ST_Pet_14 = 0x0e,
        /* 15 */
        ST_Corpse_15 = 0x0f,
        /* 16 */
        ST_Plant_16 = 0x10,
        /* 17 */
        ST_Giant_17 = 0x11, //special giant
        /* 18 */
        ST_Dragon_18 = 0x12, //special dragon
        /* 19 */
        ST_Coldain_Unused_19 = 19, // NOT USED error is 227 (This spell only works on specific coldain.)
        /* 20 */
        ST_TargetAETap_20 = 0x14,
        /* 21 */
        ST_UNK_21 = 21, // NOT USED same switch case as ST_Undead
        /* 22 */
        ST_UNK_22 = 22, // NOT USED same switch case as ST_Summoned
        /* 23 */
        ST_UNK_23 = 23, // NOT USED same switch case as ST_Animal
        /* 24 */
        ST_UndeadAE_24 = 0x18,
        /* 25 */
        ST_SummonedAE_25 = 0x19,
        /* 26 */
        ST_UNK_26 = 26, // NOT USED
        /* 27 */
        ST_Insect1_27 = 27, // NOT USED error is 223 (This spell only works on insects.)
        /* 28 */
        ST_Insect2_28 = 28, // NOT USED error is 223 (This spell only works on insects.)
        /* 29 */
        ST_UNK_29 = 29, // NOT USED
        /* 30 */
        ST_UNK_30 = 30, // NOT USED
        /* 31 */
        ST_UNK_31 = 31, // NOT USED
        /* 32 */
        ST_AETargetHateList_32 = 0x20,
        /* 33 */
        ST_HateList_33 = 0x21,
        /* 34 */
        ST_LDoNChest_Cursed_34 = 0x22,
        /* 35 */
        ST_Muramite_35 = 0x23, //only works on special muramites
        /* 36 */
        ST_AreaClientOnly_36 = 0x24,
        /* 37 */
        ST_AreaNPCOnly_37 = 0x25,
        /* 38 */
        ST_SummonedPet_38 = 0x26,
        /* 39 */
        ST_GroupNoPets_39 = 0x27,
        /* 40 */
        ST_AEBard_40 = 0x28,
        /* 41 */
        ST_Group_41 = 0x29,
        /* 42 */
        ST_Directional_42 = 0x2a, //ae around this target between two angles
        /* 43 */
        ST_GroupClientAndPet_43 = 0x2b,

        // post titanium below here

        /* 44 */
        ST_Beam_44 = 0x2c,
        /* 45 */
        ST_Ring_45 = 0x2d,
        /* 46 */
        ST_TargetsTarget_46 = 0x2e, // uses the target of your target
        /* 47 */
        ST_PetMaster_47 = 0x2f, // uses the master as target
        /* 48 */
        ST_UNK_48 = 48, // UNKNOWN
        /* 49 */
        ST_UNK_49 = 49,// NOT USED
        /* 50 */
        ST_TargetAENoPlayersPets_50 = 0x32,
    }
}
