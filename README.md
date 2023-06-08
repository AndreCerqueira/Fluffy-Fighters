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
