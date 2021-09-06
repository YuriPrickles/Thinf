using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

class AnvilUI : UIState
{
	UITextBox textbox;
	UIText button;
	UIPanel panel;
	public override void OnInitialize()
	{
		panel = new UIPanel();
		panel.Top.Set(0f, 0.313f);
		panel.Left.Set(0f, 0.351f);
		panel.Width.Set(500f, 0f);
		panel.Height.Set(200f, 0f);
		Append(panel);

		UIText text = new UIText("Rename your held item");
		text.Top.Set(0f, 0.069f);
		text.Left.Set(0f, 0.066f);
		panel.Append(text);

		textbox = new UITextBox("Write name here", 2, false);
		textbox.Top.Set(0f, 0.387f);
		textbox.Left.Set(0f, 0.059f);
		textbox.OnClick += OnTextboxClick;
		panel.Append(textbox);

		button = new UIText("Rename"); //ModContent.GetTexture("Terraria/UI/Reforge_0")
		button.Top.Set(0f, 0.179f);
		button.Left.Set(0f, 0.712f);
		button.TextColor = Color.Yellow;
		panel.Append(button);
		button.OnClick += OnButtonClick;
	}
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
	private void OnTextboxClick(UIMouseEvent evt, UIElement listeningElement)
	{
		Main.chatRelease = false;
		Main.drawingPlayerChat = true;
		PlayerInput.WritingText = true;
		Main.instance.HandleIME();
	}
    private void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
	{
		Player player = Main.LocalPlayer;
		player.HeldItem.SetNameOverride(textbox.Text.ToString());
	}

    protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		textbox.SetText(Main.GetInputText(Main.chatText));
		/*Main.chatRelease = false;
		Main.drawingPlayerChat = true;
		PlayerInput.WritingText = true;
		Main.instance.HandleIME();*/
		base.DrawSelf(spriteBatch);
    }
}
