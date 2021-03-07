﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d
{
    public class ContainerTests
    {
        private Container container;
        private Rectangle child1;
        private Rectangle child2;

        [SetUp]
        public void SetUp()
        {
            container = new Container
            {
                Offset = new Vector2(100),
                Padding = new MarginPadding { Horizontal = 50, Vertical = 50 }
            };
            container.AddChild(child1 = new Rectangle { Size = new Vector2(230) });
            container.AddChild(child2 = new Rectangle
            {
                Offset = new Vector2(100),
                Size = new Vector2(50, 300)
            });
        }

        [Test]
        public void TestAutoSizeAxes()
        {
            container.AutoSizeAxes = Axes.Both;
            container.Update();
            Assert.AreEqual(new Vector2(230, 400), container.ContainerLayoutInfo.ChildrenRenderSize);
            Assert.AreEqual(new RectangleF
            {
                Left = 150,
                Top = 150,
                Width = 230,
                Height = 230
            }, child1.LayoutInfo.RenderRect);
            Assert.AreEqual(new RectangleF
            {
                Left = 250,
                Top = 250,
                Width = 50,
                Height = 300
            }, child2.LayoutInfo.RenderRect);
        }
    }
}