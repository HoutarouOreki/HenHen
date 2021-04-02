﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds
{
    public class World
    {
        private readonly List<Medium> mediums = new();
        private readonly List<Node> nodes = new();

        private readonly ChunksManager chunksManager = new(new Vector2(10, 10), 32);
        public IReadOnlyCollection<Medium> Mediums => mediums;
        public IReadOnlyCollection<Node> Nodes => nodes;

        public object SynchronizedTime { get; private set; }

        public void Simulate(object newTime)
        {
            chunksManager.SimulationManager.SimulateAllChunks(newTime);
            SynchronizedTime = newTime;
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
            chunksManager.AddNode(node);
        }

        public void AddMedium(Medium medium)
        {
            mediums.Add(medium);
            chunksManager.AddMedium(medium);
        }

        protected virtual void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }
    }

    public enum ChunkSimulationStrategy
    {
        /// <summary>
        /// All the chunks in the world will be simulated.
        /// </summary>
        All,

        /// <summary>
        /// Only chunks in a circle will be simulated.
        /// </summary>
        BinaryCircle,

        /// <summary>
        /// Chunks in a given radius will be simulated,
        /// and chunks outside of it will get gradually
        /// less and longer simulation steps.
        /// </summary>
        FadingOutCircle
    }
}