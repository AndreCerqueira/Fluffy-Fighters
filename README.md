# Fluffy Fighters
Este projeto é sobre um jogo RPG (Role-Playing Game) Topdown 2D desenvolvido em Monogmae e C#.
No jogo, o jogador pode controlar o personagem principal, mover-se pelo mapa e participar de batalhas contra criaturas chamadas Fluffies. O jogo oferece uma jogabilidade dinâmica de RPG, com uma grande ênfase no sistema de combate e exploração do mapa.

## Grupo
- 20115 [Andre Cerqueira](https://github.com/AndreCerqueira)
- 20116 [Nuno Fernandes](https://github.com/NunoIsidoro)
- 25968 [Alexandre Marques](https://github.com/Alexmarques11)

## Requisitos
- .NET Framework
- Microsoft.Xna.Framework
- Microsoft.Xna.Framework.Graphics
- Microsoft.Xna.Framework.Input
- Microsoft.Xna.Framework.Audio
- Microsoft.Xna.Framework.Media
- Comora

## Funcionalidades
- Controle do jogador com as teclas WASD
- Sistema de batalha entre o jogador e os Fluffies
- Exploração do mapa
- Sistema de progressão para o fluffies do jogador

## Como Jogar
- Use as teclas WASD do teclado para mover o personagem controlável pelo cenário.
- Entre em combate com os Fluffies para ganhar experiência e melhorar os fluffies.
- Explore o mapa para encontrar novas áreas e Fluffies para batalhar.
- Desenvolva estratégias de combate para vencer as batalhas contra os Fluffies.

## Estrutura do Código
O projeto é dividido em várias classes, cada uma responsável por um aspecto específico do jogo. Algumas das mais importantes:

1. <b>Game1:</b> Classe principal do jogo que gere a lógica do jogo. Contém a inicialização, carregamento de conteúdo, atualização e métodos de desenho, e carrega o primeiro ecrã do jogo.
2. <b>Player:</b> Classe que gere o personagem controlável pelo jogador. Inclui a atualização da posição, animação e detecção de colisão.
3. <b>MapMonster:</b> Classe que gere os inimigos que se movem em direção ao jogador. Inclui a atualização da posição, animação.
4. <b>MonsterSpawner:</b> Classe que gere a lógica de criação de inimigos e o tempo entre as criações.
5. <b>Monster:</b> Classe que gere a lógica do monstro durante o combate contra o jogador.
6. <b>Sounds:</b> Classe estática que armazena efeitos sonoros e música de fundo utilizados no jogo.
7. <b>Map:</b> Classe que gere a lógica de inicialização do mapa, assim como atualização da sua posição de acordo com a movimentação da câmera, e colisões.
8. <b>AnimatedSprite:</b> Classes que animam sprites para personagens ou objetos do jogo.

# Análise da Organização das Pastas do Jogo
A organização das pastas do jogo está estruturada da seguinte forma:

- <b>Scripts:</b> Os arquivos de script estão na raiz do projeto, sem uma pasta específica.
- <b>Content:</b> A pasta Content contém todos os assets do jogo.
- <b>Font:</b> A fonte do jogo está localizada diretamente na pasta Content, sem uma pasta específica.
- <b>Sprites:</b> A pasta Sprites dentro de Content contém todos os sprites.
- <b>Sounds:</b> A pasta Sounds dentro de Content contém todos os audios.

## Análise do Código
### AnimatedSprite
A classe AnimatedSprite representa um sprite animado em um jogo. Um sprite é uma representação gráfica que é manipulada como uma única entidade na tela de um jogo. Aqui está uma análise de linha a linha do código fornecido:

### Código
```cs
public class AnimatedSprite
{
    private SpriteBatch spriteBatch;
    protected Texture2D texture;
    protected int rows;
    protected int columns;
    protected int currentRow;
    protected int currentColumn;
    public int width;
    public int height;
    private float timer;
    protected float animationSpeed;
    private Rectangle rectangle;

    public Vector2 position { get; set; }


    public AnimatedSprite(Game game, Texture2D texture, int rows, int columns)
    {
        this.texture = texture;
        this.rows = rows;
        this.columns = columns;
        currentRow = 0;
        currentColumn = 0;
        width = texture.Width / columns;
        height = texture.Height / rows;
        timer = 0;
        animationSpeed = 0.2f;
        spriteBatch = new SpriteBatch(game.GraphicsDevice);
    }


    public virtual void Update(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentColumn++;
            if (currentColumn >= columns)
                currentColumn = 0;
        }
    }


    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(width * currentColumn, height * currentRow, width, height);
        float layerDepth = 0.4f;
        spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, InGameScreen.GAME_SCALE_FACTOR, SpriteEffects.None, layerDepth);
    }
}
```
- Os campos privados spriteBatch, texture, rows, columns, currentRow, currentColumn, width, height, timer, animationSpeed e rectangle são usados para armazenar o estado atual do sprite animado. Por exemplo, texture armazena a textura do sprite (uma imagem ou desenho), enquanto rows e columns definem a estrutura da folha de sprites (sprite sheet). O timer é usado para controlar a velocidade da animação.
- O campo público position é uma propriedade que armazena a posição do sprite na tela do jogo.
- O método construtor AnimatedSprite(Game game, Texture2D texture, int rows, int columns) inicializa um novo objeto AnimatedSprite com uma textura específica, número de linhas e colunas. Ele também configura o spriteBatch para ser usado para desenhar o sprite no dispositivo gráfico do jogo.
- O método Update(GameTime gameTime) é chamado a cada quadro do jogo e é responsável por atualizar o estado do sprite. O código dentro deste método controla a animação do sprite, avançando para a próxima coluna da folha de sprites a cada intervalo de tempo definido por animationSpeed.
- O método Draw(SpriteBatch spriteBatch) é chamado a cada quadro do jogo e é responsável por desenhar o sprite na tela do jogo. Ele calcula qual porção da textura do sprite deve ser desenhada (com base no currentRow e currentColumn) e, em seguida, chama o método Draw do SpriteBatch para desenhar a textura na tela.

A principal função desta classe é controlar a animação e o desenho de um sprite. No entanto, esta é uma classe básica e pode ser estendida para adicionar mais funcionalidades, como detecção de colisão, movimento do sprite, entre outros.

### Player
A classe Player herda da classe AnimatedSprite e representa um jogador no jogo. Ela adiciona funcionalidades específicas do jogador à funcionalidade de sprite animado básica.

### Código
```cs
public class Player : AnimatedSprite
{
    // Constants
    private const string PLAYER_ASSET_PATH = "sprites/player/player_spritesheet";
    private const int PLAYER_ROWS = 5;
    private const int PLAYER_COLUMNS = 4;
    public const float SPEED = 100f;
    private const int IDLE_ROW = 0;
    private const int WALK_LEFT_ROW = 4;
    private const int WALK_RIGHT_ROW = 3;
    private const int WALK_UP_ROW = 2;
    private const int WALK_DOWN_ROW = 1;

    public Map map { get; set; }
    public Vector2 velocity { get; set; }

    public Player(Game game) : 
        base(game, game.Content.Load<Texture2D>(PLAYER_ASSET_PATH), PLAYER_ROWS, PLAYER_COLUMNS) 
    {
    }

    public override void Update(GameTime gameTime)
    {
        // Atualize a velocidade do jogador com base nas teclas pressionadas
        Vector2 playerInput = Vector2.Zero;
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.W))
            playerInput.Y -= 1;
        if (keyboardState.IsKeyDown(Keys.S))
            playerInput.Y += 1;
        if (keyboardState.IsKeyDown(Keys.A))
            playerInput.X -= 1;
        if (keyboardState.IsKeyDown(Keys.D))
            playerInput.X += 1;

        // Normalize o vetor de entrada para manter a velocidade constante independentemente da direção
        if (playerInput != Vector2.Zero)
            playerInput.Normalize();

        velocity = playerInput * SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
        map.Offset += velocity; // TODO: Move Map, with collisions, remove camera probably

        // Atualize a animação do jogador com base na direção
        if (playerInput.X < 0)
            currentRow = WALK_LEFT_ROW;
        else if (playerInput.X > 0)
            currentRow = WALK_RIGHT_ROW;
        else if (playerInput.Y < 0)
            currentRow = WALK_UP_ROW;
        else if (playerInput.Y > 0)
            currentRow = WALK_DOWN_ROW;
        else
            currentRow = IDLE_ROW;

        base.Update(gameTime);
    }


    public void DrawCollider(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = GetCollider();

        Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] { Color.Aqua });
        spriteBatch.Draw(texture, rectangle, Color.Aqua);
    }


    public Rectangle GetCollider()
    {
        return new Rectangle((int)position.X + 32, (int)position.Y + 64, 32, 32);
    }
}
```
- Constantes como PLAYER_ASSET_PATH, PLAYER_ROWS, PLAYER_COLUMNS, SPEED, IDLE_ROW, WALK_LEFT_ROW, WALK_RIGHT_ROW, WALK_UP_ROW e WALK_DOWN_ROW são usadas para definir características específicas do jogador, como o caminho para a textura do jogador, o número de linhas e colunas na folha de sprites do jogador, a velocidade do jogador, e as linhas correspondentes às animações de andar em diferentes direções e ficar parado.
- map é uma propriedade que armazena o mapa atual em que o jogador está.
- velocity é uma propriedade que armazena a velocidade atual do jogador.
- O método construtor Player(Game game) inicializa um novo objeto Player com a textura do jogador, número de linhas e colunas.
- Update(GameTime gameTime) é um método sobrescrito que atualiza o estado do jogador a cada quadro do jogo. Ele verifica o estado do teclado e atualiza a velocidade do jogador com base nas teclas pressionadas. Ele também atualiza a animação do jogador com base na direção em que o jogador está se movendo. Finalmente, ele chama Update(gameTime) na classe base para atualizar a animação do sprite.
- DrawCollider(SpriteBatch spriteBatch) é um método que desenha o colisor do jogador. Ele usa uma textura temporária de um pixel e dimensiona-a para a forma do colisor.
- GetCollider() é um método que retorna um Rectangle representando o colisor do jogador.

Em geral, essa classe fornece a funcionalidade básica para um jogador em um jogo, incluindo a capacidade de se mover pelo teclado e exibir diferentes animações com base na direção do movimento. Ele também inclui a capacidade de lidar com colisões, embora o código real para isso não esteja presente.

### MapMonster
A classe MapMonster herda de AnimatedSprite e representa um monstro no mapa do jogo. A classe MapMonster inclui vários campos e métodos que são específicos para a representação de um monstro em um mapa.

### Código
```cs
public class MapMonster : AnimatedSprite
{
    // Constants
    private const int MONSTER_ROWS = 4;
    private const int MONSTER_COLUMNS = 4;
    private const int WALK_LEFT_ROW = 1;
    private const int WALK_RIGHT_ROW = 3;
    private const int WALK_UP_ROW = 2;
    private const int WALK_DOWN_ROW = 0;

    public Monster monster { get; set; }
    public Map map { get; set; }
    public Vector2 velocity { get; set; }
    private float patrolTime;
    private float idleTime;
    private float speed;
    private float maxPatrolTime;
    private Color defaultColor = Color.White;
    private Color hoverColor = Color.LightGray;
    private Rectangle rectangle => new Rectangle(width * currentColumn, height * currentRow, width, height);

    public bool isHovering
    {
        get
        {
            var mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            Rectangle newRectangle = new Rectangle((int)position.X - (int)map.Offset.X, (int)position.Y - (int)map.Offset.Y, width, height);
            return mouseRectangle.Intersects(newRectangle);
        }
    }

    public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;

    // Clicked event
    public event EventHandler<Monster> OnClicked;

    public MapMonster(Game game, Map map, string assetPath, Monster monster) :
        base(game, game.Content.Load<Texture2D>(assetPath), MONSTER_ROWS, MONSTER_COLUMNS)
    {
        this.map = map;
        animationSpeed = 0.4f;
        position = new Vector2(100, 100);
        speed = GetRandomSpeed();
        maxPatrolTime = GetRandomMaxPatrolTime();
        this.monster = monster;
    }


    public override void Update(GameTime gameTime)
    {
        if (isClicked && isHovering)
            Clicked();

        Patrol(gameTime);

        base.Update(gameTime);
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
        var color = GetColor();
        Vector2 pos = position - map.Offset;
        float layerDepth = 0.4f;
        spriteBatch.Draw(texture, pos, rectangle, color, 0f, Vector2.Zero, InGameScreen.GAME_SCALE_FACTOR, SpriteEffects.None, layerDepth);
    }


    private void Clicked() => OnClicked?.Invoke(this, monster);


    public void DrawCollider(SpriteBatch spriteBatch)
    {
        Rectangle rectangle = GetCollider();

        Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] { Color.Aqua });
        spriteBatch.Draw(texture, rectangle, Color.Aqua);
    }


    public Rectangle GetCollider()
    {
        return new Rectangle((int)position.X + 32, (int)position.Y + 64, 32, 32);
    }


    private Color GetColor() =>isHovering ? hoverColor : defaultColor;


    public void Patrol(GameTime gameTime)
    {
        if (patrolTime > 0)
        {
            position += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            patrolTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            idleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (idleTime >= maxPatrolTime)
            {
                ChooseRandomDirection();
                patrolTime = maxPatrolTime;
                idleTime = 0f;
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (velocity.X > 0)
            currentRow = WALK_RIGHT_ROW;
        else if (velocity.X < 0)
            currentRow = WALK_LEFT_ROW;
        else if (velocity.Y > 0)
            currentRow = WALK_DOWN_ROW;
        else if (velocity.Y < 0)
            currentRow = WALK_UP_ROW;
    }


    private float GetRandomSpeed()
    {
        Random random = new Random();
        return random.Next(40, 100);
    }


    private float GetRandomMaxPatrolTime()
    {
        Random random = new Random();
        return random.Next(1, 5);
    }


    private void ChooseRandomDirection()
    {
        Random random = new Random();
        int direction = random.Next(0, 4);
        switch (direction)
        {
            case 0:
                velocity = new Vector2(0, -1);
                break;
            case 1:
                velocity = new Vector2(0, 1);
                break;
            case 2:
                velocity = new Vector2(-1, 0);
                break;
            case 3:
                velocity = new Vector2(1, 0);
                break;
        }
    }
}
```
1. Constantes: Semelhante à classe Player, temos várias constantes que definem o número de linhas e colunas na folha de sprite do monstro e a direção em que o monstro está se movendo.
Propriedades:
2. Propriedades:
- monster é uma propriedade que armazena o monstro associado a este MapMonster.
- map é uma propriedade que armazena o mapa atual em que o monstro está.
- velocity é uma propriedade que armazena a velocidade atual do monstro.
- isHovering e isClicked são propriedades que detectam se o monstro está sendo apontado ou clicado pelo mouse.
- rectangle retorna um Rectangle que representa a parte atual da folha de sprites a ser desenhada.
3. Eventos: OnClicked é um evento que é acionado quando o monstro é clicado.
4. Construtor: O construtor MapMonster() inicializa um novo MapMonster com o mapa e a textura fornecidos, bem como o número de linhas e colunas.
5. Métodos Update e Draw: Os métodos Update() e Draw() são sobrescritos para adicionar funcionalidade de atualização e desenho específica de monstro.
6. Métodos auxiliares: Existem vários métodos auxiliares como Patrol(), UpdateAnimation(), GetRandomSpeed(), GetRandomMaxPatrolTime(), e ChooseRandomDirection(). Esses métodos adicionam funcionalidades como patrulhar o mapa de forma aleatória, atualizar a animação com base na direção, gerar velocidades e tempos de patrulha aleatórios e escolher uma direção aleatória.
7. Métodos relacionados à colisão: GetCollider() retorna um Rectangle que representa o colisor do monstro, e DrawCollider() desenha o colisor.

### Attack
A classe Attack representa um ataque que um personagem de um jogo pode usar. Esses ataques têm várias propriedades que determinam a eficácia do ataque, incluindo:
- name: O nome do ataque.
- element: O elemento do ataque. Esta propriedade usa um tipo Element, que presumivelmente é um enum com várias opções de elementos possíveis, como Fogo, Água, Grama, etc.
- damage: A quantidade de dano que o ataque inflige.
- speed: A velocidade com que o ataque é lançado.
- successChance: A chance de sucesso do ataque.

### Código
```cs
public class Attack
{
    // Properties
    public string name { get; private set; }
    public Element element { get; private set; }
    public int damage { get; private set; }
    public int speed { get; private set; }
    public float successChance { get; private set; }

    // Constructors
    public Attack(string name, Element element, int damage, int speed, float successChance)
    {
        this.name = name;
        this.element = element;
        this.damage = damage;
        this.speed = speed;
        this.successChance = successChance;
    }


    // Methods
    // GetRandomAttack
    public static Attack GetRandomAttack(Element element, int level)
    {
        string name = GetRandomName(element);
        int damage = GetRandomDamage(level);
        int speed = GetRandomSpeed(damage);
        float successChance = GetRandomSuccessChance(damage);

        return new Attack(name, element, damage, speed, successChance);
    }


    private static string GetRandomName(Element element)
    {
        Random rnd = new Random();
        string[] nameArray;

        switch (element)
        {
            case Element.Water:
                nameArray = new string[] { "Water Splash", "Aqua Beam", "Hydro Blast" };
                break;
            case Element.Fire:
                nameArray = new string[] { "Fire Punch", "Inferno Strike", "Flame Burst" };
                break;
            case Element.Grass:
                nameArray = new string[] { "Leaf Blade", "Vine Whip", "Nature's Fury" };
                break;
            case Element.Neutral:
                nameArray = new string[] { "Neutral Strike", "Elemental Blast", "Mystic Wave" };
                break;
            default:
                nameArray = new string[] { "Unknown Element" };
                break;
        }

        return nameArray[rnd.Next(0, nameArray.Length)];
    }


    private static int GetRandomDamage(int level)
    {
        Random rnd = new Random();

        if (level < 5) 
            return rnd.Next(10, 40);
        else
            return rnd.Next(20, 70);
    }


    private static int GetRandomSpeed(int damage)
    {
        Random rnd = new Random();
        float speedMultiplier = (float)damage / 100;

        int minSpeed = (int)(100 - (speedMultiplier * 100));
        int maxSpeed = 100;

        return rnd.Next(minSpeed, maxSpeed + 1);
    }


    private static float GetRandomSuccessChance(int damage)
    {
        Random rnd = new Random();
        float successMultiplier = (float)damage / 100; 

        float minSuccessChance = successMultiplier * 0.5f;  
        float maxSuccessChance = 1.0f;

        return ((float)rnd.NextDouble() * (maxSuccessChance - minSuccessChance) + minSuccessChance) * 100f;
    }
}
```
Há um único construtor para a classe Attack, que inicializa todas as propriedades acima.
Existem também vários métodos na classe Attack:
1. GetRandomAttack: Este método cria um ataque com valores aleatórios para nome, dano, velocidade e chance de sucesso. Ele utiliza a função GetRandomName para gerar um nome de ataque aleatório baseado no elemento, e funções GetRandomDamage, GetRandomSpeed e GetRandomSuccessChance para gerar valores aleatórios para dano, velocidade e chance de sucesso, respectivamente.
2. GetRandomName: Este método gera um nome de ataque aleatório baseado no elemento. Cada elemento tem um conjunto de possíveis nomes de ataque, e um nome é escolhido aleatoriamente desse conjunto.
3. GetRandomDamage: Este método gera um valor de dano aleatório baseado no nível fornecido. A faixa de valores possíveis para o dano depende do nível.
4. GetRandomSpeed: Este método gera um valor de velocidade aleatório baseado no valor do dano. O dano atua como um multiplicador para determinar a faixa de valores possíveis para a velocidade.
5. GetRandomSuccessChance: Este método gera um valor de chance de sucesso aleatório baseado no valor do dano. Novamente, o dano atua como um multiplicador para determinar a faixa de valores possíveis para a chance de sucesso.

### Monster
A classe Monster representa um monstro no jogo, com várias propriedades e métodos que governam seu comportamento e estado.

### Código
```cs
public class Monster
{
    // Delegates
    public delegate void MonsterEventHandler(object sender, MonsterEventArgs e);

    // Constants
    public const string DEFAULT_ICON_ASSET_PATH = "sprites/ui/monster-icons/default-icon";

    // Properties
    public string assetPath { get; private set; }
    public string iconAssetPath { get; private set; }
    public string name { get; private set; }
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }
    public int level { get; private set; }
    public int maxXp { get; private set; }
    public int xp { get; private set; }
    public Element element { get; }
    public Attack[] attacks { get; private set; }

    // Events
    public event MonsterEventHandler OnDeath;


    // Constructors
    public Monster(string name, int maxHealth, Element element, Attack[] attacks, string assetPath, string iconAssetPath = null, int level = 1)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.element = element;
        this.attacks = attacks;
        this.assetPath = assetPath;
        this.level = level;
        this.iconAssetPath = iconAssetPath ?? DEFAULT_ICON_ASSET_PATH;
        maxXp = 100;
    }


    // Methods
    public void TakeDamage(float damage)
    {
        currentHealth -= (int)Math.Round(damage);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }


    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    public void HealAll() => currentHealth = maxHealth;


    public void GainXp(int amount)
    {
        xp += amount;
        if (xp >= 100)
        {
            level++;
            xp -= 100;

            // Upgrade max health
            Random r = new Random();
            maxHealth += 10 * r.Next(1, 6);
        }
    }


    public Attack GetRandomAttack()
    {
        return attacks[new Random().Next(attacks.Length)];
    }


    public void Death() => OnDeath?.Invoke(this, new MonsterEventArgs(this));


    public bool IsDead() => currentHealth <= 0;
}
```
Delegates: MonsterEventHandler é um delegate que define a assinatura de um método que será chamado quando um evento relacionado ao monstro for disparado.
Constantes: DEFAULT_ICON_ASSET_PATH é uma constante que define o caminho padrão para o ícone do monstro.
Propriedades: Monster tem várias propriedades para representar o estado atual do monstro, como o caminho do asset, o caminho do ícone, o nome, a saúde atual e máxima, o nível, a experiência (xp), o elemento e os ataques disponíveis para o monstro.
Eventos: OnDeath é um evento que é disparado quando o monstro morre.
Construtores: O construtor Monster() inicializa um novo monstro com os parâmetros fornecidos.

Métodos:
- TakeDamage(float damage) é usado para causar danos ao monstro.
- Heal(int amount) e HealAll() são usados para curar o monstro.
- GainXp(int amount) é usado para dar experiência ao monstro e atualizar seu nível e saúde máxima quando necessário.
- GetRandomAttack() retorna um ataque aleatório dos ataques disponíveis do monstro.
- Death() dispara o evento OnDeath.
- IsDead() verifica se o monstro está morto.

A classe Monster fornece uma boa estrutura para um monstro em um jogo, permitindo danos, cura, ganho de experiência e morte. Além disso, o evento OnDeath permite que outras partes do código respondam quando um monstro morre.

### MonsterSpawner
A classe MonsterSpawner é responsável por gerenciar e spawnar (criar) monstros no jogo.

### Código
```cs
public class MonsterSpawner
{
    private const int MONSTER_QUANTITY = 20;

    private Game game;
    private Map map;
    public List<MapMonster> monsters;

    private EventHandler<Monster> onMonsterClicked;
    public EventHandler<Monster> OnMonsterClicked
    {
        get { return onMonsterClicked; }
        set
        {
            onMonsterClicked = value;
            foreach (MapMonster monster in monsters)
            {
                monster.OnClicked += onMonsterClicked;
            }
        }
    }


    public MonsterSpawner(Game game, Map map)
    {
        this.game = game;
        this.map = map;
        monsters = new List<MapMonster>();

        for (int i = 0; i < MONSTER_QUANTITY; i++)
        {
            MapMonster monster = GetRandomMonster();
            monster.position = GetRandomSpawnPosition();
            monsters.Add(monster);
        }

    }


    public void Update(GameTime gameTime)
    {
        foreach (MapMonster monster in monsters)
        {
            monster.Update(gameTime);
        }
    }


    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (MapMonster monster in monsters)
        {
            monster.Draw(spriteBatch);
        }
    }


    // Get Random Spawn Position
    public Vector2 GetRandomSpawnPosition()
    {
        Random random = new Random();
        Vector2 spawnPosition = new Vector2();

        spawnPosition.X = random.Next(0, (int)Math.Round(map.map.Width * Map.FIXED_TILE_SIZE * Map.GAME_SCALE_FACTOR));
        spawnPosition.Y = random.Next(0, (int)Math.Round(map.map.Height * Map.FIXED_TILE_SIZE * Map.GAME_SCALE_FACTOR));

        return spawnPosition;
    }


    public MapMonster GetRandomMonster()
    {
        Random random = new Random();
        int monsterId = random.Next(0, 3);
        int level = GetRandomLevel();
        Monster monster;
        MapMonster mapMonster;

        switch (monsterId)
        {
            case 0:
                monster = new Monster("Fofi", GetRandomHealthByLevel(level), Element.Fire, GetRandomAttacksByElement(Element.Fire, level), "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon", level);
                mapMonster = new MapMonster(game, map, "sprites/monsters/fofi_spritesheet", monster);
                break;
            case 1:
                monster = new Monster("Bolhas", GetRandomHealthByLevel(level), Element.Water, GetRandomAttacksByElement(Element.Water, level), "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon", level);
                mapMonster = new MapMonster(game, map, "sprites/monsters/bolhas_spritesheet", monster);
                break;
            default:
                monster = new Monster("Tonco", GetRandomHealthByLevel(level), Element.Grass, GetRandomAttacksByElement(Element.Grass, level), "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon", level);
                mapMonster = new MapMonster(game, map, "sprites/monsters/toco_spritesheet", monster);
                break;
        }

        return mapMonster;
    }


    // get random level 1-10
    private int GetRandomLevel()
    {
        Random random = new Random();
        return random.Next(1, 11);
    }


    // get random attacks by element
    private Attack[] GetRandomAttacksByElement(Element element, int level)
    {
        Attack[] attacks = new Attack[4];
        for (int i = 0; i < 4; i++)
            attacks[i] = Attack.GetRandomAttack(element, level);
        return attacks;
    }


    // get random health by level
    private int GetRandomHealthByLevel(int level)
    {
        Random random = new Random();
        return random.Next(10, 100) * level;
    }

}
```

Constantes: MONSTER_QUANTITY é a quantidade de monstros que serão criados.
Propriedades:
- game é uma instância do jogo.
- map é uma instância do mapa onde os monstros serão spawnados.
- monsters é uma lista que armazena os monstros spawnados.
- OnMonsterClicked é um evento que é disparado quando um monstro é clicado.
Construtores: O construtor MonsterSpawner() cria uma nova instância de MonsterSpawner, inicializa suas propriedades e gera um número definido de monstros em posições aleatórias.

Métodos:
- Update(GameTime gameTime) atualiza o estado de cada monstro.
- Draw(SpriteBatch spriteBatch, GameTime gameTime) desenha cada monstro na tela.
- GetRandomSpawnPosition() retorna uma posição aleatória no mapa para spawnar um monstro.
- GetRandomMonster() cria e retorna um monstro aleatório.
- GetRandomLevel(), GetRandomAttacksByElement(Element element, int level), GetRandomHealthByLevel(int level) são métodos auxiliares usados para gerar um monstro com um nível, ataques e saúde aleatórios.

Esta classe, em particular, se encarrega de gerenciar os monstros no jogo, cuidando da criação dos monstros, atualização de seus estados e exibição na tela. Além disso, ela também se encarrega de gerar aleatoriamente as propriedades dos monstros, como posição, nível, ataques e saúde. Por último, ela também permite que outras partes do código se inscrevam para serem notificadas quando um monstro é clicado, através do evento OnMonsterClicked.

### Team
A classe Team parece representar um time de monstros em um jogo, provavelmente algum tipo de jogo de batalha de monstros.

### Código
```cs
public class Team
{
    // Delegates
    public delegate void LoseEventHandler(object sender, LoseEventArgs e);

    // Constants
    public const int MAX_MONSTERS = 3;

    // Properties
    private Monster[] monsters { get; set; }
    private int currentMonsterIndex { get; set; }

    // Events
    public event LoseEventHandler OnLose;


    // Constructors
    public Team()
    {
        this.monsters = new Monster[MAX_MONSTERS];
        currentMonsterIndex = 0;

    }


    // Methods
    public void AddMonster(Monster monster, int? position = null)
    {
        int pos = position ?? monsters.ToList().IndexOf(null);

        if (pos > MAX_MONSTERS || !HaveAvailableSpots())
            throw new ArgumentOutOfRangeException("position", "Position must be less than or equal to MAX_MONSTERS.");

        monster.OnDeath += OnMonsterDeath;
        monsters[pos] = monster;
    }


    public void RemoveMonster(Monster monster)
    {
        monsters.Where(m => m == monster).ToList().Remove(monster);
    }


    public Monster GetMonster(int position) => monsters[position];
    public Monster GetSelectedMonster() => monsters[currentMonsterIndex];
    public List<Monster> GetMonsters() => monsters.Where(m => m != null).ToList();


    public void HeallAllMonsters()
    {
        foreach (var monster in monsters)
            monster.HealAll();
    }


    public void SelectMonster(int position) => currentMonsterIndex = position;
    public void SelectMonster(Monster monster) => currentMonsterIndex = monsters.ToList().IndexOf(monster);


    public void SelectNextMonster()
    {
        currentMonsterIndex++;
        if (currentMonsterIndex > MAX_MONSTERS)
            currentMonsterIndex = 0;
    }


    public void SelectPreviousMonster()
    {
        currentMonsterIndex--;
        if (currentMonsterIndex < 0)
            currentMonsterIndex = 0;
    }


    public bool HaveAvailableSpots() => monsters.Where(m => m == null).ToList().Count > 0;


    private bool HaveMonstersAlive() => monsters.Where(m => m != null && m.currentHealth > 0).ToList().Count > 0;


    private void OnMonsterDeath(object sender, MonsterEventArgs e)
    {
        if (!HaveMonstersAlive())
            Lose();
    }


    private void Lose() => OnLose?.Invoke(this, new LoseEventArgs(this));

}
```

Constantes: MAX_MONSTERS é o número máximo de monstros que um time pode ter.
Propriedades:
- monsters é um array que armazena os monstros do time.
- currentMonsterIndex é um índice que aponta para o monstro atualmente selecionado no array monsters.

Eventos:
- OnLose é um evento disparado quando todos os monstros do time estão mortos.

Construtores: O construtor Team() cria uma nova instância de Team, inicializando as propriedades monsters e currentMonsterIndex.

Métodos:
- AddMonster(Monster monster, int? position = null) adiciona um monstro ao time em uma posição específica ou na primeira posição vazia. Também configura o monstro para acionar o método OnMonsterDeath quando morre.
- RemoveMonster(Monster monster) remove um monstro do time.
- GetMonster(int position), GetSelectedMonster(), GetMonsters() retornam um monstro em uma posição específica, o monstro atualmente selecionado, e todos os monstros do time, respectivamente.
- HealAllMonsters() cura todos os monstros do time.
- SelectMonster(int position) e SelectMonster(Monster monster) permitem selecionar um monstro por posição ou por referência, respectivamente.
- SelectNextMonster() e SelectPreviousMonster() alteram a seleção para o próximo ou anterior monstro, respectivamente.
- HaveAvailableSpots() e HaveMonstersAlive() verificam se há espaços disponíveis no time e se há monstros vivos, respectivamente.
- OnMonsterDeath(object sender, MonsterEventArgs e) é acionado quando um monstro morre. Se não houver monstros vivos, o evento OnLose é disparado.
- Lose() dispara o evento OnLose.

No geral, essa classe gerencia um time de monstros, permitindo a adição e remoção de monstros, seleção de um monstro específico, e cura de todos os monstros. Ela também controla o evento de derrota do time, que é disparado quando todos os monstros estão mortos.

### Map
Esta é uma classe Map que parece estar projetada para um jogo 2D usando sprites para renderização.

### Código
```cs
public class Map
{
    // Constants
    public const float GAME_SCALE_FACTOR = 0.75f;
    public const int FIXED_TILE_SIZE = 64;
    private readonly Vector2 PLAYER_START_POSITION = new(12, 6);

    // Properties
    private SpriteBatch spriteBatch;
    public TiledMap map;
    public TiledTileset[] tilesets;
    private Texture2D[] tilesetTextures;
    private string[] layersOverPlayer; 
    public Vector2 Offset { get; set; }

    private Player player;
    public MonsterSpawner spawner;


    public Map(Game game, Player player, string mapPath)
    {
        layersOverPlayer = new string[] { "trees", "pilares", "pedras", "bushes", "water" };

        this.player = player;
        player.position = PLAYER_START_POSITION * FIXED_TILE_SIZE * GAME_SCALE_FACTOR;

        spriteBatch = new SpriteBatch(game.GraphicsDevice);
        map = new TiledMap(game.Content.RootDirectory + mapPath);
        spawner = new MonsterSpawner(game, this);

        int tilesetCount = map.Tilesets.Length;
        tilesets = new TiledTileset[tilesetCount];
        tilesetTextures = new Texture2D[tilesetCount];

        for (int i = 0; i < tilesetCount; i++)
        {
            tilesets[i] = new TiledTileset("Content\\sprites\\tilesets\\" + Path.GetFileName(map.Tilesets[i].source));
            string imagePath = "Content\\sprites\\tilesImages\\" + Path.GetFileName(tilesets[i].Image.source);
            tilesetTextures[i] = Texture2D.FromStream(game.GraphicsDevice, File.OpenRead(imagePath));
        }
    }


    public void Update(Vector2 screenPosition, GameTime gameTime)
    {
        Vector2 previousOffset = Offset;

        player.Update(gameTime);

        spawner.Update(gameTime);

        if (CheckCollision(screenPosition, player.GetCollider()))
        {
            Offset = previousOffset;
        }
    }


    public void Draw(Vector2 screenPosition, GameTime gameTime)
    {
        spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

        // Draw layers below player
        foreach (TiledLayer layer in map.Layers)
        {
            if (!layer.visible || layersOverPlayer.Contains(layer.name))
                continue;

            DrawLayer(screenPosition, layer, true);
        }

        player.Draw(spriteBatch);
        spawner.Draw(spriteBatch, gameTime);

        // Draw layers over player
        foreach (TiledLayer layer in map.Layers)
        {
            if (!layer.visible || !layersOverPlayer.Contains(layer.name))
                continue;

            DrawLayer(screenPosition, layer, false);
        }

        spriteBatch.End();
    }


    private void DrawLayer(Vector2 screenPosition, TiledLayer layer, bool isBackground)
    {
        for (int y = 0; y < layer.height; y++)
        {
            for (int x = 0; x < layer.width; x++)
            {
                int index = y * layer.width + x;
                int tileId = layer.data[index];
                if (tileId == 0)
                    continue;

                // get tileset index for the tile
                int tilesetIndex = 0;
                for (int i = 0; i < map.Tilesets.Length; i++)
                {
                    if (tileId >= map.Tilesets[i].firstgid)
                        tilesetIndex = i;
                }
                if (tilesetIndex == 0)
                    continue;

                int tilesetTileId = tileId - map.Tilesets[tilesetIndex].firstgid;
                int tilesetTileX = tilesetTileId % tilesets[tilesetIndex].Columns;
                int tilesetTileY = tilesetTileId / tilesets[tilesetIndex].Columns;
                Rectangle sourceRectangle = new Rectangle(tilesetTileX * tilesets[tilesetIndex].TileWidth, tilesetTileY * tilesets[tilesetIndex].TileHeight, tilesets[tilesetIndex].TileWidth, tilesets[tilesetIndex].TileHeight);

                int scaledTileWidth = (int)(tilesets[tilesetIndex].TileWidth * GAME_SCALE_FACTOR);
                int scaledTileHeight = (int)(tilesets[tilesetIndex].TileHeight * GAME_SCALE_FACTOR);

                // Calculate the starting position of the destination rectangle based on the object's size
                int destinationX = (int)(x * FIXED_TILE_SIZE * GAME_SCALE_FACTOR) - (scaledTileWidth - (int)(FIXED_TILE_SIZE * GAME_SCALE_FACTOR)) - (int)Offset.X;
                int destinationY = (int)(y * FIXED_TILE_SIZE * GAME_SCALE_FACTOR) - (scaledTileHeight - (int)(FIXED_TILE_SIZE * GAME_SCALE_FACTOR)) - (int)Offset.Y;

                // Adjust destination based on the screen position
                destinationX -= (int)screenPosition.X;
                destinationY -= (int)screenPosition.Y;

                Rectangle destinationRectangle = new Rectangle(destinationX, destinationY, scaledTileWidth, scaledTileHeight);

                spriteBatch.Draw(tilesetTextures[tilesetIndex], destinationRectangle, sourceRectangle, Color.White);

                float layerDepth = isBackground ? 0f : GetLayerDepth(destinationY + tilesetTextures[tilesetIndex].Height);
                spriteBatch.Draw(tilesetTextures[tilesetIndex], destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
            }
        }
    }


    private List<Rectangle> GetCollisionRectangles(Vector2 screenPosition)
    {
        List<Rectangle> collisionRectangles = new List<Rectangle>();

        TiledLayer collisionLayer = map.Layers.First(l => l.name == "Colisions");

        if (collisionLayer != null)
        {
            foreach (TiledObject obj in collisionLayer.objects)
            {
                Rectangle rect = new Rectangle(
                    (int)(obj.x * GAME_SCALE_FACTOR - screenPosition.X - (int)Offset.X),
                    (int)(obj.y * GAME_SCALE_FACTOR - screenPosition.Y - (int)Offset.Y),
                    (int)(obj.width * GAME_SCALE_FACTOR),
                    (int)(obj.height * GAME_SCALE_FACTOR)
                );

                collisionRectangles.Add(rect);
            }
        }

        return collisionRectangles;
    }


    public bool CheckCollision(Vector2 screenPosition, Rectangle rectangle)
    {
        List<Rectangle> collisionRectangles = GetCollisionRectangles(screenPosition);

        foreach (Rectangle collisionRectangle in collisionRectangles)
        {
            if (collisionRectangle.Intersects(rectangle))
            {
                return true;
            }
        }

        return false;
    }


    public void DrawRectangles(SpriteBatch spriteBatch, List<Rectangle> rectangles, Color color)
    {
        foreach (Rectangle rectangle in rectangles)
        {
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { color });
            spriteBatch.Draw(texture, rectangle, color);
        }
    }


    public float GetLayerDepth(float y)
    {
        var result = MathHelper.Clamp(y / (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, 0.01f, 1f);
        return result;
    }
}
```

Constantes: GAME_SCALE_FACTOR, FIXED_TILE_SIZE e PLAYER_START_POSITION são constantes utilizadas para ajustar o mapa e a posição do jogador.

Propriedades:
- spriteBatch é um objeto SpriteBatch usado para renderizar os sprites no mapa.
- map e tilesets parecem ser relacionados ao mapa e conjuntos de tiles do jogo, provavelmente representando estruturas do mapa.
- tilesetTextures parece ser um array de Texture2D que provavelmente guarda as texturas dos tiles.
- layersOverPlayer é um array de strings que provavelmente contém os nomes das camadas que serão desenhadas por cima do jogador.
- Offset provavelmente é usado para ajustar a posição dos elementos do jogo.
- player é um objeto Player que representa o jogador do jogo.
- spawner é um MonsterSpawner, provavelmente responsável por gerar monstros no jogo.

Construtores: O construtor Map() inicializa uma nova instância de Map.

Métodos:
- Update(Vector2 screenPosition, GameTime gameTime) atualiza o estado do mapa e do jogador, além de verificar colisões.
- Draw(Vector2 screenPosition, GameTime gameTime) desenha os elementos do mapa, incluindo as camadas do mapa, o jogador e os monstros.
- DrawLayer(Vector2 screenPosition, TiledLayer layer, bool isBackground) é um método auxiliar que desenha uma camada do mapa.
- GetCollisionRectangles(Vector2 screenPosition) retorna uma lista de retângulos que representam áreas de colisão no mapa.
- CheckCollision(Vector2 screenPosition, Rectangle rectangle) verifica se o retângulo fornecido colide com qualquer retângulo de colisão no mapa.
- GetLayerDepth(float y) retorna a profundidade de uma camada no mapa.
No geral, essa classe controla o carregamento e desenho do mapa, a atualização e renderização do jogador e a geração de monstros, bem como a detecção de colisões.
    
### MainMenuScreen
Este código é para a classe MainMenuScreen, que é responsável por criar e controlar o menu principal de um jogo. Esta classe é um tipo de GameScreen, que é provavelmente uma classe de jogo genérica ou abstrata em algum lugar em sua base de código. 
    
### Código
```cs
public class MainMenuScreen : GameScreen
{
    // Constants
    private const string BACKGROUND_ASSET_PATH = "sprites/ui/background";
    private const string LOGO_ASSET_PATH = "sprites/ui/title";

    // Properties
    private ScreenManager screenManager;
    private SpriteBatch spriteBatch;
    private Texture2D backgroundTexture;
    private Texture2D logoTexture;
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    // Positioning
    Point center => new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
    Vector2 logoPosition => new(center.X - (logoTexture.Width / 2), center.Y - (logoTexture.Height / 2) - 240);

    private bool isHovering => playButton.isHovering || exitButton.isHovering; //  || settingsButton.isHovering

    // Player starting monsters
    private Attack tacle;
    private Attack waterPulse;
    private Attack ember;
    private Attack magicalLeaf;
    private Monster monster1;
    private Monster monster2;
    private Monster monster3;
    private Team playerTeam;


    public MainMenuScreen(Game game, ScreenManager screenManager) : base(game)
    {
        this.screenManager = screenManager;

        tacle = new Attack("Tackle", Element.Neutral, 10, 80, 100);
        waterPulse = new Attack("Water Pulse", Element.Water, 20, 70, 100);
        ember = new Attack("Ember", Element.Fire, 30, 60, 100);
        magicalLeaf = new Attack("Magical Leaf", Element.Grass, 40, 50, 100);
        monster1 = new Monster("Bolhas", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
        monster2 = new Monster("Fofi", 100, Element.Fire, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
        monster3 = new Monster("Tonco", 100, Element.Grass, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");
        playerTeam = new Team();
        playerTeam.AddMonster(monster3);
        playerTeam.AddMonster(monster1);
        playerTeam.AddMonster(monster2);
        playerTeam.OnLose += OnPlayerTeamLose;
    }


    public override void Initialize()
    {
        base.Initialize();
    }


    public override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Sounds.LoadSounds(Content);
        // Sounds.main.Play(volume: 0.5f, pitch: 0.0f, pan: 0.0f);

        // Load textures
        backgroundTexture = Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
        logoTexture = Content.Load<Texture2D>(LOGO_ASSET_PATH);

        // Create buttons
        CreateButtons();

        base.LoadContent();
    }


    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        spriteBatch.Draw(backgroundTexture, new Rectangle(Point.Zero, new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)), Color.White);
        spriteBatch.Draw(logoTexture, logoPosition, Color.White);
        spriteBatch.End();

        playButton.Draw(gameTime);
        // settingsButton.Draw(gameTime);
        exitButton.Draw(gameTime);
    }


    public override void Update(GameTime gameTime)
    {
        playButton.Update(gameTime);
        // settingsButton.Update(gameTime);
        exitButton.Update(gameTime);

        Mouse.SetCursor(isHovering ? Button.hoverCursor : Button.defaultCursor);
    }


    private void CreateButtons()
    {
        playButton = new Button(Game, "Play");
        Point position = new(center.X - (playButton.texture.Width / 2), center.Y + (playButton.texture.Height + Button.PADDING) * -1);
        playButton.SetPosition(position);
        playButton.OnClicked += OnPlayButtonClicked;

        // settingsButton = new Button(Game, "Settings");
        // position = new(center.X - (settingsButton.texture.Width / 2), center.Y + 0);
        // settingsButton.SetPosition(position);
        // settingsButton.OnClicked += OnSettingsButtonClicked;

        exitButton = new Button(Game, "Exit");
        position = new(center.X - (exitButton.texture.Width / 2), center.Y + 0);
        // position = new(center.X - (exitButton.texture.Width / 2), center.Y + (exitButton.texture.Height + Button.PADDING) * 1);
        exitButton.SetPosition(position);
        exitButton.OnClicked += OnExitButtonClicked;
    }


    private void OnPlayButtonClicked(object sender, EventArgs e)
    {
        InGameScreen inGameScreen = new(Game);
        screenManager.LoadScreen(inGameScreen);
        inGameScreen.map.spawner.OnMonsterClicked += OnMonsterClicked;
        inGameScreen.OnClose += OnInGameScreenClosed;
    }


    private void OnPlayerTeamLose(object sender, LoseEventArgs e)
    {
        e.team.HeallAllMonsters();
        screenManager.LoadScreen(this);
    }


    private void OnExitButtonClicked(object sender, EventArgs e)
    {
        Game.Exit();
    }


    private void OnInGameScreenClosed(object sender, EventArgs e)
    {
        screenManager.LoadScreen(this);
    }


    private void OnMonsterClicked(object sender, Monster m)
    {
        Monster monster4 = new Monster("Bolhas", 1, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
        Monster monster5 = new Monster("Fofi", 1, Element.Fire, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
        Monster monster6 = new Monster("Tonco", 1, Element.Grass, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");

        Team enemyTeam = new Team();
        enemyTeam.AddMonster(m);

        screenManager.LoadScreen(new CombatScreen(Game, playerTeam, enemyTeam, (newTeam1) =>
        {
            playerTeam = newTeam1;
            playerTeam.HeallAllMonsters();
            InGameScreen inGameScreen = new(Game);
            screenManager.LoadScreen(inGameScreen);
            inGameScreen.map.spawner.OnMonsterClicked += OnMonsterClicked;
            inGameScreen.OnClose += OnInGameScreenClosed;
        }));
    }
}
```

Propriedades e campos:
- BACKGROUND_ASSET_PATH e LOGO_ASSET_PATH: Strings constantes usadas para localizar as imagens de fundo e logo do menu.
- screenManager, spriteBatch, backgroundTexture, logoTexture, playButton, settingsButton, e exitButton: São objetos utilizados para gerenciar a tela, desenhar objetos 2D, e manipular os botões do menu, respectivamente.
- center e logoPosition: Pontos usados para centralizar elementos na tela.
- isHovering: Um booleano que verifica se o mouse está passando sobre um dos botões.
- tacle, waterPulse, ember, magicalLeaf, monster1, monster2, monster3, e playerTeam: São objetos relacionados ao gameplay do jogo.

Construtor MainMenuScreen:
No construtor, ele inicializa os objetos relacionados ao gameplay. Além disso, ele atribui ao evento OnLose de playerTeam o método OnPlayerTeamLose.

Método Initialize e LoadContent:
O método Initialize não faz nada especial além de chamar a implementação base, o que provavelmente faz alguma configuração genérica. No método LoadContent, os recursos gráficos do menu são carregados e os botões são criados.

Métodos Draw e Update:
No método Draw, a tela é limpa, o background e o logo são desenhados, e os botões são desenhados também. No método Update, a atualização dos botões é feita e a imagem do cursor é atualizada dependendo se ele está sobre um botão ou não.

Método CreateButtons:
Cria os botões do menu e os posiciona na tela. Além disso, atribui métodos a serem chamados quando os botões são clicados.

Métodos OnPlayButtonClicked, OnExitButtonClicked, OnInGameScreenClosed, e OnMonsterClicked:
São os métodos que são chamados quando os botões são clicados. OnPlayButtonClicked carrega a tela do jogo, OnExitButtonClicked fecha o jogo, OnInGameScreenClosed recarrega o menu quando a tela do jogo é fechada, e OnMonsterClicked carrega a tela de combate quando um monstro é clicado.

Método OnPlayerTeamLose:
É o método que é chamado quando o jogador perde. Ele recupera a saúde de todos os monstros do jogador e recarrega a tela do menu.

Em geral, esta classe cuida da lógica por trás do menu principal do jogo, que inclui o carregamento de recursos, a criação de botões, o desenho dos elementos na tela, e a manipulação dos eventos de clique dos botões. Além disso, lida com o carregamento das outras telas do jogo e com a perda do jogo pelo jogador.

### CombatManager
A classe CombatManager é responsável pela lógica de combate do jogo.
    
### Código
```cs
public class CombatManager
{
    // Delegates
    public delegate void AttackPerformedEventHandler(object sender, AttackPerformedEventArgs e);

    // Constants
    private const int ATTACK_DELAY = 1000;

    // Properties
    public Team playerTeam;
    public Team enemyTeam;
    public Monster playerSelectedMonster => playerTeam.GetSelectedMonster();
    public Monster enemySelectedMonster => enemyTeam.GetSelectedMonster();


    // Constructors
    public CombatManager(Team playerTeam, Team enemyTeam)
    {
        this.playerTeam = playerTeam;
        this.enemyTeam = enemyTeam;
    }

    // Events
    public event MonsterEventHandler onMonsterSelected;
    public event EventHandler onMonsterDied;
    public event EventHandler onTurnEnd;
    public event EventHandler onTurnStart;
    public event AttackPerformedEventHandler onAttackPerformed;
    public event EventHandler onAttackFailed;
    public event EventHandler onBattleEnd;


    // Methods
    public void SelectMonster(object sender, MonsterEventArgs e)
    {
        Attack enemyAttack = SelectEnemyAttack();
        PerformAttack(enemyAttack, enemySelectedMonster, playerTeam);

        onMonsterSelected?.Invoke(sender, e);
    }


    public Attack SelectEnemyAttack() => enemySelectedMonster.GetRandomAttack();


    public Queue<Action> GetAttackOrder(Attack playerAttack)
    {
        Queue<Action> actions = new Queue<Action>();
        Attack enemyAttack = SelectEnemyAttack();

        if (playerSelectedMonster.IsDead() || enemySelectedMonster.IsDead())
            return null;

        // get whose is faster
        bool playerAttackedFirst = false;
        if (playerAttack.speed > enemyAttack.speed)
            playerAttackedFirst = true;
        else if (playerAttack.speed == enemyAttack.speed)
            playerAttackedFirst = (new Random().Next(0, 2) == 0);

        Monster firstAttacker = playerAttackedFirst ? playerSelectedMonster : enemySelectedMonster;
        Monster secondAttacker = playerAttackedFirst ? enemySelectedMonster : playerSelectedMonster;

        // Get teams
        Team firstTeam = playerAttackedFirst ? playerTeam : enemyTeam;
        Team secondTeam = playerAttackedFirst ? enemyTeam : playerTeam;

        // Get attacks
        Attack firstAttack = playerAttackedFirst ? playerAttack : enemyAttack;
        Attack secondAttack = playerAttackedFirst ? enemyAttack : playerAttack;

        // Perform attacks
        actions.Enqueue(() => PerformAttack(firstAttack, firstAttacker, secondTeam));
        actions.Enqueue(() => PerformAttack(secondAttack, secondAttacker, firstTeam));

        return actions;
    }


    public async void DoTurn(object sender, AttackEventArgs e)
    {
        onTurnStart?.Invoke(this, EventArgs.Empty);

        Queue<Action> actions = GetAttackOrder(e.attack);
        if (actions == null) return;

        while (actions.Count > 0) { 
            Action action = actions.Dequeue();
            action();
            await Task.Delay(1000);
        }

        onTurnEnd?.Invoke(this, EventArgs.Empty);
    }


    public void PerformAttack(Attack attack, Monster attacker, Team target)
    {
        if (attacker.IsDead() || !IsAttackSuccessful(attack))
        {
            onAttackFailed?.Invoke(this, EventArgs.Empty);
            return;
        }

        float damage = GetDamage(attack, attacker, target.GetSelectedMonster());
        target.GetSelectedMonster().TakeDamage(damage);

        onAttackPerformed?.Invoke(this, new AttackPerformedEventArgs(attack, attacker, target));
    }


    private bool IsAttackSuccessful(Attack attack)
    {
        Random random = new Random();
        int chance = random.Next(0, 100);
        return attack.successChance >= chance;
    }


    private float GetAttackMultiplier(Attack attack, Monster target)
    {
        ElementEffectiveness effectiveness = attack.element.GetElementEffectiveness(target.element);
        return effectiveness switch
        {
            ElementEffectiveness.Effective => 1.5f,
            ElementEffectiveness.NotEffective => 0.5f,
            _ => 1f
        };
    }


    private float GetDamage(Attack attack, Monster attacker, Monster target)
    {
        float multiplier = GetAttackMultiplier(attack, target);
        return attack.damage * multiplier;
    }

}
```
Propriedades:
- ATTACK_DELAY: uma constante que representa o atraso entre os ataques.
- playerTeam e enemyTeam: são instâncias da classe Team, que provavelmente representam as equipes do jogador e do inimigo, respectivamente.
- playerSelectedMonster e enemySelectedMonster: retornam o monstro selecionado para cada equipe.

Construtor CombatManager: O construtor inicializa as equipes do jogador e do inimigo.

Eventos:
Existem vários eventos que podem ser disparados durante um combate, incluindo onMonsterSelected, onMonsterDied, onTurnEnd, onTurnStart, onAttackPerformed, onAttackFailed e onBattleEnd. Esses eventos permitem que a interface do usuário ou outras partes do jogo respondam a ações durante o combate.

Método SelectMonster:
Este método é chamado quando um monstro é selecionado. Ele seleciona um ataque do monstro inimigo e executa o ataque. Ele então dispara o evento onMonsterSelected.

Método SelectEnemyAttack:
Este método retorna um ataque aleatório do monstro selecionado pelo inimigo.

Método GetAttackOrder:
Este método determina a ordem dos ataques com base na velocidade do ataque do jogador e do inimigo. Ele cria uma fila de ações, onde cada ação é um ataque de um monstro a outro.

Método DoTurn:
Este método executa um turno, que consiste em executar ações na fila criada pelo método GetAttackOrder. Entre cada ação, há um atraso de um segundo. Ele dispara os eventos onTurnStart e onTurnEnd.

Método PerformAttack:
Este método realiza um ataque de um monstro a outro. Se o atacante estiver morto ou o ataque não for bem-sucedido, o método retorna e dispara o evento onAttackFailed. Caso contrário, o monstro de destino recebe dano e o evento onAttackPerformed é disparado.

Método IsAttackSuccessful:
Este método determina se um ataque é bem-sucedido com base na chance de sucesso do ataque.

Método GetAttackMultiplier:
Este método determina o multiplicador de dano com base na eficácia do elemento do ataque contra o elemento do monstro de destino.

Método GetDamage:
Este método calcula o dano que um ataque causa a um monstro, levando em consideração o multiplicador de dano.

Em geral, essa classe implementa a lógica de combate do jogo, permitindo que monstros ataquem uns aos outros, determinando a ordem dos ataques e calculando o dano dos ataques. Ela também fornece vários eventos que podem ser usados para atualizar a interface do usuário ou responder a ações durante o combate.
