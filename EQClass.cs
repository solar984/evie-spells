namespace Evie
{
    public class EQClass
    {
        public int Number { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }

        public static readonly EQClass[] Classes =
        {
            new EQClass() { Number = 1, LongName = "Warrior", ShortName = "WAR" },
            new EQClass() { Number = 2, LongName = "Cleric", ShortName = "CLR" },
            new EQClass() { Number = 3, LongName = "Paladin", ShortName = "PAL" },
            new EQClass() { Number = 4, LongName = "Ranger", ShortName = "RNG" },
            new EQClass() { Number = 5, LongName = "ShadowKnight", ShortName = "SHD" },
            new EQClass() { Number = 6, LongName = "Druid", ShortName = "DRU" },
            new EQClass() { Number = 7, LongName = "Monk", ShortName = "MNK" },
            new EQClass() { Number = 8, LongName = "Bard", ShortName = "BRD" },
            new EQClass() { Number = 9, LongName = "Rogue", ShortName = "ROG" },
            new EQClass() { Number = 10, LongName = "Shaman", ShortName = "SHM" },
            new EQClass() { Number = 11, LongName = "Necromancer", ShortName = "NEC" },
            new EQClass() { Number = 12, LongName = "Wizard", ShortName = "WIZ" },
            new EQClass() { Number = 13, LongName = "Magician", ShortName = "MAG" },
            new EQClass() { Number = 14, LongName = "Enchanter", ShortName = "ENC" },
            new EQClass() { Number = 15, LongName = "Beastlord", ShortName = "BST" },
            new EQClass() { Number = 16, LongName = "Berserker", ShortName = "BER" },
        };
    }
}
