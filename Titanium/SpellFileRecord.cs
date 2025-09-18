using System.ComponentModel.DataAnnotations.Schema;

namespace Evie.Titanium
{
    public class SpellFileRecord
    {
        // 0 id
        [Column("id", Order = 0)]
        public string id { get; set; }

        // 1 name
        [Column("name", Order = 1)]
        public string name { get; set; }

        // 2 player_1
        [Column("player_1", Order = 2)]
        public string player_1 { get; set; }

        // 3 teleport_zone
        [Column("teleport_zone", Order = 3)]
        public string teleport_zone { get; set; }

        // 4 you_cast
        [Column("you_cast", Order = 4)]
        public string you_cast { get; set; }

        // 5 other_casts
        [Column("other_casts", Order = 5)]
        public string other_casts { get; set; }

        // 6 cast_on_you
        [Column("cast_on_you", Order = 6)]
        public string cast_on_you { get; set; }

        // 7 cast_on_other
        [Column("cast_on_other", Order = 7)]
        public string cast_on_other { get; set; }

        // 8 spell_fades
        [Column("spell_fades", Order = 8)]
        public string spell_fades { get; set; }

        // 9 range
        [Column("range", Order = 9)]
        public string range { get; set; }

        // 10 aoerange
        [Column("aoerange", Order = 10)]
        public string aoerange { get; set; }

        // 11 pushback
        [Column("pushback", Order = 11)]
        public string pushback { get; set; }

        // 12 pushup
        [Column("pushup", Order = 12)]
        public string pushup { get; set; }

        // 13 cast_time
        [Column("cast_time", Order = 13)]
        public string cast_time { get; set; }

        // 14 recovery_time
        [Column("recovery_time", Order = 14)]
        public string recovery_time { get; set; }

        // 15 recast_time
        [Column("recast_time", Order = 15)]
        public string recast_time { get; set; }

        // 16 buffdurationformula
        [Column("buffdurationformula", Order = 16)]
        public string buffdurationformula { get; set; }

        // 17 buffduration
        [Column("buffduration", Order = 17)]
        public string buffduration { get; set; }

        // 18 AEDuration
        [Column("AEDuration", Order = 18)]
        public string AEDuration { get; set; }

        // 19 mana
        [Column("mana", Order = 19)]
        public string mana { get; set; }

        // 20 effect_base_value1
        [Column("effect_base_value1", Order = 20)]
        public string effect_base_value1 { get; set; }

        // 21 effect_base_value2
        [Column("effect_base_value2", Order = 21)]
        public string effect_base_value2 { get; set; }

        // 22 effect_base_value3
        [Column("effect_base_value3", Order = 22)]
        public string effect_base_value3 { get; set; }

        // 23 effect_base_value4
        [Column("effect_base_value4", Order = 23)]
        public string effect_base_value4 { get; set; }

        // 24 effect_base_value5
        [Column("effect_base_value5", Order = 24)]
        public string effect_base_value5 { get; set; }

        // 25 effect_base_value6
        [Column("effect_base_value6", Order = 25)]
        public string effect_base_value6 { get; set; }

        // 26 effect_base_value7
        [Column("effect_base_value7", Order = 26)]
        public string effect_base_value7 { get; set; }

        // 27 effect_base_value8
        [Column("effect_base_value8", Order = 27)]
        public string effect_base_value8 { get; set; }

        // 28 effect_base_value9
        [Column("effect_base_value9", Order = 28)]
        public string effect_base_value9 { get; set; }

        // 29 effect_base_value10
        [Column("effect_base_value10", Order = 29)]
        public string effect_base_value10 { get; set; }

        // 30 effect_base_value11
        [Column("effect_base_value11", Order = 30)]
        public string effect_base_value11 { get; set; }

        // 31 effect_base_value12
        [Column("effect_base_value12", Order = 31)]
        public string effect_base_value12 { get; set; }

        // 32 effect_limit_value1
        [Column("effect_limit_value1", Order = 32)]
        public string effect_limit_value1 { get; set; }

        // 33 effect_limit_value2
        [Column("effect_limit_value2", Order = 33)]
        public string effect_limit_value2 { get; set; }

        // 34 effect_limit_value3
        [Column("effect_limit_value3", Order = 34)]
        public string effect_limit_value3 { get; set; }

        // 35 effect_limit_value4
        [Column("effect_limit_value4", Order = 35)]
        public string effect_limit_value4 { get; set; }

