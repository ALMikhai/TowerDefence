using System.Collections.Generic;

namespace projectDirectory.Scens.Static
{
    public static class SelectedDefenders
    {
        private static List<ObjectCreator.Objects> _defenderPackedPaths = new List<ObjectCreator.Objects>
        {
            ObjectCreator.Objects.DEFENDERGINO,
            ObjectCreator.Objects.DEFENDER,
            ObjectCreator.Objects.DEFENDERGINO,
            ObjectCreator.Objects.DEFENDER,
            ObjectCreator.Objects.DEFENDER,
            ObjectCreator.Objects.DEFENDERGINO
        };

        public static List<ObjectCreator.Objects> GetDefenders() => new List<ObjectCreator.Objects>(_defenderPackedPaths);
    }
}
