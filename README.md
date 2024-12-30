
# Como utilizar

  

Para ser capaz de rodar este código em sua máquina local, será necessário baixar o seguinte:
- [AZ Func Core](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local) para desenvolver e executar funções do Azure
- git (para baixar o repositório)
- VSCode ou terminal+editor de texto
- Postman API Desktop Agent -> pode ser baixado no site oficial do Postman API, durante a tentativa enviar requisição GET para um localhost

# Dependências da Função

A função FnValidaCPF, dentro do projeto, conta com a dependência Newtonsoft.Json que foi instalada com os seguintes comandos:

`dotnet add package Newtonsoft.Json`
`dotnet restore`

Se você baixar o repositório via git clone, não será necessário instalar as dependências novamente, mas se baixar somente o script da FnValidaCpf, será necessário!

# Como executar

Abra o terminal na pasta do projeto, e digite:

`func start`

Abra o Postman API
Coloque o tipo de request POST
Coloque a url fornecida no terminal

Coloque o BODY do cpf a ser validado:
> {
>"cpf": "12345678909"
> }

cpf válido!

> {
>	"cpf":"12345678910"
> }

cpf inválido!

# Para publicar:

Crie uma função no portal azure com nome único global, um nome que dificilmente tenha sido usado por outra pessoa, já que esse nome será utilizado na url de requisição da API, como por exemplo:

Ruim:
> https://nomeAqui.azurewebsites.net/api/FnValidaCpf

Bom:
> https://BrandHardHeart001k01j92.azurewebsites.net/api/FnValidaCpf

Tenha certeza de que já tenha instalado a extensão do Portal Azure no VSCode, ou o CLI que conecta seu computador ao Portal Azure - e realize Login no portal com suas credenciais para que seja capaz de publicar uma Azure Function em sua conta

Depois basta executar o seguinte comando:

`func azure functionapp publish emillyteresadevss13`