        // 36 effect_limit_value5
        [Column("effect_limit_value5", Order = 36)]
        public string effect_limit_value5 { get; set; }

        // 37 effect_limit_value6
        [Column("effect_limit_value6", Order = 37)]
        public string effect_limit_value6 { get; set; }

        // 38 effect_limit_value7
        [Column("effect_limit_value7", Order = 38)]
        public string effect_limit_value7 { get; set; }

        // 39 effect_limit_value8
        [Column("effect_limit_value8", Order = 39)]
        public string effect_limit_value8 { get; set; }

        // 40 effect_limit_value9
        [Column("effect_limit_value9", Order = 40)]
        public string effect_limit_value9 { get; set; }

        // 41 effect_limit_value10
        [Column("effect_limit_value10", Order = 41)]
        public string effect_limit_value10 { get; set; }

        // 42 effect_limit_value11
        [Column("effect_limit_value11", Order = 42)]
        public string effect_limit_value11 { get; set; }

        // 43 effect_limit_value12
        [Column("effect_limit_value12", Order = 43)]
        public string effect_limit_value12 { get; set; }

        // 44 max1
        [Column("max1", Order = 44)]
        public string max1 { get; set; }

        // 45 max2
        [Column("max2", Order = 45)]
        public string max2 { get; set; }

        // 46 max3
        [Column("max3", Order = 46)]
        public string max3 { get; set; }

        // 47 max4
        [Column("max4", Order = 47)]
        public string max4 { get; set; }

        // 48 max5
        [Column("max5", Order = 48)]
        public string max5 { get; set; }

        // 49 max6
        [Column("max6", Order = 49)]
        public string max6 { get; set; }

        // 50 max7
        [Column("max7", Order = 50)]
        public string max7 { get; set; }

        // 51 max8
        [Column("max8", Order = 51)]
        public string max8 { get; set; }

        // 52 max9
        [Column("max9", Order = 52)]
        public string max9 { get; set; }

        // 53 max10
        [Column("max10", Order = 53)]
        public string max10 { get; set; }

        // 54 max11
        [Column("max11", Order = 54)]
        public string max11 { get; set; }

        // 55 max12
        [Column("max12", Order = 55)]
        public string max12 { get; set; }

        // 56 icon
        [Column("icon", Order = 56)]
        public string icon { get; set; }

        // 57 memicon
        [Column("memicon", Order = 57)]
        public string memicon { get; set; }

        // 58 components1
        [Column("components1", Order = 58)]
        public string components1 { get; set; }

        // 59 components2
        [Column("components2", Order = 59)]
        public string components2 { get; set; }

        // 60 components3
        [Column("components3", Order = 60)]
        public string components3 { get; set; }

        // 61 components4
        [Column("components4", Order = 61)]
        public string components4 { get; set; }

        // 62 component_counts1
        [Column("component_counts1", Order = 62)]
        public string component_counts1 { get; set; }

        // 63 component_counts2
        [Column("component_counts2", Order = 63)]
        public string component_counts2 { get; set; }

        // 64 component_counts3
        [Column("component_counts3", Order = 64)]
        public string component_counts3 { get; set; }

        // 65 component_counts4
        [Column("component_counts4", Order = 65)]
        public string component_counts4 { get; set; }

        // 66 NoexpendReagent1
        [Column("NoexpendReagent1", Order = 66)]
        public string NoexpendReagent1 { get; set; }

        // 67 NoexpendReagent2
        [Column("NoexpendReagent2", Order = 67)]
        public string NoexpendReagent2 { get; set; }

        // 68 NoexpendReagent3
        [Column("NoexpendReagent3", Order = 68)]
        public string NoexpendReagent3 { get; set; }

        // 69 NoexpendReagent4
        [Column("NoexpendReagent4", Order = 69)]
        public string NoexpendReagent4 { get; set; }

        // 70 formula1
        [Column("formula1", Order = 70)]
        public string formula1 { get; set; }

        // 71 formula2
        [Column("formula2", Order = 71)]
        public string formula2 { get; set; }

        // 72 formula3
        [Column("formula3", Order = 72)]
        public string formula3 { get; set; }

        // 73 formula4
        [Column("formula4", Order = 73)]
        public string formula4 { get; set; }

        // 74 formula5
        [Column("formula5", Order = 74)]
        public string formula5 { get; set; }

        // 75 formula6
        [Column("formula6", Order = 75)]
        public string formula6 { get; set; }

        // 76 formula7
        [Column("formula7", Order = 76)]
        public string formula7 { get; set; }

