# Top-Down-Shooter
Projeto feito curso Aprenda Unity PRO no site: https://aprendaunity.com.br

<h2>Sumário</h2>
    <ol>
        <li><h4><a href="#C1">Player Controller</a></h4></li>
        <li><h4><a href="#C2">Sistema de Mira e Tiro</a></h4></li>
        <li><h4><a href="#C3">IA com Navmesh e Inimigos</a></h4></li>
        <li><h4><a href="#C4">Adicionando Obstáculos</a></h4></li>
        <li><h4><a href="#C5">Configurando Multiplas Armas</a></h4></li>
        <li><h4><a href="#C6">Animaçoes</a></h4></li>
        <li><h4><a href="#C7">Melee Attack</a></h4></li>
        <li><h4><a href="#C8">Sistema de Dano</a></h4></li>
        <li><h4><a href="#C9">Hud, Sistema de munição e regarca com array multidimensional</a></h4></li>
        <li><h4><a href="#C10">Sons</a></h4></li>
        <li><h4><a href="#C11">Sistema de Reload</a></h4></li>
        <li><h4><a href="#C12">Adicionado Sangue, alterando o sistema de arma para permitir upgrades</a></h4></li>
        <li><h4><a href="#C13">Empilhamento de Corpos</a></h4></li>
    </ol>

<h1 id="C1">Player Controller</h1>

<td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/Recordings/Movie_Player.gif" width="1000" height="595"/></td>

<p>O personagem vai se mover para cima ou para baixo e para direita e esquerda o poderá trocar de armas, e atirar com elas.</p>

<h3>Player Controller and Player Shooter</h3>

<p>O script playerController vai ser responsável pela movimentação do player e girar para os lados e o script playerShooter controlar armas que o jogador vai utilizar no jogo.</p> 

<table border="0">
   <tr>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_player-part1.png" width="562" height="600"/></td>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_player-part2.png" width="550" height="560"/></td>
    </tr>
</table>

<h1 id="C2">Sistema de Mira e Tiro</h1>

<p>O sistema mira vai ser controlado script playerShooter que vai dizer quantas armas no jogo time entre os tiros o alcance de cada arma e ícones das armas e qual e mira que vai seguir o mouse.
  O lineRenderer que vai definir a trajetória do tiro.</p>

<table border="0">
   <tr>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_playerShooter.png" width="550" height="840"/></td>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_lineRenderer.png" width="550" height="900"/></td>
    </tr>
</table>

<img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_Aim.png" width="549" height="465"/>

<h1 id="C3">IA com Navmesh e Inimigos</h1>

<p>Os Inimigos vão estar com dois script EnemyMovement que vai ser responsável pelo movimento do inimigo e outro EnemyHP que vai cuidar da vida do inimigo.</p>

<p>A navegação vai ser utilizando NavMeshAgent que vai mapear cenários e vai dizer onde inimigo pode andar e pode atravessar.</p>

<td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/Recordings/Enemy.gif" width="1000" height="595"/></td>

<h3 id="C4">Navigation</h3>

<p>As configurações altura e raio da navegação da ia do inimigo e como ela vai se comportar no cenário</p>

<table border="0">
   <tr>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_Enemy.png" width="550" height="900"/></td>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_Navigation.png" width="550" height="423"/></td>
    </tr>
</table>

<h1 id="C4">Adicionando Obstáculos</h1>

<p>No jogo vai ter obstáculos que o inimigo não poderá atravessar no jogo e o jogador também não vai atravessar e inimigo vai dar a volta o obstáculos no cenário para seguir o jogador.</p>

<td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/Recordings/Movie_015.gif" width="1000" height="595"/></td>

<h3 id="C3">NavMesh Surface</h3>

<p>O NavMesh Surface que vai definir os espaços que ia do inimigo não vai andar.</p>

<table border="0">
   <tr>
        <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_Navigation2.png" width="666" height="583"/></td>
       <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_NavMesh%20Surface.png" width="455" height="674"/></td>
    </tr>
</table>

<h3 id="C3">Cube BoxCollider</h3>

<p>O objeto carro dentro dele vai esta objeto boxcollider esse boxcollider que vai criar um obstáculos para inimigo não poder andar, 
    o quando faz bake no gameobject NavMesh Surface ele vai criar lugares onde ia do inimigo não vai andar e boxcollider são os lugares.</p>

<table border="0">
   <tr>
       <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_cube-car-boxcollider.png" width="673" height="498"/></td>
       <td><img src="https://github.com/guiardev/Top-Down-Shooter/blob/main/Assets/imgs/img_cube-car.png" width="453" height="477"/></td>
    </tr>
</table>
