using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Evie
{
    public class EQSpellEffectsOld
    {
        public static unsafe SpellEffectOld[] ReadFile(string fileName)
        {
            byte* pBase = null;
            using var mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Open, "OLDEFFMAP", 0, MemoryMappedFileAccess.Read);
            using var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
            accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pBase);

            try
            {
                long recordSize = Marshal.SizeOf<SpellEffectOld>();
                long recordCount = accessor.Capacity / recordSize;
                SpellEffectOld[] effects = new SpellEffectOld[recordCount];

                for (int i = 0; i < recordCount; i++)
                {
                    byte* recPtr = pBase + (i * recordSize);
                    effects[i] = Marshal.PtrToStructure<SpellEffectOld>((nint)recPtr);
                }

                return effects;
            }
            finally
            {
                accessor.SafeMemoryMappedViewHandle.ReleasePointer();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EQRGB
    {
        public byte Red;
        public byte Green;
        public byte Blue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct FixedString32
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StageType
    {
        // 3 strings of length 32
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public FixedString32[] BlitSprite;

        // 1 string of length 32
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string AttachTag;

        // 3 ints
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] DAGnum;

        // 3 ints
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] pcloud;

        // 12 strings of length 32
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public FixedString32[] SpriteTAG;

        public int SpriteEffect;
        public int SoundNum;

        // 3 uints
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Tint;

        // 3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Gravity;

        // 3×3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public float[] NormalXYZ;

        // 3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Radius;

        // 3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Angle;

        // 3 uints
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Lifespan;

        // 3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Velocity;

        // 3 uints
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Rate;

        // 3 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Scale;

        // 12 RGB triplets
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public EQRGB[] SpriteRGB;

        // 12 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public float[] RollRate;

        // 12 shorts
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public short[] HeadingOffset;

        // 12 shorts
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public short[] PitchOffset;

        // 12 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public float[] Distance;

        // 12 shorts
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public short[] EffectType;

        // 12 floats
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public float[] ScaleFactor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SpellEffectOld
    {
        public int Tgts;
        public int Perm;

        // 3 StageType structs
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public StageType[] types;
    }
}

