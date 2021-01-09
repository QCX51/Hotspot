
namespace Native
{
    public enum IF_OPER_STATUS : uint
    {
        IfOperStatusUp = 1,
        IfOperStatusDown,
        IfOperStatusTesting,
        IfOperStatusUnknown,
        IfOperStatusDormant,
        IfOperStatusNotPresent,
        IfOperStatusLowerLayerDown,
    }
}
