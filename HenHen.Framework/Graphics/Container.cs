﻿using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics
{
    public class Container : Drawable, IContainer<Drawable>
    {
        protected List<Drawable> Children { get; } = new List<Drawable>();

        public MarginPadding Padding;

        public Vector2 GetChildrenRenderPosition() => throw new System.NotImplementedException();

        public Vector2 GetChildrenRenderSize() => throw new System.NotImplementedException();

        IEnumerable<Drawable> IContainer<Drawable>.Children => Children;

        public void AddChild(Drawable child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild(Drawable child)
        {
            if (Children.Remove(child))
                child.Parent = null;
        }

        protected override void OnUpdate()
        {
            foreach (var child in Children)
                child.Update();
            base.OnUpdate();
        }

        protected override void OnRender()
        {
            foreach (var child in Children)
                child.Render();
            base.OnRender();
        }
    }
}
