using Godot;
using System;
using System.Collections.Generic;
using projectDirectory.Scens.GameSM;

public class BattleGround : Node
{
	[Signal]
	public delegate void OnWaveEnd();

	private Character _crystal;
	private WaveSpawner _waveSpawner;
	private EnemyContainer _enemyContainer;
	private MoneyNode _moneyNode;
	private JumpScreen _jumpScreen;
	private Fireworks _fireworks;
	private DeathScreen _deathScreen;
	private BattleGroundMusicPlayer _musicPlayer;

	private StateMachine _stateMachine;
	public PauseState PauseState;
	public PlayerAttackState PlayerAttackState;

	private Global _global;
	private SceneChanger _sceneChanger;

	public override async void _Ready()
	{
		_crystal = GetNode<Character>("Crystal");
		_waveSpawner = GetNode<WaveSpawner>("WaveSpawner");
		_enemyContainer = _waveSpawner.GetEnemyContainer();
		_moneyNode = GetNode<MoneyNode>("Hud/Money");
		_global = GetTree().Root.GetNode<Global>("Global");
		_sceneChanger = GetTree().Root.GetNode<SceneChanger>("SceneChanger");
		_jumpScreen = GetNode<JumpScreen>("JumpScreen");
		_fireworks = GetNode<Fireworks>("Fireworks");
		_deathScreen = GetNode<DeathScreen>("DeathScreen");
		_musicPlayer = GetNode<BattleGroundMusicPlayer>("BattleGroundMusicPlayer");

		_enemyContainer.Connect(nameof(EnemyContainer.Updated), this, nameof(_OnEnemyContainerUpdated));
		_enemyContainer.Connect(nameof(EnemyContainer.EnemyDeath), this, nameof(_OnEnemyDeath));

		_stateMachine = new StateMachine();
		PauseState = new PauseState(this, _stateMachine);
		PlayerAttackState = new PlayerAttackState(this, _stateMachine);
		
		_stateMachine.Initialize(PlayerAttackState);
		
		_jumpScreen.EnterAnimation();
		await ToSignal(_jumpScreen, nameof(JumpScreen.AnimationFinished));
		
		GetNode<Effect>("Hud/WindEffect").Init(this);

		Start(3, _global.GetLevel());
	}

	public override void _Input(InputEvent @event)
	{
		_stateMachine.CurrentState.HandleInput(@event);
	}

	public void _OnCrystalBroke(Character character)
	{
		_global.Money.Add(_moneyNode.Get());
		_musicPlayer.PlayDeathMusic();
		_deathScreen.Start();
	}

	public void _OnWavesEnd()
	{
		_global.NextLevel();
		_musicPlayer.PlayWinMusic();
		_fireworks.Start();
	}

	private void _OnEnemyContainerUpdated()
	{
		if (!_enemyContainer.IsEmpty())
		{
			GetTree().CallGroup("Defender", "SetTarget", _enemyContainer.GetActual());
		}
	}

	private void _OnEnemyDeath(Enemy enemy)
	{
		_moneyNode.ViewAdd(enemy.GetCost(), enemy.GlobalPosition);
	}

	private void _OnContinueButtonPressed()
	{
		_stateMachine.ChangeState(PlayerAttackState);  
	}

	private void _OnPauseButtonPressed()
	{
		_stateMachine.ChangeState(PauseState);
	}

	private void _OnExitButtonPressed()
	{
		_stateMachine.ChangeState(PlayerAttackState);
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._menuState);
	}
	
	private async void _OnFireworksEnd()
	{
		_global.Money.Add(_moneyNode.Get());
		
		_jumpScreen.ExitAnimation();
		await ToSignal(_jumpScreen, nameof(JumpScreen.AnimationFinished));
		
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._menuState);
	}

	public void Start(int waveNum, int enemyOnWave)
	{
		_waveSpawner.Start(waveNum, enemyOnWave, GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation"), _crystal);
	}
}