        // 77 formula8
        [Column("formula8", Order = 77)]
        public string formula8 { get; set; }

        // 78 formula9
        [Column("formula9", Order = 78)]
        public string formula9 { get; set; }

        // 79 formula10
        [Column("formula10", Order = 79)]
        public string formula10 { get; set; }

        // 80 formula11
        [Column("formula11", Order = 80)]
        public string formula11 { get; set; }

        // 81 formula12
        [Column("formula12", Order = 81)]
        public string formula12 { get; set; }

        // 82 LightType
        [Column("LightType", Order = 82)]
        public string LightType { get; set; }

        // 83 goodEffect
        [Column("goodEffect", Order = 83)]
        public string goodEffect { get; set; }

        // 84 Activated
        [Column("Activated", Order = 84)]
        public string Activated { get; set; }

        // 85 resisttype
        [Column("resisttype", Order = 85)]
        public string resisttype { get; set; }

        // 86 effectid1
        [Column("effectid1", Order = 86)]
        public string effectid1 { get; set; }

        // 87 effectid2
        [Column("effectid2", Order = 87)]
        public string effectid2 { get; set; }

        // 88 effectid3
        [Column("effectid3", Order = 88)]
        public string effectid3 { get; set; }

        // 89 effectid4
        [Column("effectid4", Order = 89)]
        public string effectid4 { get; set; }

        // 90 effectid5
        [Column("effectid5", Order = 90)]
        public string effectid5 { get; set; }

        // 91 effectid6
        [Column("effectid6", Order = 91)]
        public string effectid6 { get; set; }

        // 92 effectid7
        [Column("effectid7", Order = 92)]
        public string effectid7 { get; set; }

        // 93 effectid8
        [Column("effectid8", Order = 93)]
        public string effectid8 { get; set; }

        // 94 effectid9
        [Column("effectid9", Order = 94)]
        public string effectid9 { get; set; }

        // 95 effectid10
        [Column("effectid10", Order = 95)]
        public string effectid10 { get; set; }

        // 96 effectid11
        [Column("effectid11", Order = 96)]
        public string effectid11 { get; set; }

        // 97 effectid12
        [Column("effectid12", Order = 97)]
        public string effectid12 { get; set; }

        // 98 targettype
        [Column("targettype", Order = 98)]
        public string targettype { get; set; }

        // 99 basediff
        [Column("basediff", Order = 99)]
        public string basediff { get; set; }

        // 100 skill
        [Column("skill", Order = 100)]
        public string skill { get; set; }

        // 101 zonetype
        [Column("zonetype", Order = 101)]
        public string zonetype { get; set; }

        // 102 EnvironmentType
        [Column("EnvironmentType", Order = 102)]
        public string EnvironmentType { get; set; }

        // 103 TimeOfDay
        [Column("TimeOfDay", Order = 103)]
        public string TimeOfDay { get; set; }

        // 104 classes1
        [Column("classes1", Order = 104)]
        public string classes1 { get; set; }

        // 105 classes2
        [Column("classes2", Order = 105)]
        public string classes2 { get; set; }

        // 106 classes3
        [Column("classes3", Order = 106)]
        public string classes3 { get; set; }

        // 107 classes4
        [Column("classes4", Order = 107)]
        public string classes4 { get; set; }

        // 108 classes5
        [Column("classes5", Order = 108)]
        public string classes5 { get; set; }

        // 109 classes6
        [Column("classes6", Order = 109)]
        public string classes6 { get; set; }

        // 110 classes7
        [Column("classes7", Order = 110)]
        public string classes7 { get; set; }

        // 111 classes8
        [Column("classes8", Order = 111)]
        public string classes8 { get; set; }

        // 112 classes9
        [Column("classes9", Order = 112)]
        public string classes9 { get; set; }

        // 113 classes10
        [Column("classes10", Order = 113)]
        public string classes10 { get; set; }

        // 114 classes11
        [Column("classes11", Order = 114)]
        public string classes11 { get; set; }

        // 115 classes12
        [Column("classes12", Order = 115)]
        public string classes12 { get; set; }

        // 116 classes13
        [Column("classes13", Order = 116)]
        public string classes13 { get; set; }

        // 117 classes14
        [Column("classes14", Order = 117)]
        public string classes14 { get; set; }

        // 118 classes15
        [Column("classes15", Order = 118)]
        public string classes15 { get; set; }

