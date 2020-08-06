using Godot;
using System;

public class BattleGroundMusicPlayer : AudioStreamPlayer
{
	[Export] 
	public AudioStream BattleMusic;
	[Export] 
	public AudioStream WinMusic;
	[Export] 
	public AudioStream DeathMusic;
	
	public override void _Ready()
	{
		Stream = BattleMusic;
		Play();
	}

	public void PlayWinMusic()
	{
		Stop();
		Stream = WinMusic;
		Play();
	}

	public void PlayDeathMusic()
	{
		Stop();
		Stream = DeathMusic;
		Play();
	}
}
