using BinaryTreeAnalyzer;
using BinaryTreeAnalyzer.Entities;
using Newtonsoft.Json;
using NHttp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using TestHarness.Entities;

namespace TestHarness
{
    internal class Program
    {
        private static Node _tree;
        private static Node _first;
        private static Node _second;

        private static void Main(string[] args)
        {
            SetupTree();

            var result = Analyzer.ParentFinder(_tree, _first, _second);

            var displayNodes = ConvertToDisplayable(_tree, result);

            ServeResult(displayNodes);
        }

        /// <summary>
        /// Initial setup of tree to be scanned
        /// </summary>
        private static void SetupTree()
        {
            var leaf1 = new Node(null, null, "leaf1");
            var leaf2 = new Node(null, null, "leaf2");
            var leaf3 = new Node(null, null, "leaf3");
            var leaf4 = new Node(null, null, "leaf4");
            var node2 = new Node(leaf2, leaf1, "node2");
            var node1 = new Node(leaf3, node2, "node1");
            var head = new Node(leaf4, node1, "head");
            _tree = head;
            _first = leaf1;
            _second = leaf3;
        }

        /// <summary>
        /// Convert a binary tree to a set of view-specific nodes
        /// </summary>
        private static DisplayNode ConvertToDisplayable(Node current, Node nodeToHighlight)
        {
            if (current == null)
            {
                return null;
            }

            var displayNode = new DisplayNode(ConvertToDisplayable(current.Left, nodeToHighlight), 
                                              ConvertToDisplayable(current.Right, nodeToHighlight), current.Name);

            if (current == nodeToHighlight)
            {
                displayNode.highlight = true;
            }

            return displayNode;
        }

        /// <summary>
        /// Self-host and display HTML/D3 view and JSON data
        /// </summary>
        private static void ServeResult(DisplayNode result)
        {
            using (var server = new HttpServer())
            {
                server.RequestReceived += (s, e) =>
                {
                    var path = e.Request.Path;

                    using (var writer = new StreamWriter(e.Response.OutputStream))
                    {
                        if (path == "/")
                        {
                            writer.Write(File.ReadAllText("TreeView.html"));
                        }
                        if (path == "/nodedata.json")
                        {
                            writer.Write(JsonConvert.SerializeObject(result));
                        }
                    }
                };

                server.EndPoint = new IPEndPoint(IPAddress.Loopback, 52333);

                server.Start();

                Process.Start(String.Format("http://{0}/", server.EndPoint));

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
