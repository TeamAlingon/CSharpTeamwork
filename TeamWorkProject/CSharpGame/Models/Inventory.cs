namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    using Microsoft.Xna.Framework;

    public class Inventory
    {
        public Inventory()
        {
            this.Collectables = new List<ICollectable>();
        }

        public List<ICollectable> Collectables { get; private set; }

        public void Collect(ICollectable item)
        {
            this.Collectables.Add(item);
        }

        public void Update(GameTime gameTime, Character character)
        {
            this.Collectables = this.Collectables.Where(c => !c.HasBeenUsed).ToList();

            foreach (ICollectable collectable in this.Collectables)
            {
                collectable.Update(gameTime);
            }
        }
    }
}