        // 119 classes16
        [Column("classes16", Order = 119)]
        public string classes16 { get; set; }

        // 120 CastingAnim
        [Column("CastingAnim", Order = 120)]
        public string CastingAnim { get; set; }

        // 121 TargetAnim
        [Column("TargetAnim", Order = 121)]
        public string TargetAnim { get; set; }

        // 122 TravelType
        [Column("TravelType", Order = 122)]
        public string TravelType { get; set; }

        // 123 SpellAffectIndex
        [Column("SpellAffectIndex", Order = 123)]
        public string SpellAffectIndex { get; set; }

        // 124 disallow_sit
        [Column("disallow_sit", Order = 124)]
        public string disallow_sit { get; set; }

        // 125 deities0
        [Column("deities0", Order = 125)]
        public string deities0 { get; set; }

        // 126 deities1
        [Column("deities1", Order = 126)]
        public string deities1 { get; set; }

        // 127 deities2
        [Column("deities2", Order = 127)]
        public string deities2 { get; set; }

        // 128 deities3
        [Column("deities3", Order = 128)]
        public string deities3 { get; set; }

        // 129 deities4
        [Column("deities4", Order = 129)]
        public string deities4 { get; set; }

        // 130 deities5
        [Column("deities5", Order = 130)]
        public string deities5 { get; set; }

        // 131 deities6
        [Column("deities6", Order = 131)]
        public string deities6 { get; set; }

        // 132 deities7
        [Column("deities7", Order = 132)]
        public string deities7 { get; set; }

        // 133 deities8
        [Column("deities8", Order = 133)]
        public string deities8 { get; set; }

        // 134 deities9
        [Column("deities9", Order = 134)]
        public string deities9 { get; set; }

        // 135 deities10
        [Column("deities10", Order = 135)]
        public string deities10 { get; set; }

        // 136 deities11
        [Column("deities11", Order = 136)]
        public string deities11 { get; set; }

        // 137 deities12
        [Column("deities12", Order = 137)]
        public string deities12 { get; set; }

        // 138 deities13
        [Column("deities13", Order = 138)]
        public string deities13 { get; set; }

        // 139 deities14
        [Column("deities14", Order = 139)]
        public string deities14 { get; set; }

        // 140 deities15
        [Column("deities15", Order = 140)]
        public string deities15 { get; set; }

        // 141 deities16
        [Column("deities16", Order = 141)]
        public string deities16 { get; set; }

        // 142 field142
        [Column("field142", Order = 142)]
        public string npc_no_cast { get; set; }

        // 143 field143
        [Column("field143", Order = 143)]
        public string npc_usefulness_not_used { get; set; }

        // 144 new_icon
        [Column("new_icon", Order = 144)]
        public string new_icon { get; set; }

        // 145 spellanim
        [Column("spellanim", Order = 145)]
        public string spellanim { get; set; }

        // 146 uninterruptable
        [Column("uninterruptable", Order = 146)]
        public string uninterruptable { get; set; }

        // 147 ResistDiff
        [Column("ResistDiff", Order = 147)]
        public string ResistDiff { get; set; }

        // 148 dot_stacking_exempt
        [Column("dot_stacking_exempt", Order = 148)]
        public string dot_stacking_exempt { get; set; }

        // 149 deleteable
        [Column("deleteable", Order = 149)]
        public string deleteable { get; set; }

        // 150 RecourseLink
        [Column("RecourseLink", Order = 150)]
        public string RecourseLink { get; set; }

        // 151 no_partial_resist
        [Column("no_partial_resist", Order = 151)]
        public string no_partial_resist { get; set; }

        // 152 field152
        [Column("field152", Order = 152)]
        public string field152 { get; set; }

        // 153 field153
        [Column("field153", Order = 153)]
        public string field153 { get; set; }

        // eqmac fields end here (also no limit/base2 in eqmac)

        // 154 short_buff_box
        [Column("short_buff_box", Order = 154)]
        public string short_buff_box { get; set; }

        // 155 descnum
        [Column("descnum", Order = 155)]
        public string descnum { get; set; }

        // 156 typedescnum
        [Column("typedescnum", Order = 156)]
        public string typedescnum { get; set; }

        // 157 effectdescnum
        [Column("effectdescnum", Order = 157)]
        public string effectdescnum { get; set; }

        // 158 effectdescnum2
        [Column("effectdescnum2", Order = 158)]
        public string effectdescnum2 { get; set; }

        // 159 npc_no_los
        [Column("npc_no_los", Order = 159)]
        public string npc_no_los { get; set; }

