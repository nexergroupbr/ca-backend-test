**Teste para vaga de Desenvolvimento Back-end .NET**
---------------------
Criar uma API REST para gerenciar faturamento de clientes.
---------------------
**Funcionalidades 🛠️**

* Customer: CRUD;
Criar um cadastro do cliente com os seguintes campos:
    * Id;
    * Name;
    * Email;
    * Address;
    * **Todos os campos são de preenchimento obrigatório.**
* Produtos: CRUD;
Criar um cadastro de produtos com os seguintes campos:
    * Id;
    * Nome do produto;
    * **Todos os campos são de preenchimento obrigatório.**
* Controle de conferência e importação de billing.
    * Utilizar postman para consulta dos dados da API’s para criação das tabelas de billing e billingLines.
	  * Após consulta, e criação do passo anterior, inserir no banco de dados o primeiro registro do retorno da API de billing para criação de cliente e produto através do swagger ou dataseed.

    * Utilizar as API’s para consumo dos dados a partir da aplicação que está criada e fazer as seguintes verificações:
      * Se o cliente e o produto existirem, inserir o registro do billing e billingLines no DB local.
      * Caso se o cliente existir ou só o produto existir, deve retornar um erro na aplicação informando sobre a criação do registro faltante.
      * Criar exceptions tratando mal funcionamento ou interrupção de serviço quando API estiver fora.
* Lista de API’s :
	* Get https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing
	* Get https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
	* Post https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing
	* Delete https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
	* PUT https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
---------------------
**Requisitos 💻**

* A aplicação deverá ser desenvolvida usando .NET a partir da versão 5+;
* Modelagem de dados pode ser no banco de dados de sua preferência, podendo ser um banco relacional ou não relacional (mongodb, SQL Server, PostgreSQL, MySQL, etc);
* Persistência de dados no banco deverá ser feita utilizando o Entity Framework Core;
* O retorno da API deverá ser em formato JSON;
* Utilizar as requisições GET, POST, PUT ou DELETE, conforme a melhor prática;
* Criar o README do projeto descrevendo as tecnologias utilizadas, chamadas dos serviços e configurações necessário para executar a aplicação.
---------------------
**Pontos Extras ⭐**

* Desenvolvimento baseado em TDD;
* Práticas de modelagem de projeto;
* Criar e configurar o Swagger da API de acordo com as melhores práticas;
* Criar uma API para extração dos dados de faturamento.
* Sugestões serão bem vindas.
---------------------
**Submissão do teste 📝**

Crie um fork do teste para acompanharmos o seu desenvolvimento através dos seus commits.

---------------------
Obrigado!

Agradecemos sua participação no teste. Boa sorte! 😄
