using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestHarness.Entities
{
    /// <summary>
    /// View-specific wrapper for converting Node entity to consumable JSON
    /// </summary>
    public class DisplayNode
    {
        private readonly DisplayNode _left;
        private readonly DisplayNode _right;
        private readonly string _name;

        public DisplayNode(DisplayNode left, DisplayNode right, string name)
        {
            _left = left;
            _right = right;
            _name = name;
        }

        [JsonIgnore]
        public DisplayNode Left
        {
            get
            {
                return _left;
            }
        }

        [JsonIgnore]
        public DisplayNode Right
        {
            get
            {
                return _right;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public bool highlight { get; set; }

        public IEnumerable<DisplayNode> children
        {
            get
            {
                List<DisplayNode> c = new List<DisplayNode>();
                if (Left != null) c.Add(Left);
                if (Right != null) c.Add(Right);
                return c;
            }
        }

    }
}