        // 160 feedbackable
        [Column("feedbackable", Order = 160)]
        public string feedbackable { get; set; }

        // 161 reflectable
        [Column("reflectable", Order = 161)]
        public string reflectable { get; set; }

        // 162 bonushate
        [Column("bonushate", Order = 162)]
        public string bonushate { get; set; }

        // 163 field163
        [Column("field163", Order = 163)]
        public string field163 { get; set; }

        // 164 field164
        [Column("field164", Order = 164)]
        public string field164 { get; set; }

        // 165 ldon_trap
        [Column("ldon_trap", Order = 165)]
        public string ldon_trap { get; set; }

        // 166 EndurCost
        [Column("EndurCost", Order = 166)]
        public string EndurCost { get; set; }

        // 167 EndurTimerIndex
        [Column("EndurTimerIndex", Order = 167)]
        public string EndurTimerIndex { get; set; }

        // 168 IsDiscipline
        [Column("IsDiscipline", Order = 168)]
        public string IsDiscipline { get; set; }

        // 169, 170, 171, 172 are a set of 4 similar to reagents

        // 169 field169
        [Column("field169", Order = 169)]
        public string field169 { get; set; }

        // 170 field170
        [Column("field170", Order = 170)]
        public string field170 { get; set; }

        // 171 field171
        [Column("field171", Order = 171)]
        public string field171 { get; set; }

        // 172 field172
        [Column("field172", Order = 172)]
        public string field172 { get; set; }

        // 173 HateAdded
        [Column("HateAdded", Order = 173)]
        public string HateAdded { get; set; }

        // 174 EndurUpkeep
        [Column("EndurUpkeep", Order = 174)]
        public string EndurUpkeep { get; set; }

        // 175 numhitstype
        [Column("numhitstype", Order = 175)]
        public string numhitstype { get; set; }

        // 176 numhits
        [Column("numhits", Order = 176)]
        public string numhits { get; set; }

        // 177 pvpresistbase
        [Column("pvpresistbase", Order = 177)]
        public string pvpresistbase { get; set; }

        // 178 pvpresistcalc
        [Column("pvpresistcalc", Order = 178)]
        public string pvpresistcalc { get; set; }

        // 179 pvpresistcap
        [Column("pvpresistcap", Order = 179)]
        public string pvpresistcap { get; set; }

        // 180 spell_category
        [Column("spell_category", Order = 180)]
        public string spell_category { get; set; }

        // 181 pvp_duration
        [Column("pvp_duration", Order = 181)]
        public string pvp_duration { get; set; }

        // 182 pvp_duration_cap
        [Column("pvp_duration_cap", Order = 182)]
        public string pvp_duration_cap { get; set; }

        // 183 pcnpc_only_flag
        [Column("pcnpc_only_flag", Order = 183)]
        public string pcnpc_only_flag { get; set; }

        // 184 cast_not_standing
        [Column("cast_not_standing", Order = 184)]
        public string cast_not_standing { get; set; }

        // 185 can_mgb
        [Column("can_mgb", Order = 185)]
        public string can_mgb { get; set; }

        // 186 nodispell
        [Column("nodispell", Order = 186)]
        public string nodispell { get; set; }

        // 187 npc_category
        [Column("npc_category", Order = 187)]
        public string npc_category { get; set; }

        // 188 npc_usefulness
        [Column("npc_usefulness", Order = 188)]
        public string npc_usefulness { get; set; }

        // 189 MinResist
        [Column("MinResist", Order = 189)]
        public string MinResist { get; set; }

        // 190 MaxResist
        [Column("MaxResist", Order = 190)]
        public string MaxResist { get; set; }

        // 191 viral_targets
        [Column("viral_targets", Order = 191)]
        public string viral_targets { get; set; }

        // 192 viral_timer
        [Column("viral_timer", Order = 192)]
        public string viral_timer { get; set; }

        // 193 nimbuseffect
        [Column("nimbuseffect", Order = 193)]
        public string nimbuseffect { get; set; }

        // 194 ConeStartAngle
        [Column("ConeStartAngle", Order = 194)]
        public string ConeStartAngle { get; set; }

        // 195 ConeStopAngle
        [Column("ConeStopAngle", Order = 195)]
        public string ConeStopAngle { get; set; }

        // 196 sneaking
        [Column("sneaking", Order = 196)]
        public string sneaking { get; set; }

