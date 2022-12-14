# TryBlog Backend - Aceleração de C#

Esse projeto consiste em uma API REST que retornaria informações para o front-end. A aplicação seria um fórum para estudantes, que permite os usuários
criar postagens novas, alterar postagens antigas, buscar postagens, etc. 

<details>
  <summary><strong>Tecnologias utilizadas:</strong></summary><br />
  Foi utilizado <strong>SQL Server</strong> para gerenciar o banco de dados e <strong>ASP.NET Core 6</strong> para criar a aplicação Web, 
  o ORM <strong>Entity Framework</strong> para realizar a conexão do banco de dados com a API, <strong>XUnit</strong> e <strong>FluentAssertions</strong>
  para realizar os testes.
</details>

<details>
  <summary><strong>Como rodar a aplicação:</strong></summary><br />
 Como a aplicação utiliza um banco de dados local será necessário rodar um container Docker com uma imagem SQL Server para conseguir realizar as requisições.
 <br>
  <ul>
    <li>Primeiro rode <code>docker-compose up -d</code> para subir o container com o Banco de Dados</li>
    <li>A senha para a conexão com o DB  é <code>Senha123$</code>, o server é <code>127.0.0.1</code> e o username é <code>SA</code></li> 
    <li>Após conectar ao SQL Server, rode a query <code>TryBlog_Query.sql</code> na sua ferramenta de banco de dados</li>
    <li>Vá para a pasta TryBlog e rode o comando <code>dotnet restore</code>, seguido de <code>dotnet run</code></li>
  </ul>
</details>

<hr>
