using Godot;
using System;

public class WaveSpawner : Node
{
	[Signal]
	public delegate void WavesEnd();

	private EnemyContainer _enemyContainer;
	private AnimationPlayer _animationPlayer;
	private Label _waveLabel;
	
	private Character _target;
	private PathFollow2D _spawnLocation;
	private Timer _waveSpawnTimer;
	private int _wavesNum = 3;
	private int _currentWaveNum = 1;
	private int _enemyOnWave = 3;
	private Random _random = new Random();

	public override void _Ready()
	{
		_enemyContainer = GetNode<EnemyContainer>("EnemyContainer");
		_waveSpawnTimer = GetNode<Timer>("WaveSpawnTimer");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_waveLabel = GetNode<Label>("WaveLabel/Value");
	}

	public void _OnWaveSpawnTimerTimeout()
	{
		for (var i = 0; i < _enemyOnWave; i++)
		{
			var enemy = _enemyContainer.Add();
			AddChild(enemy);
			_spawnLocation.Offset = _random.Next();
			enemy.GlobalPosition = _spawnLocation.GlobalPosition;
			enemy.SetTarget(_target);
		}
	}

	private void _StartNewWave()
	{
		if (_currentWaveNum > _wavesNum)
		{
			EmitSignal(nameof(WavesEnd));
		}
		else
		{
			_waveLabel.Text = _currentWaveNum.ToString();
			_animationPlayer.Play("ShowWave");
			
			_currentWaveNum++;
			_waveSpawnTimer.Start();
		}
	}

	public void Start(int waveNum, int enemyOnWave, PathFollow2D spawnLocation, Character target)
	{
		_target = target;
		_spawnLocation = spawnLocation;
		_wavesNum = waveNum;
		_enemyOnWave = enemyOnWave;
		_StartNewWave();
	}

	public EnemyContainer GetEnemyContainer() => _enemyContainer;
}