        // 197 not_extendable
        [Column("not_extendable", Order = 197)]
        public string not_extendable { get; set; }

        // 198 no_detrimental_spell_aggro
        [Column("no_detrimental_spell_aggro", Order = 198)]
        public string no_detrimental_spell_aggro { get; set; }

        // 199 field199
        [Column("field199", Order = 199)]
        public string field199 { get; set; }

        // 200 suspendable
        [Column("suspendable", Order = 200)]
        public string suspendable { get; set; }

        // 201 viral_range
        [Column("viral_range", Order = 201)]
        public string viral_range { get; set; }

        // 202 songcap
        [Column("songcap", Order = 202)]
        public string songcap { get; set; }

        // titanium fields end here 

#if false
        // 203 field203
        [Column("field203", Order = 203)]
        public string field203 { get; set; }

        // 204 field204
        [Column("field204", Order = 204)]
        public string field204 { get; set; }

        // 205 no_block
        [Column("no_block", Order = 205)]
        public string no_block { get; set; }

        // 206 field206
        [Column("field206", Order = 206)]
        public string field206 { get; set; }

        // 207 spellgroup
        [Column("spellgroup", Order = 207)]
        public string spellgroup { get; set; }

        // 208 rank
        [Column("rank", Order = 208)]
        public string rank { get; set; }

        // 209 no_resist
        [Column("no_resist", Order = 209)]
        public string no_resist { get; set; }

        // 210 field210
        [Column("field210", Order = 210)]
        public string field210 { get; set; }

        // 211 CastRestriction
        [Column("CastRestriction", Order = 211)]
        public string CastRestriction { get; set; }

        // 212 allowrest
        [Column("allowrest", Order = 212)]
        public string allowrest { get; set; }

        // 213 InCombat
        [Column("InCombat", Order = 213)]
        public string InCombat { get; set; }

        // 214 OutofCombat
        [Column("OutofCombat", Order = 214)]
        public string OutofCombat { get; set; }

        // 215 field215
        [Column("field215", Order = 215)]
        public string field215 { get; set; }

        // 216 field216
        [Column("field216", Order = 216)]
        public string field216 { get; set; }

        // 217 override_crit_chance
        [Column("override_crit_chance", Order = 217)]
        public string override_crit_chance { get; set; }

        // 218 aemaxtargets
        [Column("aemaxtargets", Order = 218)]
        public string aemaxtargets { get; set; }

        // 219 maxtargets
        [Column("maxtargets", Order = 219)]
        public string maxtargets { get; set; }

        // 220 no_heal_damage_item_mod
        [Column("no_heal_damage_item_mod", Order = 220)]
        public string no_heal_damage_item_mod { get; set; }

        // 221 caster_requirement_id
        [Column("caster_requirement_id", Order = 221)]
        public string caster_requirement_id { get; set; }

        // 222 spell_class
        [Column("spell_class", Order = 222)]
        public string spell_class { get; set; }

        // 223 spell_subclass
        [Column("spell_subclass", Order = 223)]
        public string spell_subclass { get; set; }

        // 224 persistdeath
        [Column("persistdeath", Order = 224)]
        public string persistdeath { get; set; }

        // 225 field225
        [Column("field225", Order = 225)]
        public string field225 { get; set; }

        // 226 field226
        [Column("field226", Order = 226)]
        public string field226 { get; set; }

        // 227 min_dist
        [Column("min_dist", Order = 227)]
        public string min_dist { get; set; }

        // 228 min_dist_mod
        [Column("min_dist_mod", Order = 228)]
        public string min_dist_mod { get; set; }

        // 229 max_dist
        [Column("max_dist", Order = 229)]
        public string max_dist { get; set; }

        // 230 max_dist_mod
        [Column("max_dist_mod", Order = 230)]
        public string max_dist_mod { get; set; }

        // 231 min_range
        [Column("min_range", Order = 231)]
        public string min_range { get; set; }

        // 232 no_remove
        [Column("no_remove", Order = 232)]
        public string no_remove { get; set; }

        // 233 field233
        [Column("field233", Order = 233)]
        public string field233 { get; set; }

        // 234 field234
        [Column("field234", Order = 234)]
        public string field234 { get; set; }

        // 235 field235
        [Column("field235", Order = 235)]
        public string field235 { get; set; }

        // 236 field236
        [Column("field236", Order = 236)]
        public string field236 { get; set; }
#endif

        public override System.String ToString()
        {
            return DebugUtil.ObjectToString(this);
        }
    }
}
