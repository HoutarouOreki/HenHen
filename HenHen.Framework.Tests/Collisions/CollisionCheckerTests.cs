﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Nodes;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Collisions
{
    public class CollisionCheckerTests
    {
        [Test]
        public void CheckNodeCollisionsTest()
        {
            var tc = new NodeCollisionTestCase();
            {
                var a = new TestCollisionNode(1, new Vector3(), new[] { new Sphere { Radius = 1 } });
                var b = new TestCollisionNode(2, new Vector3(1.5f, 0, 1.5f), new[] { new Sphere { Radius = 1.5f } });
                var c = new TestCollisionNode(3, new Vector3(-3, 0, 3), new[] { new Sphere { Radius = 3 } });
                var d = new TestCollisionNode(4, new Vector3(-3, -6, 3), new[] { new Sphere { Radius = 3 } });
                var e = new TestCollisionNode(5, new Vector3(0, 0, 2), new[] { new Sphere { Radius = 2 } });
                tc.AddExpectedCollision(a, b);
                tc.AddExpectedCollision(c, d);
                tc.AddExpectedCollision(e, a);
                tc.AddExpectedCollision(b, e);
                tc.AddExpectedCollision(e, c);
            }

            var collisionHandler = new TestCollisionHandler();
            CollisionChecker.CheckNodeCollisions(tc.Nodes, collisionHandler);
            foreach (var (a, b) in tc.ExpectedCollisions)
            {
                Assert.IsTrue(collisionHandler.DetectedCollisions.Contains((a, b)) || collisionHandler.DetectedCollisions.Contains((b, a)), $"Failed to detect an expected collision between two nodes.\nNode a: {a}\nNode b: {b}");
            }
            foreach (var (a, b) in collisionHandler.DetectedCollisions)
            {
                Assert.IsTrue(tc.ExpectedCollisions.Contains((a, b)) || tc.ExpectedCollisions.Contains((b, a)), $"Detected an unexpected collision between two nodes.\nNode a: {a}\nNode b: {b}");
            }
        }

        [Test]
        public void IsNodeContainedInMediumsTest() => Assert.Inconclusive("Test not implemented");

        public class NodeCollisionTestCase
        {
            private readonly HashSet<(Node a, Node b)> expectedCollisions = new();
            private readonly List<Node> nodes = new();

            public IReadOnlySet<(Node a, Node b)> ExpectedCollisions => expectedCollisions;
            public IReadOnlyList<Node> Nodes => nodes;

            public void AddExpectedCollision(Node a, Node b)
            {
                if (!nodes.Contains(a))
                    nodes.Add(a);
                if (!nodes.Contains(b))
                    nodes.Add(b);
                expectedCollisions.Add((a, b));
            }
        }

        private class TestCollisionHandler : ICollisionHandler
        {
            private readonly HashSet<(Node, Node)> detectedCollisions = new();
            public IReadOnlySet<(Node a, Node b)> DetectedCollisions => detectedCollisions;

            public void OnCollision(Node a, Node b)
            {
                Assert.IsFalse(detectedCollisions.Contains((a, b)));
                detectedCollisions.Add((a, b));
            }
        }
    }
}