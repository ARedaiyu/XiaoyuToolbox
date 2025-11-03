namespace XiaoyuToolbox.Common
{
    public class ToolboxVersion(int major, int minor, int patch, VersionType type, int build)
    {
        public static ToolboxVersion Current { get; } = new ToolboxVersion(1, 0, 0, VersionType.Alpha, 3);

        public int Major { get; } = major;
        public int Minor { get; } = minor;
        public int Patch { get; } = patch;
        public VersionType Type { get; } = type;
        public int Build { get; } = build;

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}-{Type}{Build}";
        }
    }

    public enum VersionType
    {
        Alpha,
        Beta,
        Release,
        Unknown
    }
}
