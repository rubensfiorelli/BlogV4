# BlogV4 - Api de estudos - Baseada em um exercício do Balta.io, fiz o refatoramento completo.

mudando de uma api de arquivo único, para uma APi baseada em Clean Arch, usando geração e validação de Tokens JWT, 
Dominios ricos com comportamentos, metodos, validações no dominio, setter TODOS privados e 
Getters abertos para a extensão, assim como recomenda o SOLID.

Acredito que o código esteja bem limpo e organizado, código praticamente livre de dependencias, 
dependendo apenas do Entity Framework.

Api esta acessando um Banco SQL Server via Docker;

Código escrito em .NET 7, Entity Framework 7

Todas os mapeamentos de Dominio/DTO estao feitos com implicit operators, obviamente para o desempenho de performance
superior e jamais depender AutoMapper por exemplo.

Deixando o código muito mais limpo, performatico e sem dependencias.
