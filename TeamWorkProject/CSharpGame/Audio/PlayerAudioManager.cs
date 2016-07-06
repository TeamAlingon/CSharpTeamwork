namespace CSharpGame.Audio
{
    using System;
    using System.Collections.Generic;

    using CSharpGame.Enums;
    using CSharpGame.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class PlayerAudioManager : GameComponent
    {
        private readonly Character player;

        private readonly Dictionary<string, SoundEffectInstance> soundEffects;

        private string lastPlayedSound;

        public PlayerAudioManager(Game game, Character player)
            : base(game)
        {
            this.player = player;
            this.soundEffects = new Dictionary<string, SoundEffectInstance>();
            this.lastPlayedSound = string.Empty;

            this.Initialize();
            this.PlayLevelTheme();
        }

        public void PlayLevelTheme()
        {
            this.soundEffects["levelTheme"].Play();
        }

        public void StopLevelTheme()
        {
            this.soundEffects["levelTheme"].Stop();
        }

        public sealed override void Initialize()
        {
            SoundEffect runEffect = this.Game.Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            var run = runEffect.CreateInstance();
            run.IsLooped = false;
            this.soundEffects.Add(nameof(run), run);

            SoundEffect jumpEffect = this.Game.Content.Load<SoundEffect>("Soundtrack/jump");
            var jump = jumpEffect.CreateInstance();
            jump.IsLooped = false;
            this.soundEffects.Add(nameof(jump), jump);

            //SoundEffect hitEffect = this.Game.Content.Load<SoundEffect>("Soundtrack/scratch");
            //var hit = hitEffect.CreateInstance();
            //hit.IsLooped = false;
            //this.soundEffectInstances.Add(nameof(hit), hit);

            SoundEffect levelThemeEffect = this.Game.Content.Load<SoundEffect>("Soundtrack/level");
            var levelTheme = levelThemeEffect.CreateInstance();
            levelTheme.IsLooped = true;
            levelTheme.Volume = 0.1f;
            this.soundEffects.Add(nameof(levelTheme), levelTheme);

        }

        public override void Update(GameTime gameTime)
        {
            switch (this.player.State)
            {
                case CharacterState.Jumping:
                    if (this.lastPlayedSound != "jump")
                    {
                        this.soundEffects["jump"].Play();
                        this.soundEffects["run"].Stop();

                        this.lastPlayedSound = "jump";
                    }
                    break;

                case CharacterState.RunningLeft:
                case CharacterState.RunningRight:
                    this.soundEffects["run"].Play();
                    this.lastPlayedSound = "run";
                    break;

                case CharacterState.Idle:
                    this.soundEffects["run"].Stop();
                    this.soundEffects["jump"].Stop();
                    break;

                case CharacterState.Attacking:
                    this.soundEffects["run"].Stop();
                    this.soundEffects["jump"].Stop();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
