
namespace BinaryTreeAnalyzer.Entities
{
    public class Node
    {
        private readonly Node left;
        private readonly Node right;
        private readonly string name;

        public Node(Node left, Node right, string name)
        {
            this.left = left;
            this.right = right;
            this.name = name;
        }

        public Node Left
        {
            get
            {
                return this.left;
            }
        }

        public Node Right
        {
            get
            {
                return this.right;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
