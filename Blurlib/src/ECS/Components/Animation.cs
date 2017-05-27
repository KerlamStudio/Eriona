using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Blurlib.ECS.Components
{
    public class Frame
    {
        public Texture2D Texture;
        public Rectangle? TextureClip;
        public Vector2 TextureOffset;
        public Transform Hitbox;
    }

    public class FramesManager
    {
        public string Id { get; private set; }
        public List<Frame> Frames;
        public float DeltaTimeFrame;
        public int CurrentFrameIndex;
        private float currentDeltaTime;
        public bool Changed { get; private set; }

        public Frame CurrentFrame
        {
            get { return Frames[CurrentFrameIndex]; }
        }

        public FramesManager(string id, float deltaTimeFrame)
        {
            Id = id;
            DeltaTimeFrame = deltaTimeFrame;
            currentDeltaTime = 0;
            CurrentFrameIndex = 0;
            Changed = true;
        }

        public void Update()
        {
            currentDeltaTime += GameCore.DeltaTime;
            if (currentDeltaTime >= DeltaTimeFrame)
            {
                NextFrame();
                currentDeltaTime = 0;
                Changed = true;
            }
            else
            {
                Changed = false;
            }
        }

        public void NextFrame()
        {
            if (CurrentFrameIndex + 1 >= Frames.Count)
            {
                CurrentFrameIndex = 0;
            }
            else
            {
                CurrentFrameIndex++;
            }
        }

    }

    public class Animation : Sprite
    {
        private Dictionary<string, FramesManager> _animations;

        private FramesManager _currentAnimation;
        public FramesManager CurrentAnimation
        {
            get { return _currentAnimation; }
        }

        public string CurrentAnimationId
        {
            get
            {
                if (_currentAnimation.IsNull())
                {
                    return string.Empty;
                }
                else
                {
                    return _currentAnimation.Id;
                }
            }
        }

        private string _nextAnimation;

        public Animation(int zIndex = 0) : base(default(Texture2D), true, zIndex)
        {
            Active = true;
            _animations = new Dictionary<string, FramesManager>();
        }

        public override void Update()
        {
            base.Update();
            if (_nextAnimation != CurrentAnimationId)
            {
                _currentAnimation = _animations[_nextAnimation];

                Texture = _currentAnimation.CurrentFrame.Texture;
                TextureOffset = _currentAnimation.CurrentFrame.TextureOffset;
                TextureClip = _currentAnimation.CurrentFrame.TextureClip;
            }

            if (_currentAnimation.IsNotNull())
            {
                _currentAnimation.Update();

                if (_currentAnimation.Changed)
                {
                    Texture = _currentAnimation.CurrentFrame.Texture;
                    TextureOffset = _currentAnimation.CurrentFrame.TextureOffset;
                    TextureClip = _currentAnimation.CurrentFrame.TextureClip;
                }
            }
        }

        public void AddAnimation(string id, FramesManager frames)
        {
            if (!_animations.ContainsKey(id))
            {
                _animations.Add(id, frames);
            }
        }

        public void AddAnimation(string id, float delta, params Frame[] frames)
        {
            if (!_animations.ContainsKey(id))
            {
                _animations.Add(id, new FramesManager(id, delta) { Frames = new List<Frame>(frames) });
            }
        }

        public void RemoveAnimation(string id)
        {
            if (_animations.ContainsKey(id) && _currentAnimation.Id != id)
            {
                _animations.Remove(id);
            }
        }

        public void ChangeAnimation(string id)
        {
            if (_animations.ContainsKey(id))
            {
                _nextAnimation = id;
            }
        }
    }
}
