using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Evie
{
    public class EQSpellEffectsNew
    {
        public static unsafe SpellEffectNew[] ReadFile(string fileName)
        {
            byte* pBase = null;
            using var mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Open, "NEWEFFMAP", 0, MemoryMappedFileAccess.Read);
            using var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
            accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pBase);

            try
            {
                long recordSize = Marshal.SizeOf<SpellEffectNew>();
                long recordCount = accessor.Capacity / recordSize;
                SpellEffectNew[] effects = new SpellEffectNew[recordCount];

                for (int i = 0; i < recordCount; i++)
                {
                    byte* recPtr = pBase + (i * recordSize);
                    effects[i] = Marshal.PtrToStructure<SpellEffectNew>((nint)recPtr);
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
    public struct StageEmitter
    {
        public int EmitterType;
        public int MinLevel;
        public int AttachType;     // 3 = target player's position
        public int DAGnum;         // 0–8
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StageTypeNew
    {
        public int SoundNum;

        // 4 emitters
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public StageEmitter[] Emitters;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct SpellEffectNew
    {
        // 64-byte fixed-length C string
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;

        // 3 stages
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public StageTypeNew[] Stage;
    }
}
