using Terraria.ModLoader;
namespace Thinf.Projectiles
{
	public abstract class Minion : ModProjectile
	{
		protected bool justCreated = true;
		public override void AI()
		{
			CheckActive();
			Behavior();
			if (justCreated)
			{
				OnCreated();
				justCreated = false;
			}
		}
		public abstract void CheckActive();
		public abstract void Behavior();
		public virtual void OnCreated() { }
		public virtual void SelectFrame() { }
	}
}