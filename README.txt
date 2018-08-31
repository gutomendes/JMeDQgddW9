Olá!

	• Todos os métodos (com exceção do Login) necessitam dos seguintes headers:
		» "Authorization": token gerado ao logar.
		» "Login": login cadastrado para o usuário.
		
	• O banco deve ser gerado ao executar o arquivo "script.sql" dentro da pasta "Script DB".
		» Nesse script é criado um usuário padrão para o primeiro acesso aos métodos.
		» "Login": "JMeDQgddW9", "Password": "JMalucelli".
		» Lembrem-se de alterar a "ConnectionString" nos arquivos "appsettings.json"
		» A aplicação está funcionando com banco de dados SQL Server.

	• Caso possuam a ferramenta Postman, de testes de requisição POST, disponibilizei uma collection com exemplos das chamadas:
		» arquivo "Postman.postman_collection.json".
	
Atenciosamente,
Luis Gustavo